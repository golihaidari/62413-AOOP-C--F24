using DesktopApp.ViewModels;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace DesktopApp.View.WelcomePages
{
    /// <summary>
    /// Interaction logic for Register_Page.xaml
    /// </summary>
    public partial class Register_Page : Page
    {
        public Register_Page()
        {
            InitializeComponent();
            DataContext = new UserVM();
        }

        private void navigateToSignIn_click(object sender, RoutedEventArgs e)
        {
            var frame = (Frame)Application.Current.MainWindow.FindName("Index_frame");
            frame.Navigate(new Login_Page());
        }

        private void exitApp(object sender, RoutedEventArgs e)
        {
            var wnd = Window.GetWindow(this);
            wnd.Close();
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

        private void txtRepeatPassword_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (DataContext is UserVM viewModel)
            {
                if (sender is PasswordBox passwordBox)
                {
                    viewModel.User.RepeatedPassword = passwordBox.Password;
                }
            }
        }
    }
}
