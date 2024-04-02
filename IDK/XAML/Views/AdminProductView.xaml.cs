using IDK.Infrastructure.Models;
using Microsoft.Data.SqlClient;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interop;

namespace IDK.XAML.Views
{
    public partial class AdminProductView : Window, INotifyPropertyChanged
    {
        private ObservableCollection<Product> _products = new();
        private Product _selectedProduct;

        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public ObservableCollection<Product> Products
        {
            get { return _products; }
            set { _products = value; OnPropertyChanged(); }
        }
        public Product SelectedProduct
        {
            get { return _selectedProduct; }
            set { _selectedProduct = value; OnPropertyChanged(); }
        }

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
                /*SqlConnection con = new SqlConnection("Data Source=DESKTOP-87GDKF5\\SQLEXPRESS; Initial Catalog = IDK;" +
                " Integrated Security = True;TrustServerCertificate=True");*/

                //Desktop Connection str;
                SqlConnection con = new ("Data Source=DESKTOP-HC94VC5\\SQLEXPRESS01; Initial Catalog = IDK;" +
                " Integrated Security = True;TrustServerCertificate=True");
                con.Open();
                SqlCommand cmd = new ("Select * from [Products]", con);
                SqlDataAdapter adapter = new (cmd);
                DataSet ds = new DataSet();
                adapter.Fill(ds, "[Products]");
                Product product = new ();
                IList<Product> products = new ObservableCollection<Product>();

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    products.Add(new Product
                    {
                        Id = Convert.ToInt32(dr[0].ToString()),
                        Name = dr[1].ToString(),
                        Price = Convert.ToDecimal(dr[2].ToString()),
                        Quantity = Convert.ToInt32(dr[3].ToString())
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
            if (Product_Name.Text != "" && Product_Price.Text != "")
            {
                //Dekstop
                SqlConnection con = new ("Data Source=DESKTOP-HC94VC5\\SQLEXPRESS01; Initial Catalog = IDK;" +
                   " Integrated Security = True;TrustServerCertificate=True");
                //Laptop
                /*SqlConnection con = new SqlConnection("Data Source=DESKTOP-87GDKF5\\SQLEXPRESS; Initial Catalog = IDK;" +
                    " Integrated Security = True;TrustServerCertificate=True");*/
                con.Open();

                string productName = Product_Name.Text;
                decimal productPrice = decimal.Parse(Product_Price.Text);
                int quantity = Convert.ToInt32(Product_Quantity.Text);

                string query = "Insert into [Products] (Name, Price, Quantity) values (@Name, @Price, @Quantity)";
                SqlCommand cmd = new(query, con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@Name", productName);
                cmd.Parameters.AddWithValue("@Price", productPrice);
                cmd.Parameters.AddWithValue("@Quantity", quantity);
                cmd.ExecuteScalar();

                Product_Name.Text = "";
                Product_Price.Text = "";
                Product_Quantity.Text = "";

                con.Close();
                FillProducts();
            }
            else
            {
                string error = "Product is missing a Name/Price !";
                MessageBox.Show(error);
            }
        }

        private void RemoveItem_Click(object sender, RoutedEventArgs e)
        {
            if (ProductsList.SelectedItem != null)
            {
                string name = ((Product)ProductsList.SelectedItem).Name;

                // Desktop
                SqlConnection con = new ("Data Source=DESKTOP-HC94VC5\\SQLEXPRESS01; Initial Catalog = IDK;" +
                      " Integrated Security = True;TrustServerCertificate=True");

                con.Open();

                SqlCommand cmd = new ("Delete from [Products] where Name = @name", con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@name", name);
                cmd.ExecuteNonQuery();
                ProductsList.SelectedItem = null;

                con.Close();
                FillProducts();
            }
        }

        private void ProductSearchBtn_Click(object sender, RoutedEventArgs e)
        {
            //Laptop
            /*SqlConnection con = new SqlConnection("Data Source=DESKTOP-87GDKF5\\SQLEXPRESS; Initial Catalog = IDK;" +
             " Integrated Security = True;TrustServerCertificate=True");*/

            //Dekstop
            SqlConnection con = new ("Data Source=DESKTOP-HC94VC5\\SQLEXPRESS01; Initial Catalog = IDK;" +
               " Integrated Security = True;TrustServerCertificate=True");

            con.Open();

            string str = ProductSearchBar.Text;
            string failed = "No Products to list";

            SqlCommand cmd = new("Select * from [Products] where Name Like @str or Price Like @str", con);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@str", '%' + str + '%');
            List<Product> products = new();
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                if(reader.HasRows) 
                {
                    while(reader.Read()) 
                    {
                        Product product = new()
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            Name = Convert.ToString(reader["Name"]),
                            Price = Convert.ToDecimal(reader["Price"]),
                            Quantity = Convert.ToInt32(reader["Quantity"])
                        };
                        products.Add(product);
                    }
                    ProductsList.ItemsSource = products;
                }
                else
                {
                    MessageBox.Show(failed);
                }
                con.Close();
            }
            ProductSearchBar.Text = "";
        }
    }
}
