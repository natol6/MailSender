using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Collections.ObjectModel;
using WpfTest.Models;

namespace WpfTest.ViewModels
{
    class MailSenderViewModels : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public ObservableCollection<MailService> MailServices { get; set; } = new ObservableCollection<MailService>();
        public ObservableCollection<MessagePattern> MessagePatterns { get; set; } = new ObservableCollection<MessagePattern>();
        public ObservableCollection<EmailAddress> EmailAddresses { get; set; } = new ObservableCollection<EmailAddress>();
        private MailService selectedMailService;
        public MailService SelectedMailService
        {
            get => selectedMailService;
            set
            {
                selectedMailService = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SelectedMailService)));
            }
        }
        private MessagePattern selectedMessagePattern;
        public MessagePattern SelectedMessagePattern
        {
            get => selectedMessagePattern;
            set
            {
                selectedMessagePattern = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SelectedMessagePattern)));
            }
        }
        private EmailAddress selectedEmailAddress;
        public EmailAddress SelectedEmailAddress
        {
            get => selectedEmailAddress;
            set
            {
                selectedEmailAddress = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SelectedEmailAddress)));
            }
        }
        private Account selectedAccount;
        public Account SelectedAccount
        {
            get => selectedAccount;
            set
            {
                selectedAccount = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SelectedAccount)));
            }
        }
    }
}
