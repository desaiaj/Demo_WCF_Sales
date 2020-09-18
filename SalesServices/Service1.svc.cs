using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace SalesServices
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class Service1 : IService1
    {
        private static SalesEntities _context;
        public Service1()
        {
            _context = new SalesEntities();
        }
        public bool AddCustomer(string custName, int ytdOrder, int ytdsales)
        {
            try
            {
                Customer customer = new Customer();

                customer.CustomerName = custName;
                customer.YTDOrders = ytdOrder;
                customer.YTDSales = ytdsales;

                _context.Customers.Add(customer);
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                return false;
            }

            return true;
        }

        public bool AddOrder(int customerID, System.DateTime orderDate, System.DateTime fillDate, string status, int amount)
        {
            try
            {
                Order order = new Order();

                order.CustomerID = customerID;
                order.OrderDate = orderDate;
                order.FilledDate = fillDate;
                order.Status = status.ElementAt(0).ToString();
                order.Amount = amount;

                _context.Orders.Add(order);
                _context.SaveChanges();
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

        public List<Customer> GetCustomers()
        {
            return _context.Customers.ToList();
        }

        public string GetData(int value)
        {
            return string.Format("You entered: {0}", value);
        }

        public CompositeType GetDataUsingDataContract(CompositeType composite)
        {
            if (composite == null)
            {
                throw new ArgumentNullException("composite");
            }
            if (composite.BoolValue)
            {
                composite.StringValue += "Suffix";
            }
            return composite;
        }

        public List<Order> GetOrders()
        {
            return _context.Orders.ToList();
        }

        public bool UpdateCustomer(int CustomerID, string custName, int ytdOrder, int ytdsales)
        {
            try
            {
                using (SalesEntities _context = new SalesEntities())
                {
                    var customer = _context.Customers.Where(x => x.CustomerID == CustomerID).FirstOrDefault();

                    if (customer != null)
                    {
                        //customer.CustomerID = objCustomer.CustomerID;

                        customer.CustomerName = custName;
                        customer.YTDOrders = ytdOrder;
                        customer.YTDSales = ytdsales;

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

        public bool UpdateOrder(int customerID, int orderID, System.DateTime orderDate, System.DateTime fillDate, string status, int amount)
        {
            try
            {
                using (SalesEntities _context = new SalesEntities())
                {
                    var order = _context.Orders.Where(x => x.OrderID == orderID).FirstOrDefault();

                    if (order != null)
                    {
                        order.CustomerID = customerID;
                        order.OrderDate = orderDate;
                        order.FilledDate = fillDate;
                        order.Status = status.ElementAt(0).ToString();
                        order.Amount = amount;

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
    }
}
