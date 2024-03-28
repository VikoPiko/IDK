using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace IDK.XAML.Views
{
    public partial class AdminControl : Window
    {
        bool pwavIsOpened = false, aowOpened = false, acwOpened = false;
        
        private AdminProductView pwavInstance;
        private AdminOrderView aowInstance;
        private AdminCustomerView acwInstance;
        public AdminControl()
        {
            InitializeComponent();
        }
        [DllImport("user32.dll")]
        public static extern IntPtr SendMessage(IntPtr hWnd, int wMsg, int wParam, int lParam);
        private void pnlControlBar_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            WindowInteropHelper helper = new WindowInteropHelper(this);
            SendMessage(helper.Handle, 161, 2, 0);
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnMaximize_Click(object sender, RoutedEventArgs e)
        {
            if (this.WindowState == WindowState.Normal)
                this.WindowState = WindowState.Maximized;
            else
                this.WindowState = WindowState.Normal;
        }

        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }
        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void Products_Click(object sender, RoutedEventArgs e)
        {
            if (!pwavIsOpened)
            {
                pwavInstance = new AdminProductView();
                pwavInstance.Closed += (s, args) => pwavIsOpened = false;
                pwavInstance.Show();
                pwavIsOpened = true;
            }
            else
            {
                pwavInstance.Focus();
            }
        }

        private void Orders_Click(object sender, RoutedEventArgs e)
        {
            if (!aowOpened)
            {
                aowInstance = new AdminOrderView();
                aowInstance.Closed += (s, args) => aowOpened = false;
                aowInstance.Show();
                aowOpened = true;
            }
            else
            {
                aowInstance.Focus();
            }
        }

        private void Customers_Click(object sender, RoutedEventArgs e)
        {
            if (!acwOpened)
            {
                acwInstance = new AdminCustomerView();
                acwInstance.Closed += (s, args) => acwOpened = false;
                acwInstance.Show();
                acwOpened = true;
            }
            else
            {
                acwInstance.Focus();
            }
        }
    }
}
