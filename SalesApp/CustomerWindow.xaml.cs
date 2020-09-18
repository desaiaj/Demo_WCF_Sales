//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Windows;
//using System.Windows.Controls;
//using System.Windows.Data;
//using System.Windows.Documents;
//using System.Windows.Input;
//using System.Windows.Media;
//using System.Windows.Media.Imaging;
//using System.Windows.Shapes;
//using SalesApp.ServiceReference;

//namespace SalesApp
//{
//    public partial class CustomerWindow : Window
//    {
//        private static ServiceReference.ServiceClient _serviceClient;
//        public CustomerWindow()
//        {
//            InitializeComponent();
//            _serviceClient = new ServiceClient();
//        }

//        private void btnInsert_Click(object sender, RoutedEventArgs e)
//        {
//            ServiceCustomerData objOustomer = new ServiceCustomerData();
//            objOustomer.CustomerID = Convert.ToInt32(txt_customers_id.Text);
//            objOustomer.CustomerName = txt_customers_name.Text;

//            _serviceClient.AddCustomer(objOustomer);
//        }
//    }
//}
