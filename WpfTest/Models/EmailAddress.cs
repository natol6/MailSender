using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace WpfTest.Models
{
    class EmailAddress : INotifyPropertyChanged
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
        private string person;
        public string Person
        {
            get => person;
            set
            {
                person = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Person)));
            }
        }
        private string company;
        public string Company
        {
            get => company;
            set
            {
                company = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Company)));
            }
        }
        private string email;
        public string EMail
        {
            get => email;
            set
            {
                email = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(EMail)));
            }
        }
    }
}
