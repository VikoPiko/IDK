using IDK.Infrastructure.Models;
using IDK.XAML.Views.CustomerViews;
using Microsoft.Data.SqlClient;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interop;

namespace IDK.XAML.Views;

public partial class MainWindow : Window, INotifyPropertyChanged
{
    private ShoppingCartView _shoppingCartView;
    private UserAccountView _userAccountView;
    private MainProductsView _mainProductsView;
    int UID;
    public string UNAME {  get; set; }

    public Product? Product { get; set; }
    public ObservableCollection<Product>? UserProductList { get; set; }

    public MainWindow(int userId, string username)
    {
        InitializeComponent();
        DataContext = this;
        UID = userId;
        UNAME = username;
        _mainProductsView = new MainProductsView(userId);
        UserProductList = new();
        CurrentChildView = _mainProductsView;
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    public void OnPropertyChanged([CallerMemberName] string name = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
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
        Application.Current.Shutdown();
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


    private void Cart_Click(object sender, RoutedEventArgs e)
    {
        if(_shoppingCartView == null)
        {
            _shoppingCartView = new ShoppingCartView(UID);
        }
        CurrentChildView = _shoppingCartView;
    }

    private void UserAccount_Click(object sender, RoutedEventArgs e)
    {
        if(_userAccountView == null)
        {
            _userAccountView = new UserAccountView(this, UID);
        }
        CurrentChildView = _userAccountView;
    }

    private UserControl _currentChildView;
    public UserControl CurrentChildView
    {
        get { return _currentChildView; }
        set
        {
            _currentChildView = value;
            OnPropertyChanged(nameof(CurrentChildView));
        }
    }

    private void Main_Click(object sender, RoutedEventArgs e)
    {
        if(_mainProductsView == null) 
        {
            _mainProductsView = new MainProductsView(UID);
        }
        CurrentChildView = _mainProductsView;
    }
    private void QueryBtn_Click(object sender, RoutedEventArgs e)
    {
        //Dekstop
        /*SqlConnection con = new ("Data Source=DESKTOP-HC94VC5\\SQLEXPRESS01; Initial Catalog = IDK;" +
           " Integrated Security = True;TrustServerCertificate=True");*/
        //Laptop
        SqlConnection con = new SqlConnection("Data Source=DESKTOP-87GDKF5\\SQLEXPRESS; Initial Catalog = IDK;" +
            " Integrated Security = True;TrustServerCertificate=True");

        con.Open();

        string str = CustomerSearchBar.Text;
        string failed = "No Products to list";

        SqlCommand cmd = new("Select * from [Products] where Name Like @str or Price Like @str", con);
        cmd.CommandType = CommandType.Text;
        cmd.Parameters.AddWithValue("@str", '%' + str + '%');
        List<Product> products = new();
        using (SqlDataReader reader = cmd.ExecuteReader())
        {
            if (reader.HasRows)
            {
                while (reader.Read())
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
                _mainProductsView.UserProducts.ItemsSource = products;
            }
            else
            {
                MessageBox.Show(failed);
            }
            con.Close();
        }
        CustomerSearchBar.Text = "";
        CurrentChildView = _mainProductsView;
    }
}