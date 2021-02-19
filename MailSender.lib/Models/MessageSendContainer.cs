using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;

namespace MailSender.lib.Models
{
    public class MessageSendContainer : INotifyPropertyChanged
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
        private string _SmtpServerUse;
        public string SmtpServerUse
        {
            get => _SmtpServerUse;
            set
            {
                _SmtpServerUse = value;
                OnPropertyChanged("SmtpServerUse");
            }
        }
        private int _PortUse;
        public int PortUse
        {
            get => _PortUse;
            set
            {
                _PortUse = value;
                OnPropertyChanged("PortUse");
            }
        }
        private bool _SSLUse;
        public bool SSLUse
        {
            get => _SSLUse;
            set
            {
                _SSLUse = value;
                OnPropertyChanged("SSLUse");
            }
        }
        private string _SmtpAccountEmailUse;
        public string SmtpAccountEmailUse
        {
            get => _SmtpAccountEmailUse;
            set
            {
                _SmtpAccountEmailUse = value;
                OnPropertyChanged("SmtpAccountEmailUse");
            }
        }
        private string _SmtpAccountPasswordlUse;
        public string SmtpAccountPasswordUse
        {
            get => _SmtpAccountPasswordlUse;
            set
            {
                _SmtpAccountPasswordlUse = value;
                OnPropertyChanged("SmtpAccountPasswordUse");
            }
        }
        private string _SmtpAccountPerson_CompanyUse;
        public string SmtpAccountPerson_CompanyUse
        {
            get => _SmtpAccountPerson_CompanyUse;
            set
            {
                _SmtpAccountPerson_CompanyUse = value;
                OnPropertyChanged("SmtpAccountPerson_CompanyUse");
            }
        }
        private string _EmailAddresses;
        public string EmailAddresses
        {
            get => _EmailAddresses;
            set
            {
                _EmailAddresses = value;
                OnPropertyChanged("EmailAddresses");
            }
        }
        private string _Subject;
        public string Subject
        {
            get => _Subject;
            set
            {
                _Subject = value;
                OnPropertyChanged("Subject");
            }
        }
        private string _Body;
        public string Body
        {
            get => _Body;
            set
            {
                _Body = value;
                OnPropertyChanged("Body");
            }
        }
        private DateTime _SendDate;
        public DateTime SendDate
        {
            get => _SendDate;
            set
            {
                _SendDate = value;
                OnPropertyChanged("SendDate");
            }
        }
        private string _Status;
        public string Status
        {
            get => _Status;
            set
            {
                _Status = value;
                OnPropertyChanged("Status");
            }
        }
        private void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
