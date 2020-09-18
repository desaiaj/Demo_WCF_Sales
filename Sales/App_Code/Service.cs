using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

public class Service : IService
{
    private static SalesEntities _context;

    public Service()
    {
        _context = new SalesEntities();
    }

    #region Customer
    public List<Service.CustomerData> GetCustomers()
    {
        return _context.Customers.Select(x =>
        new Service.CustomerData
        {
            CustomerID = x.CustomerID,
            CustomerName = x.CustomerName,
            YTDOrders = x.YTDOrders,
            YTDSales = x.YTDSales
        }).ToList<Service.CustomerData>();
    }

    public bool AddCustomer(CustomerData objCustomer)
    {
        try
        {
            Customer customer = new Customer();

            customer.CustomerID = objCustomer.CustomerID;
            customer.CustomerName = objCustomer.CustomerName;
            customer.YTDOrders = objCustomer.YTDOrders;
            customer.YTDSales = objCustomer.YTDSales;

            _context.Customers.Add(customer);
            _context.SaveChanges();
        }
        catch (Exception e)
        {
            return false;
        }

        return true;
    }

    public bool UpdateCustomer(CustomerData objCustomer)
    {
        try
        {
            using (SalesEntities _context = new SalesEntities())
            {
                var customer = _context.Customers.Where(x => x.CustomerID == objCustomer.CustomerID).FirstOrDefault();

                if(customer != null)
                {
                    //customer.CustomerID = objCustomer.CustomerID;
                    customer.CustomerName = objCustomer.CustomerName;
                    customer.YTDOrders = objCustomer.YTDOrders;
                    customer.YTDSales = objCustomer.YTDSales;

                    _context.SaveChanges();
                }
            }
        }
        catch (Exception e)
        {
            return false;
        }

        return true;
    }

    public bool DeleteCustomer(int customerID)
    {
        try
        {
            using (SalesEntities _context = new SalesEntities())
            {
                var customer = _context.Customers.Where(x => x.CustomerID == customerID).FirstOrDefault();

                if (customer != null)
                {
                    _context.Customers.Remove(customer);

                    _context.SaveChanges();
                }
            }
        }
        catch (Exception e)
        {
            return false;
        }

        return true;
    }
    #endregion

    #region Order
    public List<Service.OrderData> GetOrders()
    {
        return _context.Orders.Select(x =>
        new Service.OrderData
        {
            OrderID = x.OrderID,
            CustomerID = x.CustomerID,
            CustomerName = x.Customer.CustomerName,
            OrderDate = x.OrderDate,
            FilledDate = x.FilledDate,
            Status = x.Status,
            Amount = x.Amount
        }).ToList<Service.OrderData>();
    }

    public bool AddOrder(OrderData objOrder)
    {
        try
        {
            Order order = new Order();

            order.OrderID = objOrder.OrderID;
            order.CustomerID = objOrder.CustomerID;
            order.OrderDate = objOrder.OrderDate.GetValueOrDefault();
            order.FilledDate = objOrder.FilledDate.GetValueOrDefault();
            order.Status = objOrder.Status;
            order.Amount = objOrder.Amount;

            _context.Orders.Add(order);
            _context.SaveChanges();
        }
        catch (Exception e)
        {
            return false;
        }

        return true;
    }

    public bool UpdateOrder(OrderData objOrder)
    {
        try
        {
            using (SalesEntities _context = new SalesEntities())
            {
                var order = _context.Orders.Where(x => x.OrderID == objOrder.OrderID).FirstOrDefault();

                if (order != null)
                {
                    order.CustomerID = objOrder.CustomerID;
                    order.FilledDate = objOrder.FilledDate;
                    order.OrderDate = objOrder.OrderDate.GetValueOrDefault();
                    order.Status = objOrder.Status;
                    order.Amount = objOrder.Amount;

                    _context.SaveChanges();
                }
            }
        }
        catch (Exception e)
        {
            return false;
        }

        return true;
    }

    public bool DeleteOrder(int orderID)
    {
        try
        {
            using (SalesEntities _context = new SalesEntities())
            {
                var order = _context.Orders.Where(x => x.OrderID == orderID).FirstOrDefault();

                if (order != null)
                {
                    _context.Orders.Remove(order);

                    _context.SaveChanges();
                }
            }
        }
        catch (Exception e)
        {
            return false;
        }

        return true;
    }
    #endregion

    #region Model
    [DataContract]
    public class CustomerData
    {
        int customerID = 0;
        string customerName = String.Empty;
        int ytdOrders = 0;
        int ytdSales = 0;
        //public virtual ICollection<Order> Orders { get; set; }


        [DataMember]
        public int CustomerID
        {
            get { return customerID; }
            set { customerID = value; }
        }

        [DataMember]
        public string CustomerName
        {
            get { return customerName; }
            set { customerName = value; }
        }

        [DataMember]
        public int YTDOrders
        {
            get { return ytdOrders; }
            set { ytdOrders = value; }
        }

        [DataMember]
        public int YTDSales
        {
            get { return ytdSales; }
            set { ytdSales = value; }
        }
    }

    [DataContract]
    public class OrderData
    {
        int orderID = 0;
        int customerID = 0;
        string customerName = string.Empty;
        DateTime? orderDate = null;
        DateTime? filledDate = null;
        string status = String.Empty;
        string statusName = String.Empty;
        int amount = 0;
        //public virtual ICollection<Order> Orders { get; set; }

        [DataMember]
        public int OrderID
        {
            get { return orderID; }
            set { orderID = value; }
        }

        [DataMember]
        public int CustomerID
        {
            get { return customerID; }
            set { customerID = value; }
        }

        [DataMember]
        public string CustomerName
        {
            get { return customerName; }
            set { customerName = value; }
        }

        [DataMember]
        public DateTime? OrderDate
        {
            get { return orderDate; }
            set { orderDate = value; }
        }

        [DataMember]
        public DateTime? FilledDate
        {
            get { return filledDate; }
            set { filledDate = value; }
        }

        [DataMember]
        public string Status
        {
            get { return status; }
            set { status = value; }
        }

        [DataMember]
        public string StatusName
        {
            get { return getStatus(status); }
            set { statusName = getStatus(value); }
        }

        [DataMember]
        public int Amount
        {
            get { return amount; }
            set { amount = value; }
        }

        private string getStatus(string status)
        {
            switch (status)
            {
                case "N":
                    status = "New";
                    break;
                case "P":
                    status = "In Progress";
                    break;
                case "D":
                    status = "Done";
                    break;
                default:
                    status = string.Empty;
                    break;
            }

            return status;
        }
    }
    #endregion
}