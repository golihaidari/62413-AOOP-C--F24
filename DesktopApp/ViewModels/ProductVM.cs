using DesktopApp.Models;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Net.Http;
using System.Windows;

namespace DesktopApp.ViewModels
{
    public class ProductVM : INotifyPropertyChanged
    {
        private ObservableCollection<Product?> products = [];
        public ObservableCollection<Product?> Products
        {
            get => products;
            set
            {
                if (value != null)
                {
                    products = value;
                    OnPropertyChanged(nameof(Products));
                }
            }
        }


        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public ProductVM()
        {
            LoadProducts();
            InsertToDb();
        }

        public async void LoadProducts()
        {  
            try
            {
                string apiUrl = "https://raw.githubusercontent.com/larsthorup/checkout-data/main/product-v2.json";
                var response = await ApiHelper.SendRequest(apiUrl, HttpMethod.Get, "");

                var productList = JsonConvert.DeserializeObject<IList<Product>>(response);
                if (productList != default && productList.Count > 0)
                {
                    Products = new ObservableCollection<Product?>(productList.Where(p => p.imageUrl != null && p.imageUrl != string.Empty));
                }
                else
                {
                    MessageBox.Show("Unable to load products!!", "Error");
                }                 
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }         
        }


        //this method is used to insert the products data to the db, so later when the customer need to settle the order. the prodcut should be existed in the database.
        private async void InsertToDb()
        {
            var response = await ApiHelper.SendRequest("https://localhost:7257/api/", HttpMethod.Get, "product");
            if (response.Equals("Request failed with status code NotFound") && Products.Count > 0)
            {
                try
                {
                    foreach (var product in Products!)
                    {
                        if (!string.IsNullOrEmpty(product!.imageUrl))
                        {
                            // Serialize the product object to JSON string
                            string jsonPayload = JsonConvert.SerializeObject(product, Formatting.Indented);
                            response = await ApiHelper.SendRequest("https://localhost:7257/api/", HttpMethod.Post, "product", jsonPayload);
                            MessageBox.Show(response);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"{ex.Message}");
                }
            }                    
        }        
        
    }

}
