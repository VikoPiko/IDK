using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interop;
using CourseDB;
using IDK;
using Microsoft.Data.SqlClient;

namespace IDK.XAML.Views;

public partial class CustomerLogin : Window
{
    public CustomerLogin()
    {
        InitializeComponent();
    }

    private void btnLogin_Click(object sender, RoutedEventArgs e)
    {
        bool validUser = false;
        string username = UsernameU.Text;
        string password = PasswordU.Password;
        SqlConnection con = new SqlConnection("Data Source=DESKTOP-87GDKF5\\SQLEXPRESS; Initial Catalog = IDK;" +
            " Integrated Security = True;TrustServerCertificate=True");
        try
        {
            if (con.State == System.Data.ConnectionState.Closed)
            {
                con.Open();
            }
            string query = "Select * from [Users] where Username = @Username and Password = @Password";
            SqlCommand sqlCommand = new SqlCommand(query, con);
            sqlCommand.CommandType = System.Data.CommandType.Text;
            sqlCommand.Parameters.AddWithValue("@Username", username);
            sqlCommand.Parameters.AddWithValue("@Password", password);
            validUser = sqlCommand.ExecuteScalar() == null ? false : true;
            if (validUser)
            {
                MainWindow main = new MainWindow();
                main.Show();
                this.Close();
            }
            else
            {
                Console.WriteLine("Invalid Login");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
        finally { con.Close(); }
    }

    private void AdminWindow_Click(object sender, RoutedEventArgs e)
    {
        AdminLogin adminLoginView = new AdminLogin();
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
}
