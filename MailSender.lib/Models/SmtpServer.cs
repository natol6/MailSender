using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MailSender.lib.Models
{
    public class SmtpServer : INotifyPropertyChanged
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
        private string _Title;
        public string Title
        {
            get => _Title;
            set
            {
                _Title = value;
                OnPropertyChanged("Title");
            }
        }
        private string _SmtpServ;
        public string SmtpServ
        {
            get => _SmtpServ;
            set
            {
                _SmtpServ = value;
                OnPropertyChanged("SmtpServ");
            }
        }
        private string _DomainName;
        public string DomainName
        {
            get => _DomainName;
            set
            {
                _DomainName = value;
                OnPropertyChanged("DomainName");
            }
        }
        private int _Port;
        public int Port
        {
            get => _Port;
            set
            {
                _Port = value;
                OnPropertyChanged("Port");
            }
        }
        private bool _UseSSL;
        public bool UseSSL
        {
            get => _UseSSL;
            set
            {
                _UseSSL = value;
                OnPropertyChanged("UseSSL");
            }
        }
        private void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
