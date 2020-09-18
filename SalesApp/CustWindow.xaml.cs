using SalesApp.ServiceReference;
using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace SalesApp
{
    public partial class CustWindow : Window
    {
        private const string strErrorMsg = "Something went wrong, please try after some time!!!";
        private const string strSuccessMsg = "Transaction successfully completed!!!";
        private static ServiceReference.Service1Client _serviceClient;
        public CustWindow()
        {
            InitializeComponent();
            hideMsg();
            _serviceClient = new Service1Client();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string name = txt_customers_name.Text;
                int ytdOrder = Convert.ToInt32(txt_ytd_orders.Text);
                int ytdSales = Convert.ToInt32(txt_ytd_Sales.Text);
                if (name == "")
                {
                    showErrorMsg();
                    return;
                }
                bool isSuccess = _serviceClient.AddCustomer(name, ytdOrder, ytdSales);

                if (isSuccess)
                {
                    ReloadListView();
                    showSuccessMsg();
                }
                else
                {
                    showErrorMsg();
                }
            }
            catch
            {
                showErrorMsg();
            }
        }

        private void Btn_update_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int id = Convert.ToInt32(txt_customers_id.Text);
                string name = txt_customers_name.Text;
                int ytdOrder = Convert.ToInt32(txt_ytd_orders.Text);
                int ytdSales = Convert.ToInt32(txt_ytd_Sales.Text);

                bool isSuccess = _serviceClient.UpdateCustomer(id, name, ytdOrder, ytdSales);

                if (isSuccess)
                {
                    ReloadListView();
                    showSuccessMsg();
                }
                else
                {
                    showErrorMsg();
                }
            }
            catch
            {
                showErrorMsg();
            }
        }

        private void Btn_delete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bool isSuccess = _serviceClient.DeleteCustomer(Convert.ToInt32(txt_customers_id.Text));

                if (isSuccess)
                {
                    ReloadListView();
                    showSuccessMsg();
                }
                else
                {
                    showErrorMsg();
                }
            }
            catch
            {
                showErrorMsg();
            }
        }

        private void Btn_reset_Click(object sender, RoutedEventArgs e)
        {
            txt_customers_id.Text = "";
            txt_customers_name.Text = "";
            txt_ytd_orders.Text = "0";
            txt_ytd_Sales.Text = "0";

            hideMsg();
        }

        private void List_customers_OnLoad(object sender, RoutedEventArgs e)
        {
            ReloadListView();
        }

        private void List_customers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                var objCustomer = (Customer)list_customers.SelectedItem;

                if (objCustomer != null)
                {
                    txt_customers_id.Text = objCustomer.CustomerID.ToString();
                    txt_customers_name.Text = objCustomer.CustomerName;
                    txt_ytd_orders.Text = objCustomer.YTDOrders.ToString();
                    txt_ytd_Sales.Text = objCustomer.YTDSales.ToString();
                }
            }
            catch
            {
                showErrorMsg();
            }
        }

        private void txt_customers_id_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = new Regex("[^0-9]+").IsMatch(e.Text);
        }

        private void txt_ytd_orders_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = new Regex("[^0-9]+").IsMatch(e.Text);
        }

        private void txt_ytd_Sales_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = new Regex("[^0-9]+").IsMatch(e.Text);
        }

        #region helper
        private void ReloadListView()
        {
            var objCustomers = _serviceClient.GetCustomers();

            list_customers.ItemsSource = objCustomers.ToList();
        }

        private void showErrorMsg()
        {
            MessageBox.Show(strErrorMsg, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void showSuccessMsg()
        {
            MessageBox.Show(strSuccessMsg, "Success", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void hideMsg()
        {
            lbl_error_msg.Content = String.Empty;
            lbl_success_msg.Content = String.Empty;
        }
        #endregion
    }
}
