using DesktopApp.Models;
using Prism.Commands;
using Prism.Mvvm;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Net.Http;
using System.Windows;
using System.Windows.Input;
using Newtonsoft.Json;
using System.Windows.Controls;
using DesktopApp.View.MainPages;

namespace DesktopApp.ViewModels
{
    public class BasketVM : BindableBase
    {
        private readonly ProductVM pVM = new();
        public ObservableCollection<Product?> Products => pVM.Products;

        public ObservableCollection<BasketItem> BasketItems { get; set; } = [];
        public decimal TotalPrice => BasketItems.Sum(item => item.Product!.price * item.Quantity);

        public ICommand AddToCartCommand { get; }
        public ICommand RemoveFromCartCommand { get; }
        public ICommand IncreaseQuantityCommand { get; }
        public ICommand DecreaseItemQuantityCommand { get; }
        public ICommand CheckoutCommand { get; }

        public BasketVM()
        {
            // Initialize commands
            AddToCartCommand = new DelegateCommand<Product?>(AddToCart);
            RemoveFromCartCommand = new DelegateCommand<BasketItem?>(RemoveFromCart);
            IncreaseQuantityCommand = new DelegateCommand<BasketItem?>(IncreaseQuantity);
            DecreaseItemQuantityCommand = new DelegateCommand<BasketItem?>(DecreaseQuantity);
            CheckoutCommand = new DelegateCommand(CheckOut);

            FillBasket();
        }

        // Sample products for demo purposes
        private void FillBasket()
        {
            var products = new[]
            {
                new Product { id= "vitamin-d-90-120", name = "D-vitamin, 90ug, 120 stk", price = 116,
                    currency = "DKK", rebateQuantity = 3, rebatePercent = 10,
                    upsellProductId = "adf", imageUrl = "https://apopro.dk/Images/d3-vitamin-staerk-kapsler-90-%C2%B5g-kosttilskud-120-stk-214878" },

                new Product { id= "trimmer-battery", name = "Barbermaskine m batteri", price = 200,
                    currency = "DKK", rebateQuantity = 0, rebatePercent = 0,
                    upsellProductId = "dafadf", imageUrl = "https://www.helsebixen.dk/shop/media/catalog/product/cache/1/image/400x/9df78eab33525d08d6e5fb8d27136e95/f/3/f3_1.png" },
            };

            foreach (var product in products)
            {
                BasketItems.Add(new BasketItem { Product = product, Quantity = 1 });
            }
        }


        private void AddToCart(Product? product)
        {
            if (product != null)
            {
                var basketItem = BasketItems.FirstOrDefault(item => item.Product!.id == product.id);
                if (basketItem != null)
                {
                    basketItem.Quantity++;
                }
                else
                {
                    BasketItems.Add(new BasketItem { Product = product, Quantity = 1 });
                }
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(TotalPrice)));
            }
            else
            {
                MessageBox.Show("No product is selected");
            }
        }

        private void RemoveFromCart(BasketItem? item)
        {
            if (item != null)
            {
                BasketItems.Remove(item);
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(TotalPrice)));
            }
        }

        private void IncreaseQuantity(BasketItem? item)
        {
            if (item != null)
            {
                item.Quantity++;
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(TotalPrice)));
            }
        }

        private void DecreaseQuantity(BasketItem? item)
        {
            if (item != null)
            {
                if (item.Quantity == 1) BasketItems.Remove(item);
                else item.Quantity--;

                OnPropertyChanged(new PropertyChangedEventArgs(nameof(TotalPrice)));
            }
        }

        private async void CheckOut()
        {
            var aVM = new AddressVM();
            await aVM.LoadAddress();
            if (string.IsNullOrEmpty( aVM.Address!.FirstName))
            {
                MessageBox.Show("Fill out  the profile form first!!!\nTo make an order, address information is required.");
                var frame = (Frame)Application.Current.MainWindow.FindName("Main_frame");
                frame.Navigate(new Profile_Page());
            }
            else
            {

                if (BasketItems.Count <= 0)
                {
                    MessageBox.Show("Basket is empty!!!\nSelect products first.", "Error"); ;
                }
                else
                {
                    string apiURL = "https://localhost:7257/api/";
                    Order order = new()
                    {
                        CustomerEmail = ((App)Application.Current).UserEmail, //"baran@gmail.com", //((App)Application.Current).UserEmail;
                        OrderDetails = BasketItems.ToList(),
                    };

                    // Serialize the Order object to JSON string
                    string jsonPayload = JsonConvert.SerializeObject(order, Formatting.Indented);

                    var response = await ApiHelper.SendRequest(apiURL, HttpMethod.Post, "order", jsonPayload);

                    MessageBox.Show(response);

                    if (response != null && response.Equals("Order is inserted successfully!"))
                    {
                        BasketItems = [];

                        var frame = (Frame)Application.Current.MainWindow.FindName("Main_frame");
                        frame.Navigate(new Products_Page());
                    }
                }
            }
        }           
    }
}
