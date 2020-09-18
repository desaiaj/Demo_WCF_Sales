using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

[ServiceContract]
public interface IService
{
    [OperationContract]
    bool AddCustomer(Service.CustomerData objCustomer);

    [OperationContract]
    List<Service.CustomerData> GetCustomers();

    [OperationContract]
    bool UpdateCustomer(Service.CustomerData objCustomer);

    [OperationContract]
    bool DeleteCustomer(int customerID);

    [OperationContract]
    List<Service.OrderData> GetOrders();

    [OperationContract]
    bool AddOrder(Service.OrderData objOrder);
        
    [OperationContract]
    bool UpdateOrder(Service.OrderData objOrder);

    [OperationContract]
    bool DeleteOrder(int orderID);
}