using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace WpfTest.Models
{
    class MailService : INotifyPropertyChanged
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
        private string title;
        public string Title
        {
            get => title;
            set
            {
                title = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Title)));
            }
        }
        private string smtpServer;
        public string SmtpServer
        {
            get => smtpServer;
            set
            {
                smtpServer = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SmtpServer)));
            }
        }
        private string domainname;
        public string DomainName
        {
            get => domainname;
            set
            {
                domainname = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(DomainName)));
            }
        }
        public ObservableCollection<Account> Accounts { get; set; } = new ObservableCollection<Account>();
    }
}
