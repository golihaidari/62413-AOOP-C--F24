using DesktopApp.View.MainPages;
using DesktopApp.ViewModels;
using System.Windows;

namespace DesktopApp
{
    /// <summary>
    /// Interaction logic for HomeWindow.xaml
    /// </summary>
    public partial class HomeWindow : Window
    {
        public HomeWindow()
        {
            InitializeComponent();           
            DataContext = new UserVM();
            Main_frame.Navigate(new Products_Page());
        }

        private void BtnOpenMenu_Click(object sender, RoutedEventArgs e)
        {
            BtnOpenMenu.Visibility = Visibility.Collapsed;
            BtnCloseMenu.Visibility = Visibility.Visible;
        }

        private void BtnCloseMenu_Click(object sender, RoutedEventArgs e)
        {
            BtnOpenMenu.Visibility = Visibility.Visible;
            BtnCloseMenu.Visibility = Visibility.Collapsed;
        }        

        private void BtnHome_Click(object sender, RoutedEventArgs e)
        {
            Main_frame.Navigate(new Products_Page());
        }

        private void BtnBasket_Click(object sender, RoutedEventArgs e)
        {
            Main_frame.Navigate(new Basket_Page());
        }

        private void BtnProfile_Click(object sender, RoutedEventArgs e)
        {
            Main_frame.Navigate(new Profile_Page());
        }

        private void BtnOrdersHistory_Click(object sender, RoutedEventArgs e)
        {
            Main_frame.Navigate(new Order_Page());
        }

        private void BtnCloseWindow_Click(object sender, RoutedEventArgs e)
        {
            ((App)Application.Current).AccessToken = string.Empty;
            ((App)Application.Current).UserRole = string.Empty;

            Application.Current.Shutdown();
        }
    }
}
