using IDK.Infrastructure.Models;
using Microsoft.Data.SqlClient;
using System.Data;
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
                SqlConnection con = new SqlConnection("Data Source=DESKTOP-HC94VC5\\SQLEXPRESS01; Initial Catalog = IDK;"
                                                     + " Integrated Security = True;TrustServerCertificate=True");
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
    }
}
