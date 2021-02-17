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
        private SmtpServer _SmtpServerUse;
        public SmtpServer SmtpServerUse
        {
            get => _SmtpServerUse;
            set
            {
                _SmtpServerUse = value;
                OnPropertyChanged("SmtpServerUse");
            }
        }
        private SmtpAccount _SmtpAccountUse;
        public SmtpAccount SmtpAccountUse
        {
            get => _SmtpAccountUse;
            set
            {
                _SmtpAccountUse = value;
                OnPropertyChanged("SmtpAccountUse");
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
        public ObservableCollection<EmailAddress> EmailAddresses { get; set; } = new ObservableCollection<EmailAddress>();
        private void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
