using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MailSender.ViewModels.Base;
using System.Collections.ObjectModel;
using MailSender.lib.Service;
using MailSender.lib.Models;
using MailSender.lib.Commands;
using System.Windows.Input;

namespace MailSender.ViewModels
{
    class MainWindowViewModel: ViewModel
    {
        private string _Title = "Рассыльщик почты";
        public string Title
        {
            get => _Title;
            set => Set(ref _Title, value);
        }
        private ObservableCollection<SmtpServer> _SmtpServers;
        public ObservableCollection<SmtpServer> SmtpServers
        {
            get => _SmtpServers;
            set => Set(ref _SmtpServers, value);
        }
        private ObservableCollection<MessagePattern> _MessagePatterns;
        public ObservableCollection<MessagePattern> MessagePatterns
        {
            get => _MessagePatterns;
            set => Set(ref _MessagePatterns, value);
        }
        private ObservableCollection<EmailAddress> _EmailAddresses;
        public ObservableCollection<EmailAddress> EmailAddresses
        {
            get => _EmailAddresses;
            set => Set(ref _EmailAddresses, value);
        }
        private ObservableCollection<MessageSendContainer> _MessageSendContainers;
        public ObservableCollection<MessageSendContainer> MessageSendContainers
        {
            get => _MessageSendContainers;
            set => Set(ref _MessageSendContainers, value);
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
        
        private readonly IDataBaseConnect _DbConnect;
        public MainWindowViewModel(IDataBaseConnect DBMailSender)
        {
            _DbConnect = DBMailSender;
        }
        #region Commands
        private ICommand _LoadDataCommand;
        public ICommand LoadDataCommand => _LoadDataCommand ??= new LambdaCommand(OnLoadDataCommandExecuted);
        private void OnLoadDataCommandExecuted(object p)
        {
            //var accounts = _DbConnect.DBGetSmtpAccounts();
            SmtpServers = new ObservableCollection<SmtpServer>(_DbConnect.DBGetSmtpServers());
            MessagePatterns = new ObservableCollection<MessagePattern>(_DbConnect.DBGetMessagePatterns());
            EmailAddresses = new ObservableCollection<EmailAddress>(_DbConnect.DBGetEmailAddresses());
            MessageSendContainers = new ObservableCollection<MessageSendContainer>(_DbConnect.DBGetMessageSendContainers());
        }

        #endregion
    }
}
