using SalesApp.ServiceReference;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SalesApp
{
    /// <summary>
    /// Interaction logic for OrdersTableWindow.xaml
    /// </summary>
    public partial class OrdersTableWindow : Window
    {
        private static ServiceReference.Service1Client _serviceClient;
        public OrdersTableWindow()
        {
            InitializeComponent();
            _serviceClient = new Service1Client();
        }

        private void grdOrders_Loaded(object sender, RoutedEventArgs e)
        {
            var objOrder = _serviceClient.GetOrders();
            var orderedlist = from o in objOrder
                              orderby o.OrderDate descending
                              select o;
            grdOrders.ItemsSource = orderedlist.ToList();
        }
    }
}
