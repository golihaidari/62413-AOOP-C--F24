using DesktopApp.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace DesktopApp.View.WelcomePages
{
    /// <summary>
    /// Interaction logic for Login_Page.xaml
    /// </summary>
    public partial class Login_Page : Page
    {
        public Login_Page()
        {
            InitializeComponent();
            DataContext= new UserVM();
        }

   
        private void navigateToSignUp_click(object sender, RoutedEventArgs e)
        {
            var frame = (Frame)Application.Current.MainWindow.FindName("Index_frame");
            frame.Navigate(new Register_Page());
        }       

        private void txtPassword_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (DataContext is UserVM viewModel)
            {
                if (sender is PasswordBox passwordBox)
                {
                    viewModel.User.Password = passwordBox.Password;
                }
            }
        }

        private void ExitApp(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
