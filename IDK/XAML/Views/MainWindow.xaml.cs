using IDK.Infrastructure.Models;
using IDK.XAML.Views.CustomerViews;
using Microsoft.Data.SqlClient;
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
    private CategoriesView _categoriesView;
    private UserAccountView _userAccountView;
    private MainProductsView _mainProductsView;
    public MainWindow()
    {
        InitializeComponent();
        DataContext = this;
        _mainProductsView = new MainProductsView();
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

    private void Categories_Click(object sender, RoutedEventArgs e)
    {
        if(_categoriesView == null)
        {
            _categoriesView = new CategoriesView();
        }
        CurrentChildView = _categoriesView;
    }

    private void Cart_Click(object sender, RoutedEventArgs e)
    {
        if(_shoppingCartView == null)
        {
            _shoppingCartView = new ShoppingCartView();
        }
        CurrentChildView = _shoppingCartView;
    }

    private void UserAccount_Click(object sender, RoutedEventArgs e)
    {
        if(_userAccountView == null)
        {
            _userAccountView = new UserAccountView();
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
            _mainProductsView = new MainProductsView();
        }
        CurrentChildView = _mainProductsView;
    }
    private void QueryBtn_Click(object sender, RoutedEventArgs e)
    {
  /*      string str = CustomerSearchBar.Text;
        string failed = "No matching Records found";
        //Laptop
        *//*SqlConnection con = new SqlConnection("Data Source=DESKTOP-87GDKF5\\SQLEXPRESS; Initial Catalog = IDK;" +
            " Integrated Security = True;TrustServerCertificate=True");*//*

        //Dekstop
        SqlConnection con = new SqlConnection("Data Source=DESKTOP-HC94VC5\\SQLEXPRESS01; Initial Catalog = IDK;" +
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
            CustomerSearchBar.Text = "";
        }
        con.Close();*/
    }
}