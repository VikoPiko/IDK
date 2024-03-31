using IDK.Infrastructure.Models;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interop;

namespace IDK.XAML.Views
{
    public partial class AdminProductView : Window
    {
        public AdminProductView()
        {
            InitializeComponent();
            FillProducts();
        }
        public void FillProducts()
        {
            try
            {
                //usually should close all running processes, but doesnt matter rn this is just for homework

                //Laptop Connection str;
                SqlConnection con = new SqlConnection("Data Source=DESKTOP-87GDKF5\\SQLEXPRESS; Initial Catalog = IDK;" +
                " Integrated Security = True;TrustServerCertificate=True");

                //Desktop Connection str;
                /* SqlConnection con = new SqlConnection("Data Source=DESKTOP-HC94VC5\\SQLEXPRESS01; Initial Catalog = IDK;" +
                 " Integrated Security = True;TrustServerCertificate=True");*/
                con.Open();
                SqlCommand cmd = new SqlCommand("Select * from [Products]", con);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adapter.Fill(ds, "[Products]");
                Product product = new Product();
                IList<Product> products = new List<Product>();

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    products.Add(new Product
                    {
                        Id = Convert.ToInt32(dr[0].ToString()),
                        Name = dr[1].ToString(),
                        Price = Convert.ToDecimal(dr[2].ToString())
                    });
                }
                ProductsList.ItemsSource = products;
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

        private void AddItem_Click(object sender, RoutedEventArgs e)
        {
            //Dekstop
            /*SqlConnection con = new SqlConnection("Data Source=DESKTOP-HC94VC5\\SQLEXPRESS01; Initial Catalog = IDK;" +
               " Integrated Security = True;TrustServerCertificate=True");*/
            //Laptop
            SqlConnection con = new SqlConnection("Data Source=DESKTOP-87GDKF5\\SQLEXPRESS; Initial Catalog = IDK;" +
                " Integrated Security = True;TrustServerCertificate=True");
            con.Open();

            string productName = Product_Name.Text;
            decimal productPrice = decimal.Parse(Product_Price.Text);

            string query = "Insert into [Products] (Name, Price) values (@Name, @Price)";
            SqlCommand cmd = new(query, con);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@Name", productName);
            cmd.Parameters.AddWithValue("@Price", productPrice);
            cmd.ExecuteScalar();

            con.Close();
        }

        private void RemoveItem_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
