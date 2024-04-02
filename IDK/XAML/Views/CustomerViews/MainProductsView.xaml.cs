using IDK.Infrastructure.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for MainProductsView.xaml
    /// </summary>
    public partial class MainProductsView : UserControl
    {
        public Product? Product { get; set; }
        public ObservableCollection<Product>? UserProductList { get; set; }
        public MainProductsView()
        {
            InitializeComponent();
            UserProductList = new ObservableCollection<Product>();
            DataContext = this;
            FillProducts();
        }

        public void FillProducts()
        {
            try
            {
                using (SqlConnection con = new("Data Source=DESKTOP-HC94VC5\\SQLEXPRESS01; Initial Catalog = IDK;" +
                                               " Integrated Security = True;TrustServerCertificate=True"))
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

        }
    }
}
