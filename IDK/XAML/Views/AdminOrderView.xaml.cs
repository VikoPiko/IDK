using IDK.Infrastructure.Models;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interop;

namespace IDK.XAML.Views
{
    public partial class AdminOrderView : Window
    {
        public AdminOrderView()
        {
            InitializeComponent();
            FillOrders();
        }

        public void FillOrders()
        {
            try
            {
                //usually should close all running processes, but doesnt matter rn this is just for homework
                //Desktop
                /*SqlConnection con = new SqlConnection("Data Source=DESKTOP-HC94VC5\\SQLEXPRESS01; Initial Catalog = IDK;"
                                                     + " Integrated Security = True;TrustServerCertificate=True");*/
                //Laptop
                SqlConnection con = new SqlConnection("Data Source=DESKTOP-87GDKF5\\SQLEXPRESS; Initial Catalog = IDK;" +
                " Integrated Security = True;TrustServerCertificate=True");
                con.Open();
                SqlCommand cmd = new SqlCommand("Select * from [Orders]", con);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adapter.Fill(ds, "[Orders]");
                Order order = new Order();
                IList<Order> orders = new List<Order>();

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    orders.Add(new Order
                    {
                        Id = Convert.ToInt32(dr[0].ToString()),
                        OrderPlaced = Convert.ToDateTime(dr[1].ToString()),
                        OrderFulfilled = Convert.ToDateTime(dr[2].ToString()),
                        CustomerId = 1,
                        TotalPrice = Convert.ToDecimal(dr[4].ToString()),
                        IsComplete = Convert.ToBoolean(dr[5].ToString())
 /*                        TotalPrice = Convert.ToDecimal(dr[6].ToString()),
                        IsComplete = Convert.ToBoolean(dr[7].ToString())*/
                    });
                }
                OrdersList.ItemsSource = orders;
                con.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
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

        private void IsOrderCompleteCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            if(IsNotComplete.IsChecked == true)
            {
                IsNotComplete.IsChecked = false;
            }
            else if(IsNotComplete.IsChecked == false)
            {
                //Laptop
                SqlConnection con = new SqlConnection("Data Source=DESKTOP-87GDKF5\\SQLEXPRESS; Initial Catalog = IDK;" +
                 " Integrated Security = True;TrustServerCertificate=True");

                //Dekstop
                /*SqlConnection con = new SqlConnection("Data Source=DESKTOP-HC94VC5\\SQLEXPRESS01; Initial Catalog = IDK;" +
                   " Integrated Security = True;TrustServerCertificate=True");*/

                con.Open();

                SqlCommand cmd = new("Select * from [Orders] where IsComplete = 1", con);
                cmd.CommandType = CommandType.Text;
                List<Order> ordersChecked = new List<Order>();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            Order order = new Order()
                            {
                                Id = Convert.ToInt32(reader["Id"]),
                                OrderPlaced = Convert.ToDateTime(reader["OrderPlaced"]),
                                OrderFulfilled = Convert.ToDateTime(reader["OrderFulfilled"]),
                                CustomerId = Convert.ToInt32(reader["CustomerId"]),
                                TotalPrice = Convert.ToDecimal(reader["TotalPrice"]),
                                IsComplete = Convert.ToBoolean(reader["IsComplete"])
                            };
                            ordersChecked.Add(order);
                        }
                        OrdersList.ItemsSource = ordersChecked;
                    }
                    con.Close();
                }
            }
        }
        private void IsOrderCompleteCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            FillOrders();
        }

        private void IsNotComplete_Checked(object sender, RoutedEventArgs e)
        {
            if(IsOrderCompleteCheckBox.IsChecked == true) 
            {
                IsOrderCompleteCheckBox.IsChecked = false;
            }
            else if(IsOrderCompleteCheckBox.IsChecked == false)
            {
                //Laptop
                SqlConnection con = new SqlConnection("Data Source=DESKTOP-87GDKF5\\SQLEXPRESS; Initial Catalog = IDK;" +
                 " Integrated Security = True;TrustServerCertificate=True");

                //Dekstop
                /*SqlConnection con = new SqlConnection("Data Source=DESKTOP-HC94VC5\\SQLEXPRESS01; Initial Catalog = IDK;" +
                   " Integrated Security = True;TrustServerCertificate=True");*/

                con.Open();

                SqlCommand cmd = new("Select * from [Orders] where IsComplete = 0", con);
                cmd.CommandType = CommandType.Text;
                List<Order> ordersChecked = new List<Order>();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            Order order = new Order()
                            {
                                Id = Convert.ToInt32(reader["Id"]),
                                OrderPlaced = Convert.ToDateTime(reader["OrderPlaced"]),
                                OrderFulfilled = Convert.ToDateTime(reader["OrderFulfilled"]),
                                CustomerId = Convert.ToInt32(reader["CustomerId"]),
                                TotalPrice = Convert.ToDecimal(reader["TotalPrice"]),
                                IsComplete = Convert.ToBoolean(reader["IsComplete"])
                            };
                            ordersChecked.Add(order);
                        }
                        OrdersList.ItemsSource = ordersChecked;
                    }
                    con.Close();
                }
            }
        }

        private void IsNotComplete_Unchecked(object sender, RoutedEventArgs e)
        {
            FillOrders();
        }
    }
}
