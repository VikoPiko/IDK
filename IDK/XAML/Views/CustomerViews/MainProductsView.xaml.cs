using IDK.Infrastructure.Models;
using Microsoft.Data.SqlClient;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;

namespace IDK.XAML.Views.CustomerViews
{
    /// <summary>
    /// Interaction logic for MainProductsView.xaml
    /// </summary>
    public partial class MainProductsView : UserControl, INotifyPropertyChanged
    {
        private decimal _totalPrice;
        CartViewModel _shoppingCartViewModel;
        int userid;

        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public decimal TotalPrice {
            get { return _totalPrice; }
            set
            {
                _totalPrice = value;
                OnPropertyChanged();
            }
        }
        public Product? Product { get; set; }
        public ObservableCollection<Product>? UserProductList { get; set; }


        public MainProductsView(int userID)
        {
            InitializeComponent();
            UserProductList = new ObservableCollection<Product>();
            List<Product> productsInCart = new();
            DataContext = this;
            userid = userID;
            FillProducts();
        }

        public void FillProducts()
        {
            try
            {
                //Desktop
                /*using (SqlConnection con = new("Data Source=DESKTOP-HC94VC5\\SQLEXPRESS01; Initial Catalog = IDK;" +
                                               " Integrated Security = True;TrustServerCertificate=True"))*/
                //Laptop
                SqlConnection con = new SqlConnection("Data Source=DESKTOP-87GDKF5\\SQLEXPRESS; Initial Catalog = IDK;" +
                    " Integrated Security = True;TrustServerCertificate=True");
                {
                    con.Open();
                    SqlCommand cmd = new("Select * from [Products]", con);
                    SqlDataAdapter adapter = new(cmd);
                    DataSet ds = new DataSet();
                    adapter.Fill(ds, "[Products]");

                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        UserProductList?.Add(new Product
                        {
                            Id = Convert.ToInt32(dr[0].ToString()),
                            Name = dr[1].ToString(),
                            Price = Convert.ToDecimal(dr[2].ToString()),
                            Quantity = Convert.ToInt32(dr[3].ToString())
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        private void AddToCart_Click(object sender, RoutedEventArgs e)
        {

            Button clickedButton = sender as Button;
            // Get the corresponding Product object bound to the clicked button
            Product selectedProduct = clickedButton.DataContext as Product;
            // Add selected product to the global list
            _shoppingCartViewModel.AddToCart(selectedProduct);
            TotalPrice += selectedProduct.Price;
            MessageBox.Show($"Product: {selectedProduct.Name} was added to the cart.");
        }
    }
}
