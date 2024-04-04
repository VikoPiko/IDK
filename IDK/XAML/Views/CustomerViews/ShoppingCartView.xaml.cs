using IDK.Infrastructure.Models;
using Microsoft.Data.SqlClient;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
namespace IDK.XAML.Views.CustomerViews
{
    /// <summary>
    /// Interaction logic for ShoppingCartView.xaml
    /// </summary>
    public partial class ShoppingCartView : UserControl
    {
        CartViewModel cartViewModel;
        int usercartid;

        public ShoppingCartView(int USID)
        {
            InitializeComponent();
            usercartid = USID;
            cartViewModel = new ();
            DataContext = this;
            FillShoppingCart();
        }

        public void FillShoppingCart()
        {
            SqlConnection con = new ("Data Source=DESKTOP-87GDKF5\\SQLEXPRESS; Initial Catalog = IDK;" +
                                                  " Integrated Security = True;TrustServerCertificate=True");
            con.Open();

            SqlCommand sqlCommand = new ("Select * from [Carts] where userId = @userID", con);
            sqlCommand.Parameters.AddWithValue("@userID", usercartid);
            List<Cart> Products = new();
            using (SqlDataReader reader = sqlCommand.ExecuteReader()) 
            {
                if(reader.HasRows)
                {
                    while (reader.Read()) 
                    {
                        Cart cart = new Cart()
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            userId = Convert.ToInt32(reader["userId"]),
                            ProductId = Convert.ToInt32(reader["ProductId"]),
                            ProductName = reader["ProductName"].ToString(),
                            Price = Convert.ToDecimal(reader["Price"]),
                            Quantity = Convert.ToInt32(reader["Quantity"])
                        };
                        Products.Add(cart);
                    }
                    ShoppingCartList.ItemsSource = Products;
                }
            }
        }

        private void RefreshLMAO_Click(object sender, RoutedEventArgs e)
        {
            FillShoppingCart();
        }
    }
}
