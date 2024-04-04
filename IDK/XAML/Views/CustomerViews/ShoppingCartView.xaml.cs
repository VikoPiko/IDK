using IDK.Infrastructure.Models;
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
        public ObservableCollection<Product> Products { get; set; }

     
        public ShoppingCartView()
        {
            InitializeComponent();
            Products = new ();
            DataContext = this;
        }

        public void FillShoppingCart()
        {
            
        }
    }
}
