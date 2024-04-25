using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopApp.Models
{
    public class BasketItem : INotifyPropertyChanged
    {
        private Product? product;
        private int quantity;

        public Product? Product
        {
            get { return product; }
            set
            {
                if (value != null)
                {
                    product = value;
                    OnPropertyChanged(nameof(Product));
                }
            }
        }

        public int Quantity
        {
            get { return quantity; }
            set
            {
                if (quantity != value)
                {
                    quantity = value;
                    OnPropertyChanged(nameof(Quantity));
                }
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        
        public override string ToString()
        {
            return Product!.ToString() + $", Quantity: {Quantity}";
        }

        
    }
}
