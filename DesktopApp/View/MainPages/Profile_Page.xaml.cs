using DesktopApp.ViewModels;
using System.Windows.Controls;

namespace DesktopApp.View.MainPages
{
    /// <summary>
    /// Interaction logic for Profile_Page.xaml
    /// </summary>
    public partial class Profile_Page : Page
    {
        public Profile_Page()
        {
            InitializeComponent();
            AddressVM vm= new AddressVM();
            DataContext = vm;
        }
    }
}
