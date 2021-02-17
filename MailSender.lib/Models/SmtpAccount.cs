using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MailSender.lib.Models
{
    public class SmtpAccount : INotifyPropertyChanged
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
        private string _Login;
        public string Login
        {
            get => _Login;
            set
            {
                _Login = value;
                OnPropertyChanged("Login");
            }
        }
        private string _Password;
        public string Password
        {
            get => _Password;
            set
            {
                _Password = value;
                OnPropertyChanged("Password");
            }
        }
        private string _Person;
        public string Person
        {
            get => _Person;
            set
            {
                _Person = value;
                OnPropertyChanged("Person");
            }
        }
        private int _SmtpServerid;
        public int SmtpServerId
        {
            get => _SmtpServerid;
            set
            {
                _SmtpServerid = value;
                OnPropertyChanged("MailServiceId");
            }
        }
        private void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
