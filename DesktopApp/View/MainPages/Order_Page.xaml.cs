using DesktopApp.Models.JsonModels;
using DesktopApp.ViewModels;
using System.Windows.Controls;

namespace DesktopApp.View.MainPages
{
    /// <summary>
    /// Interaction logic for Order_Page.xaml
    /// </summary>
    public partial class Order_Page : Page
    {
        public Order_Page()
        {
            InitializeComponent();
            DataContext = new OrderVM();
        }

        private void OrderListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sender is ListView listView && listView.SelectedItem is OrderResponse selectedOrder)
            {
                // Update the ItemsSource of the basket item ListView
                orderItemListView.ItemsSource = selectedOrder.OrderDetails;
            }
        }
    }
}
