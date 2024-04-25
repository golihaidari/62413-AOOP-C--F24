using DesktopApp.View.WelcomePages;
using System.Windows;

namespace DesktopApp
{
    /// <summary>
    /// Interaction logic for WelcomeWindow.xaml
    /// </summary>
    public partial class WelcomeWindow : Window
    {
        public WelcomeWindow()
        {
            InitializeComponent();
            Index_frame.Navigate(new Login_Page());
            //Index_frame.Content = new Login_Page();
        }
    }
}
