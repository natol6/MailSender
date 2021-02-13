using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace WpfTest.Models
{
    class Account : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private int id;
        public int Id
        {
            get => id;
            set
            {
                id = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Id)));
            }
        }
        private string login;
        public string Login
        {
            get => login;
            set
            {
                login = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Login)));
            }
        }
        private string securepassword;
        public string SecurePassword
        {
            get => securepassword;
            set
            {
                securepassword = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SecurePassword)));
            }
        }
    }
}
