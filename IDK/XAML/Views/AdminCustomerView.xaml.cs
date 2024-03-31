using IDK.Infrastructure.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
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
    /// Interaction logic for AdminCustomerView.xaml
    /// </summary>
    public partial class AdminCustomerView : Window
    {
        public AdminCustomerView()
        {
            InitializeComponent();
            FillCustomers();
        }

        public void FillCustomers()
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
                SqlCommand cmd = new SqlCommand("Select * from [Customers]", con);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adapter.Fill(ds, "[Customers]");
                Customer customer = new Customer();
                IList<Customer> customers = new List<Customer>();

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    customers.Add(new Customer()
                    {
                        Id = Convert.ToInt32(dr[0].ToString()),
                        Name = dr[1].ToString(),
                        Address = dr[2].ToString(),
                        Email = dr[3].ToString(),
                    });

                }
                CustomerList.ItemsSource = customers;
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

        private void QueryBtn_Click(object sender, RoutedEventArgs e)
        {
            string str = SearchQueryBox.Text;
            string failed = "No matching Records found";
            SqlConnection con = new SqlConnection("Data Source=DESKTOP-87GDKF5\\SQLEXPRESS; Initial Catalog = IDK;" +
                " Integrated Security = True;TrustServerCertificate=True");
            con.Open();
            SqlCommand cmd = new SqlCommand("Select * from [Customers] where Name Like @str or Id Like @str or " +
                "Address Like @str or Email Like @str", con);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@str", '%' + str + '%');

            List<Customer> customers = new List<Customer>();

            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Customer customer = new Customer()
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            Name = Convert.ToString(reader["Name"]),
                            Email = Convert.ToString(reader["Email"]),
                            Address = Convert.ToString(reader["Address"])
                        };
                        customers.Add(customer);
                    }
                    CustomerList.ItemsSource = customers;
                }
                else
                {
                    MessageBox.Show(failed);
                }
            }
            con.Close();
        }

    }
}
