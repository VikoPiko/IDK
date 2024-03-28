using CourseDB;
using Microsoft.Data.SqlClient;
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
    /// <summary>
    /// Interaction logic for AdminLogin.xaml
    /// </summary>
    public partial class AdminLogin : Window
    {
        public AdminLogin()
        {
            InitializeComponent();
        }
        private void AdminLogin_Click(object sender, RoutedEventArgs e)
        {
            bool validUser = false;
            string usernameA = AdminUsername.Text;
            string passwordA = AdminPassword.Password;
            SqlConnection con = new SqlConnection("Data Source=DESKTOP-87GDKF5\\SQLEXPRESS; Initial Catalog = IDK;" +
                " Integrated Security = True;TrustServerCertificate=True");
            try
            {
                if (con.State == System.Data.ConnectionState.Closed)
                {
                    con.Open();
                }
                string query = "Select count(1) from [Admins] where Username = @Username and Password = @Password";
                SqlCommand sqlCommand = new SqlCommand(query, con);
                sqlCommand.CommandType = System.Data.CommandType.Text;
                sqlCommand.Parameters.AddWithValue("@Username", usernameA);
                sqlCommand.Parameters.AddWithValue("@Password", passwordA);
                validUser = sqlCommand.ExecuteScalar() == null ? false : true;
                if (validUser)
                {
                    AdminControl adminControl = new ();
                    adminControl.Show();
                    this.Close();
                }
                else
                {
                    string title = "ERROR";
                    string error = "INVALID LOGIN";
                    MessageBox.Show(error, title);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            finally { con.Close(); }
        }

        private void UserWindow_Click(object sender, RoutedEventArgs e)
        {
            CustomerLogin customerLogin = new CustomerLogin();
            customerLogin.Show();
            this.Close();
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
            Application.Current.Shutdown();
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
    }
}
