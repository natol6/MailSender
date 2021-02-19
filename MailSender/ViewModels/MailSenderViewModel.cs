using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MailSender.lib.Service;
using MailSender.lib.Models;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Windows;
using System.Runtime.CompilerServices;

namespace MailSender.ViewModels
{
    class MailSenderViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public ObservableCollection<SmtpServer> SmtpServers { get; set; } = new ObservableCollection<SmtpServer>();
        public ObservableCollection<MessagePattern> MessagePatterns { get; set; } = new ObservableCollection<MessagePattern>();
        public ObservableCollection<EmailAddress> EmailAddresses { get; set; } = new ObservableCollection<EmailAddress>();

        private SmtpServer _SelectedSmtpServer;
        public SmtpServer SelectedSmtpServer
        {
            get => _SelectedSmtpServer;
            set
            {
                _SelectedSmtpServer = value;
                OnPropertyChanged("SelectedSmtpServer");
            }
        }
        private MessagePattern _SelectedMessagePattern;
        public MessagePattern SelectedMessagePattern
        {
            get => _SelectedMessagePattern;
            set
            {
                _SelectedMessagePattern = value;
                OnPropertyChanged("SelectedMessagePattern");
            }
        }
        private EmailAddress _SelectedEmailAddress;
        public EmailAddress SelectedEmailAddress
        {
            get => _SelectedEmailAddress;
            set
            {
                _SelectedEmailAddress = value;
                OnPropertyChanged("SelectedEmailAddress");
            }
        }
        private SmtpAccount _SelectedAccount;
        public SmtpAccount SelectedAccount
        {
            get => _SelectedAccount;
            set
            {
                _SelectedAccount = value;
                OnPropertyChanged("SelectedAccount");
            }
        }
        private MessageSendContainer _SelectedMessageSendConteiner;
        public MessageSendContainer SelectedMessageSendConteiner
        {
            get => _SelectedMessageSendConteiner;
            set
            {
                _SelectedMessageSendConteiner = value;
                OnPropertyChanged("SelectedMessageSendConteiner");
            }
        }
        private SmtpServer _SelectedSmtpServerForEmail;
        public SmtpServer SelectedSmtpServerForEmail
        {
            get => _SelectedSmtpServerForEmail;
            set
            {
                _SelectedSmtpServerForEmail = value;
                OnPropertyChanged("SelectedSmtpServerForEmail");
            }
        }
        private MessagePattern _SelectedMessagePatternForEmail;
        public MessagePattern SelectedMessagePatternForEmail
        {
            get => _SelectedMessagePatternForEmail;
            set
            {
                _SelectedMessagePatternForEmail = value;
                OnPropertyChanged("SelectedMessagePatternForEmail");
            }
        }
        private EmailAddress _SelectedEmailAddressForEmail;
        public EmailAddress SelectedEmailAddressForEmail
        {
            get => _SelectedEmailAddressForEmail;
            set
            {
                _SelectedEmailAddressForEmail = value;
                OnPropertyChanged("SelectedEmailAddressForEmail");
            }
        }
        private SmtpAccount _SelectedAccountForEmail;
        public SmtpAccount SelectedAccountForEmail
        {
            get => _SelectedAccountForEmail;
            set
            {
                _SelectedAccountForEmail = value;
                OnPropertyChanged("SelectedAccountForEmail");
            }
        }
        private void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
        public MailSenderViewModel()
        {
            //LoadMailSet();
        }
    }
}
