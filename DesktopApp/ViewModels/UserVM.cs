using DesktopApp.Models;
using DesktopApp.Models.JsonModels;
using DesktopApp.View.WelcomePages;
using Newtonsoft.Json;
using Prism.Commands;
using System.ComponentModel;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace DesktopApp.ViewModels
{
    public class UserVM: INotifyPropertyChanged
    {
        private User user = new();

        public User User
        {
            get => user;
            set
            {
                if (value != null)
                {
                    user = value;
                    OnPropertyChanged(nameof(User));
                }
            }
        }        

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public ICommand RegisterCommand { get; }
        public ICommand LoginCommand { get; }
        public ICommand LogoutCommand { get; }

        public UserVM()
        {
            RegisterCommand = new DelegateCommand(Register);
            LoginCommand = new DelegateCommand(Login);
            LogoutCommand = new DelegateCommand(Logout);
        }

        private async void Register()
        {
            if (!ValidateInputs())
            {
                MessageBox.Show("Please enter a valid email and password.\n valid Emaii= example@example.com\nvalid password= at leat 12 character including: an uppercase letter + a lowecase letter + and a special letter #.");
            }
            else
            {

                if (User.Password != User.RepeatedPassword)
                {
                    MessageBox.Show("your password does not match!!");
                }
                else
                {
                    string apiURL = "https://localhost:7257/api/";
                    string payload = user.toString(); 
                    string response = await ApiHelper.SendRequest(apiURL, HttpMethod.Post, "Users", payload);
                    MessageBox.Show(response);

                    if (response.Equals("User is registered successfully!"))
                    {
                        var frame = (Frame)Application.Current.MainWindow.FindName("Index_frame");
                        frame.Navigate(new Login_Page());
                    }
                }
            }

        }

        private async void Login()
        {
            if (!ValidateInputs())
            {
                MessageBox.Show("Please enter a valid email and password.");
            }
            else
            {
                string apiURL = "https://localhost:7257/api/";
                string payload = user.toString();

                var response = await ApiHelper.SendRequest(apiURL, HttpMethod.Post, "login", payload);
                try
                {
                    var loginResponse = JsonConvert.DeserializeObject<LoginResponse>(response);
                    if (loginResponse != default && !string.IsNullOrEmpty(loginResponse!.Token))
                    {
                        // Set the token in the App class
                        ((App)Application.Current).AccessToken = loginResponse!.Token;
                        ((App)Application.Current).UserRole = loginResponse!.UserRole;
                        ((App)Application.Current).UserEmail = User.Email;


                        Application.Current.MainWindow.Hide();

                        Application.Current.MainWindow = new HomeWindow();
                        Application.Current.MainWindow.Show();
                    }
                    else
                    {
                        MessageBox.Show("Your credential is not valid!");
                    }
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    if (response.Equals("Request failed with status code Unauthorized"))
                    {
                        MessageBox.Show("Your credential is not valid!.\n Please Try Again.");
                    }
                    else
                    {
                        MessageBox.Show(response);
                    }                    
                }                
            }            
        }

        private void Logout()
        {
            ((App)Application.Current).AccessToken = string.Empty;
            ((App)Application.Current).UserRole = string.Empty;
            ((App)Application.Current).UserEmail = string.Empty;

            Application.Current.MainWindow.Hide();

            Application.Current.MainWindow = new WelcomeWindow();
            Application.Current.MainWindow.Show();
        }

        

        public bool ValidateInputs()
        {
            bool IsValidEmail = !string.IsNullOrWhiteSpace(User.Email) && Regex.IsMatch(User.Email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$");
            bool IsValidPassword = !string.IsNullOrWhiteSpace(User.Password) && User.Password.Length >= 12;
            
            return IsValidEmail && IsValidPassword;
        }
        
    }
}
