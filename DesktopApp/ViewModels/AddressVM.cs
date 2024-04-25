using DesktopApp.Models;
using DesktopApp.View.MainPages;
using Newtonsoft.Json;
using Prism.Commands;
using System.ComponentModel;
using System.Net.Http;
using System.Windows;
using System.Windows.Input;

namespace DesktopApp.ViewModels
{
     class AddressVM : INotifyPropertyChanged
    {

        private Address? address = new();

        public ICommand SaveCommand { get; }
        public ICommand DeleteCommand { get; }

        public AddressVM()
        {
            SaveCommand = new DelegateCommand(Update);
            DeleteCommand = new DelegateCommand(Delete);

            _ =  LoadAddress();
        }


        public Address? Address
        {
            get => address;
            set
            {
                if (value != null)
                {
                    address = value;
                    OnPropertyChanged(nameof(Address));
                }
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public async Task LoadAddress()
        {
            Address!.Email = ((App)Application.Current).UserEmail;

            string apiURL = "https://localhost:7257/api/";
            string endpoint = "address/" + Address!.Email;

            var response = await ApiHelper.SendRequest(apiURL, HttpMethod.Get, endpoint);
            try
            {
                if (response != default && !response.Equals("Request failed with status code NotFound"))
                {
                    Address = JsonConvert.DeserializeObject<Address>(response);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                //MessageBox.Show(response);
            }

        }

        private bool ValidateInputs()
        {
            if (string.IsNullOrEmpty(address!.FirstName) || string.IsNullOrEmpty(address!.LastName) ||
                string.IsNullOrEmpty(address!.Company) || string.IsNullOrEmpty(address!.VatNr) ||
                string.IsNullOrEmpty(address!.MobileNr) || string.IsNullOrEmpty(address!.Country) ||
                string.IsNullOrEmpty(address!.ZipCode) || string.IsNullOrEmpty(address!.City) ||
                string.IsNullOrEmpty(address!.Address1) || string.IsNullOrEmpty(address!.Address2)
            )
            {
                return false;
            }


            return true;
        }
        
        public async void Update()
        {
            if (!ValidateInputs())
            {
                MessageBox.Show("All fields are required!!");
            }
            else
            {
                string apiURL = "https://localhost:7257/api/";
                string endpoint = "address/" + address!.Email;
                var getResponse = await ApiHelper.SendRequest(apiURL, HttpMethod.Get, endpoint);

                try
                {
                    string payload = address!.toString();
                    string? sendResponse;

                    if (getResponse.Equals("Request failed with status code NotFound"))
                    {
                        sendResponse = await ApiHelper.SendRequest(apiURL, HttpMethod.Post, "address", payload);
                    }
                    else
                    {
                        sendResponse = await ApiHelper.SendRequest(apiURL, HttpMethod.Put, "address", payload);
                    }

                    MessageBox.Show(sendResponse);

                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    MessageBox.Show($"Error: {ex.Message}");
                }

            }
           
        }

        public async void Delete()
        {
            string apiURL = "https://localhost:7257/api/";            
            try
            {
                MessageBoxResult result = MessageBox.Show("Do you want to proceed?", "Confirmation", MessageBoxButton.OKCancel);

                if (result == MessageBoxResult.OK)
                {
                    string endpoint = "address/" + address!.Email;
                    var response = await ApiHelper.SendRequest(apiURL, HttpMethod.Delete, endpoint);
                    if (!response.Equals("The specified address has been deleted!"))
                    {
                        MessageBox.Show("Error occures cannot delete the address");
                    }
                    else
                    {
                        endpoint = "Users/" + address!.Email;
                        response = await ApiHelper.SendRequest(apiURL, HttpMethod.Delete, endpoint);
                        MessageBox.Show(response);

                        if (response.Equals("User has been deleted!"))
                        {
                            var window = Application.Current.MainWindow;
                            window.Close();

                            var welcomeWindow = new WelcomeWindow();
                            Application.Current.MainWindow = welcomeWindow;
                            welcomeWindow.Show();
                        }
                    }
                }
            }
            catch(Exception ex) 
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
            
        }
    }
}
