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
        private string _AccountEmail;
        public string AccountEmail
        {
            get => _AccountEmail;
            set
            {
                _AccountEmail = value;
                OnPropertyChanged("AccountEmail");
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
        private string _Person_Comnany;
        public string Person_Company
        {
            get => _Person_Comnany;
            set
            {
                _Person_Comnany = value;
                OnPropertyChanged("Person_Company");
            }
        }
        private int _SmtpServerid;
        public int SmtpServerId
        {
            get => _SmtpServerid;
            set
            {
                _SmtpServerid = value;
                OnPropertyChanged("SmtpServerId");
            }
        }
        private SmtpServer _Smtp_Server;
        public SmtpServer Smtp_Server
        {
            get => _Smtp_Server;
            set
            {
                _Smtp_Server = value;
                OnPropertyChanged("Smtp_Server");
            }
        }
        private void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
