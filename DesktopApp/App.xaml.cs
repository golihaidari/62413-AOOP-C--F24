
using DesktopApp.ViewModels;
using System.Windows;

namespace DesktopApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public string AccessToken { get; set; } = string.Empty;
        public string UserEmail { get; set; } = string.Empty;
        public string UserRole { get; set; }    = string.Empty ;
        public BasketVM Basket { get; } = new BasketVM();
    }

}
