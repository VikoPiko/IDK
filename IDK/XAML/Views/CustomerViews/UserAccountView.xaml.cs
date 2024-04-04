using IDK.Infrastructure.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace IDK.XAML.Views.CustomerViews
{
    /// <summary>
    /// Interaction logic for UserAccountView.xaml
    /// </summary>
    public partial class UserAccountView : UserControl
    {
        private MainWindow mainWindow;
        int userid;
        public UserAccountView(MainWindow mainWindow, int UserId)
        {
            InitializeComponent();
            this.mainWindow = mainWindow;
            this.userid = UserId;
            FillCustomerOrders();

        }
        private void UserLogout_Click(object sender, RoutedEventArgs e)
        {
            CustomerLogin cl = new ();
            cl.Show();
            mainWindow.Close();
        }

        public void FillCustomerOrders()
        {
            //Laptop
            SqlConnection con = new SqlConnection("Data Source=DESKTOP-87GDKF5\\SQLEXPRESS; Initial Catalog = IDK;" +
             " Integrated Security = True;TrustServerCertificate=True");

            //Dekstop
            /*SqlConnection con = new SqlConnection("Data Source=DESKTOP-HC94VC5\\SQLEXPRESS01; Initial Catalog = IDK;" +
               " Integrated Security = True;TrustServerCertificate=True");*/

            con.Open();

            SqlCommand cmd = new("Select * from [Orders] where CustomerId = @id", con);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@id", userid);
            List<Order> CustomerOrders = new();
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
                        CustomerOrders.Add(order);
                    }
                    CustomerOrderList.ItemsSource = CustomerOrders;
                }
                con.Close();
            }
        }
    }
}
