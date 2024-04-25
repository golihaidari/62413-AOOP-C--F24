using System.ComponentModel;

namespace DesktopApp.Models
{
    public class User : INotifyPropertyChanged
    {
        private string userEmail = string.Empty;
        private string userPassword = string.Empty;
        private string userRepeatedPassword = string.Empty;
        public string Email
        {
            get => userEmail;
            set
            {
                if (value != null)
                {
                    userEmail = value;
                    OnPropertyChanged(nameof(Email));
                }
            }
        }

        public string Password
        {
            get => userPassword;
            set
            {
                if (value != null)
                {
                    userPassword = value;
                    OnPropertyChanged(nameof(Password));
                }
            }
        }

        public string RepeatedPassword
        {
            get => userRepeatedPassword;
            set
            {
                if (value != null)
                {
                    userRepeatedPassword = value;
                    OnPropertyChanged(nameof(RepeatedPassword));
                }
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public string toString()
        {
            return $"{{\"Email\": \"{userEmail}\",\"Password\": \"{userPassword}\"}}";
        }
    }
}
