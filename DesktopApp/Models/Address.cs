using System.ComponentModel;

namespace DesktopApp.Models
{
    public class Address : INotifyPropertyChanged
    {
        private string _email = string.Empty;
        private string _mobileNr = string.Empty;
        private string _firstName = string.Empty;
        private string _lastName = string.Empty;
        private string _company = string.Empty;
        private string _vatNr = string.Empty;
        private string _country = string.Empty;
        private string _zipCode = string.Empty;
        private string _city = string.Empty;
        private string _address1 = string.Empty;
        private string _address2 = string.Empty;

        public string Email
        {
            get => _email;
            set
            {
                if (value != null)
                {
                    _email = value;
                    OnPropertyChanged(nameof(Email));
                }
            }
        }

        public string MobileNr
        {
            get => _mobileNr;
            set
            {
                if (value != null)
                {
                    _mobileNr = value;
                    OnPropertyChanged(nameof(MobileNr));
                }
            }
        }

        public string FirstName
        {
            get => _firstName;
            set
            {
                if (value != null)
                {
                    _firstName = value;
                    OnPropertyChanged(nameof(FirstName));
                }
            }
        }

        public string LastName
        {
            get => _lastName;
            set
            {
                if (value != null)
                {
                    _lastName = value;
                    OnPropertyChanged(nameof(LastName));
                }
            }
        }

        public string Company
        {
            get => _company;
            set
            {
                if (value != null)
                {
                    _company = value;
                    OnPropertyChanged(nameof(Company));
                }
            }
        }

        public string VatNr
        {
            get => _vatNr;
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    _vatNr = value;
                    OnPropertyChanged(nameof(VatNr));
                }
            }
        }

        public string Country
        {
            get => _country;
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    _country = value;
                    OnPropertyChanged(nameof(Country));
                }
            }
        }

        public string ZipCode
        {
            get => _zipCode;
            set
            {
                if (value != null)
                {
                    _zipCode = value;
                    OnPropertyChanged(nameof(ZipCode));
                }
            }
        }

        public string City
        {
            get => _city;
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    _city = value;
                    OnPropertyChanged(nameof(City));
                }
            }
        }

        public string Address1
        {
            get => _address2;
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    _address2 = value;
                    OnPropertyChanged(nameof(Address1));
                }
            }
        }

        public string Address2
        {
            get => _address1;
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    _address1 = value;
                    OnPropertyChanged(nameof(Address2));
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
            return $"{{\"FirstName\": \"{FirstName}\",\"LastName\": \"{LastName}\", \"Email\": \"{Email}\", \"MobileNr\" :\"{MobileNr}\",\"Company\":\"{Company}\", \"VatNr\": \"{VatNr}\", \"Country\": \"{Country}\", \"ZipCode\": \"{ZipCode}\", \"City\": \"{City}\",\"Address1\":\"{Address1}\", \"Address2\": \"{Address2} \"}}";
        }
    }
}
