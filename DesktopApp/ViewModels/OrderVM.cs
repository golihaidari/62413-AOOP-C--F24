using DesktopApp.Models;
using DesktopApp.Models.JsonModels;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Net.Http;
using System.Windows;

namespace DesktopApp.ViewModels
{
    class OrderVM : INotifyPropertyChanged
    {
        private ObservableCollection<OrderResponse?> orders = [];
        public ObservableCollection<OrderResponse?> Orders 
        {
            get => orders;
            set
            {
                if (value != null)
                {
                    orders = value;
                    OnPropertyChanged(nameof(Orders));
                }
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        public OrderVM() 
        {
            LoadOrders();
        }       

        private async void LoadOrders()
        {
            string apiURL = "https://localhost:7257/api/order/"+((App)Application.Current).UserEmail;

            var response = await ApiHelper.SendRequest(apiURL, HttpMethod.Get, "");

            try
            {
                var orderList = JsonConvert.DeserializeObject<IList<OrderResponse>>(response);
                if (orderList != default)
                {
                    Orders = new ObservableCollection<OrderResponse?>(orderList);
                }
                else
                {
                    MessageBox.Show("Order History is empty!");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                MessageBox.Show(response);
            }                    

        }       
    }
}
