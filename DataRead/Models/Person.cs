using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Net;
using System.Net.Mail;

namespace DataRead.Models
{
    public class Person : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private int _Id;
        public int Id
        {
            get => _Id;
            set
            {
                _Id = value;
                OnPropertyChanged("Id");
            }
        }
        private string _LastName; //LastName, string Name, string Patronymic, string Address);
        public string LastName
        {
            get => _LastName;
            set
            {
                _LastName = value;
                OnPropertyChanged("LastName");
            }
        }
        private string _Name; //LastName, string Name, string Patronymic, string Address);
        public string Name
        {
            get => _Name;
            set
            {
                _Name = value;
                OnPropertyChanged("Name");
            }
        }
        private string _Patronymic; //LastName, string Name, string Patronymic, string Address);
        public string Patronymic
        {
            get => _Patronymic;
            set
            {
                _Patronymic = value;
                OnPropertyChanged("Patronymic");
            }
        }
        private string _Address;
        public string Address
        {
            get => _Address;
            set
            {
                _Address = value;
                OnPropertyChanged("Address");
            }
        }
        private void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
