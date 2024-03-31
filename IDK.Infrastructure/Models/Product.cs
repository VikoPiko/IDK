using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace IDK.Infrastructure.Models
{
    public class Product : INotifyPropertyChanged
    {
        private int _id;
        [Key]
        public int Id 
        { 
            get 
            { return _id; } 
            set
            {
                _id = value;
                OnPropertyChanged();
            }
        }
        private string _name;
        [Required]
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value; 
                OnPropertyChanged();
            }
        }
        

        [Column(TypeName = "decimal(6,2)")]
        [Required]
        private decimal _price;
        public decimal Price
        {
            get { return _price; }
            set
            {
                _price = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
