using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace SalesServices
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IService1
    {

        [OperationContract]
        string GetData(int value);

        [OperationContract]
        CompositeType GetDataUsingDataContract(CompositeType composite);

        // TODO: Add your service operations here

        [OperationContract]
        bool AddCustomer(string custName, int ytdOrder, int ytdsales);

        [OperationContract]
        List<Customer> GetCustomers();

        [OperationContract]
        bool UpdateCustomer(int id, string custName, int ytdOrder, int ytdsales);

        [OperationContract]
        bool DeleteCustomer(int customerID);

        [OperationContract]
        List<Order> GetOrders();

        // Orders contract

        [OperationContract]
        bool AddOrder(int customerID, System.DateTime orderDate, System.DateTime fillDate, string status, int amount);

        [OperationContract]
        bool UpdateOrder(int customerID, int orderID, System.DateTime orderDate, System.DateTime fillDate, string status, int amount);

        [OperationContract]
        bool DeleteOrder(int orderID);


    }


    // Use a data contract as illustrated in the sample below to add composite types to service operations.
    [DataContract]
    public class CompositeType
    {
        bool boolValue = true;
        string stringValue = "Hello ";

        [DataMember]
        public bool BoolValue
        {
            get { return boolValue; }
            set { boolValue = value; }
        }

        [DataMember]
        public string StringValue
        {
            get { return stringValue; }
            set { stringValue = value; }
        }
    }
}
