using DesktopApp.Models;
using DesktopApp.ViewModels;
using Newtonsoft.Json;
using System.Net.Http;
using System.Windows;
using System.Windows.Controls;

namespace DesktopApp.View.MainPages
{
    /// <summary>
    /// Interaction logic for Products_Page.xaml
    /// </summary>
    public partial class Products_Page : Page
    {
        public Products_Page()
        {
            InitializeComponent();            
            var app = (App)Application.Current;
            DataContext = app.Basket;
        }               
    }
}
