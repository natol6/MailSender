using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Net;
using System.Net.Mail;

namespace MailSender.lib.Models
{
    public class EmailAddress : INotifyPropertyChanged
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
        private string _Person_Company;
        public string Person_Company
        {
            get => _Person_Company;
            set
            {
                _Person_Company = value;
                OnPropertyChanged("Person_Company");
            }
        }
        private string _Email;
        public string Email
        {
            get => _Email;
            set
            {
                _Email = value;
                OnPropertyChanged("Email");
            }
        }
        private void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
