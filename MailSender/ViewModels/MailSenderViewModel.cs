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
using MailSender.ViewModels.Base;

namespace MailSender.ViewModels
{
    class MailSenderViewModel : ViewModel
    {
        public ObservableCollection<SmtpServer> SmtpServers { get; set; } = new ObservableCollection<SmtpServer>();
        public ObservableCollection<MessagePattern> MessagePatterns { get; set; } = new ObservableCollection<MessagePattern>();
        public ObservableCollection<EmailAddress> EmailAddresses { get; set; } = new ObservableCollection<EmailAddress>();
        private string _Title = "Главное окно программы";
        /// <summary>Заголовок окна</summary>
        public string Title
        {
            get => _Title;
            set => Set(ref _Title, value);
        }
        private SmtpServer _SelectedSmtpServer;
        public SmtpServer SelectedSmtpServer
        {
            get => _SelectedSmtpServer;
            set => Set(ref _SelectedSmtpServer, value);
            
        }
        private MessagePattern _SelectedMessagePattern;
        public MessagePattern SelectedMessagePattern
        {
            get => _SelectedMessagePattern;
            set => Set(ref _SelectedMessagePattern, value);
        }
        private EmailAddress _SelectedEmailAddress;
        public EmailAddress SelectedEmailAddress
        {
            get => _SelectedEmailAddress;
            set => Set(ref _SelectedEmailAddress, value);
        }
        private SmtpAccount _SelectedAccount;
        public SmtpAccount SelectedAccount
        {
            get => _SelectedAccount;
            set => Set(ref _SelectedAccount, value);
        }
        private MessageSendContainer _SelectedMessageSendConteiner;
        public MessageSendContainer SelectedMessageSendConteiner
        {
            get => _SelectedMessageSendConteiner;
            set => Set(ref _SelectedMessageSendConteiner, value);
        }
        private SmtpServer _SelectedSmtpServerForEmail;
        public SmtpServer SelectedSmtpServerForEmail
        {
            get => _SelectedSmtpServerForEmail;
            set => Set(ref _SelectedSmtpServerForEmail, value);
        }
        private MessagePattern _SelectedMessagePatternForEmail;
        public MessagePattern SelectedMessagePatternForEmail
        {
            get => _SelectedMessagePatternForEmail;
            set => Set(ref _SelectedMessagePatternForEmail, value);
        }
        private EmailAddress _SelectedEmailAddressForEmail;
        public EmailAddress SelectedEmailAddressForEmail
        {
            get => _SelectedEmailAddressForEmail;
            set => Set(ref _SelectedEmailAddressForEmail, value);
        }
        private SmtpAccount _SelectedAccountForEmail;
        public SmtpAccount SelectedAccountForEmail
        {
            get => _SelectedAccountForEmail;
            set => Set(ref _SelectedAccountForEmail, value);
        }
        public MailSenderViewModel()
        {
            //LoadMailSet();
        }
    }
}
