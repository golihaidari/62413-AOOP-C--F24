using System.Windows;
using System.Windows.Controls;

namespace DesktopApp.View.MainPages
{
    /// <summary>
    /// Interaction logic for Basket_Page.xaml
    /// </summary>
    public partial class Basket_Page : Page
    {        
        public Basket_Page()
        {
            InitializeComponent();
            var app = (App)Application.Current;
            DataContext = app.Basket;
        }     

    }
}
