using CourseDB;
using IDK;
using IDK.XAML.Views;
using System.Windows;

namespace IDK
{
    public partial class App : Application
    {
        /*protected void ApplicationStart(object sender, EventArgs e)
        {
            var customerWindow = new CustomerLogin();
            customerWindow.Show();
            customerWindow.IsVisibleChanged += (s, ev) =>
           {
               if (customerWindow.IsVisible == false && customerWindow.IsLoaded)
               {
                   var mainWindow = new MainWindow();
                   mainWindow.Show();
                   customerWindow.Close();
               }
           };
        }*/
    }

}
