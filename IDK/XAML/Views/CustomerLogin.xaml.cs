using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interop;
using IDK.XAML.Views.CustomerViews;
using Microsoft.Data.SqlClient;

namespace IDK.XAML.Views;

public partial class CustomerLogin : Window
{
    public CustomerLogin()
    {
        InitializeComponent();
    }

    private void AdminWindow_Click(object sender, RoutedEventArgs e)
    {
        AdminLogin adminLoginView = new ();
        adminLoginView.Show();
        this.Close();
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

    private void Customer_Login_Click(object sender, RoutedEventArgs e)
    {
        int userId = -1;
        bool validUser = false;
        string username = UsernameU.Text;
        string password = PasswordU.Password;
        //Desktop Login
        /*SqlConnection con = new("Data Source=DESKTOP-HC94VC5\\SQLEXPRESS01; Initial Catalog = IDK;" +
            " Integrated Security = True;TrustServerCertificate=True");*/
        //Laptop login
        SqlConnection con = new SqlConnection("Data Source=DESKTOP-87GDKF5\\SQLEXPRESS; Initial Catalog = IDK;" +
                " Integrated Security = True;TrustServerCertificate=True");

        try
        {
            con.Open();
            string query = "select * from [Customers] where Name = @Username and Password = @Password";
            SqlCommand cmd = new(query, con);
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.Parameters.AddWithValue("@Username", username);
            cmd.Parameters.AddWithValue("@Password", password);
            validUser = cmd.ExecuteScalar() == null ? false : true;
            if(validUser)
            {
                userId = Convert.ToInt32(cmd.ExecuteScalar() ?? -1);
                MainWindow main = new (userId, username);
                main.Show();
                Application.Current.Properties["UserID"] = userId;
                this.Close();
            }
        }
        catch(Exception ex)
        {
            MessageBox.Show(ex.ToString());
        }
        finally
        {
            con.Close();
        }
    }
}
