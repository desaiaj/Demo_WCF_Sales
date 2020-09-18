using SalesApp.ServiceReference;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Interaction logic for OrderWindow.xaml
    /// </summary>
    public partial class OrderWindow : Window
    {
        private const string strErrorMsg = "Something went wrong, please try after some time!!!";
        private const string strSuccessMsg = "Transaction successfully completed!!!";
        private static ServiceReference.Service1Client _serviceClient;
        public OrderWindow()
        {
            InitializeComponent();
            hideMsg();
            _serviceClient = new Service1Client();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int custID = Convert.ToInt32(ddl_customer.SelectedValue.ToString());
                DateTime orddate = (DateTime)dt_order_date.SelectedDate;
                DateTime filldate = (DateTime)dt_filled_date.SelectedDate;
                string status = ddl_status.SelectedValue.ToString();
                int amt = Convert.ToInt32(txt_amount.Text);

                bool isSuccess = _serviceClient.AddOrder(custID, orddate, filldate, status, amt);

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
                int OrderID = Convert.ToInt32(txt_Order_id.Text.ToString());
                int custID = Convert.ToInt32(ddl_customer.SelectedValue.ToString());
                DateTime orddate = (DateTime)dt_order_date.SelectedDate;
                DateTime filldate = (DateTime)dt_filled_date.SelectedDate;
                string status = ddl_status.SelectedValue.ToString();
                int amt = Convert.ToInt32(txt_amount.Text);

                bool isSuccess = _serviceClient.UpdateOrder(custID, OrderID, orddate, filldate, status, amt);

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
                bool isSuccess = _serviceClient.DeleteOrder(Convert.ToInt32(txt_Order_id.Text));

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
            txt_Order_id.Text = "";
            ddl_customer.SelectedValue = null;
            dt_filled_date.SelectedDate = null;
            dt_order_date.SelectedDate = null;
            ddl_status.SelectedValue = null;
            txt_amount.Text = null;

            hideMsg();
        }

        private void List_orders_OnLoad(object sender, RoutedEventArgs e)
        {
            ReloadListView();
        }

        private void ddl_customers_OnLoad(object sender, RoutedEventArgs e)
        {
            FillCustomerDdl();
        }

        private void ddl_status_OnLoad(object sender, RoutedEventArgs e)
        {
            FillStatusDdl();
        }

        private void List_orders_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                var objOrder = (Order)list_orders.SelectedItem;

                if (objOrder != null)
                {
                    txt_Order_id.Text = objOrder.OrderID.ToString();
                    ddl_customer.SelectedValue = objOrder.CustomerID;
                    dt_filled_date.SelectedDate = objOrder.FilledDate;
                    dt_order_date.SelectedDate = objOrder.OrderDate;
                    ddl_status.SelectedValue = objOrder.Status;
                    txt_amount.Text = objOrder.Amount.ToString();
                }
            }
            catch
            {
                showErrorMsg();
            }
        }

        private void txt_Order_id_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = new Regex("[^0-9]+").IsMatch(e.Text);
        }

        private void txt_amount_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = new Regex("[^0-9]+").IsMatch(e.Text);
        }

        #region helper
        private void ReloadListView()
        {
            var objOrder = _serviceClient.GetOrders();
            list_orders.ItemsSource = objOrder.ToList();
        }

        private void FillCustomerDdl()
        {
            var objCustomers = _serviceClient.GetCustomers();

            ddl_customer.ItemsSource = objCustomers.Select(x => new { x.CustomerID, x.CustomerName }).ToList();
            ddl_customer.DisplayMemberPath = "CustomerName";
            ddl_customer.SelectedValuePath = "CustomerID";
        }

        private void FillStatusDdl()
        {
            ddl_status.Items.Add(new { StatusName = "New", Value = "N" });
            ddl_status.Items.Add(new { StatusName = "In Progress", Value = "P" });
            ddl_status.Items.Add(new { StatusName = "Filled", Value = "F" });

            ddl_status.DisplayMemberPath = "StatusName";
            ddl_status.SelectedValuePath = "Value";
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

        private void btn_Fulltable_Click(object sender, RoutedEventArgs e)
        {
            new OrdersTableWindow().ShowDialog();
        }
    }
}