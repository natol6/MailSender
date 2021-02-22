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
using MailSender.lib.Interfaces;
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
        private SmtpAccount _SelectedSmtpAccount;
        public SmtpAccount SelectedSmtpAccount
        {
            get => _SelectedSmtpAccount;
            set => Set(ref _SelectedSmtpAccount, value);
        }
        private MessageSendContainer _SelectedMessageSendContainer;
        public MessageSendContainer SelectedMessageSendContainer
        {
            get => _SelectedMessageSendContainer;
            set => Set(ref _SelectedMessageSendContainer, value);
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
        private string _SelectedEmailAddressForEmail;
        public string SelectedEmailAddressForEmail
        {
            get => _SelectedEmailAddressForEmail;
            set => Set(ref _SelectedEmailAddressForEmail, value);
        }
        private SmtpAccount _SelectedSmtpAccountForEmail;
        public SmtpAccount SelectedSmtpAccountForEmail
        {
            get => _SelectedSmtpAccountForEmail;
            set => Set(ref _SelectedSmtpAccountForEmail, value);
        }
        private DateTime _SelectedDateTimeForEmail;
        public DateTime SelectedDateTimeForEmail
        {
            get => _SelectedDateTimeForEmail;
            set => Set(ref _SelectedDateTimeForEmail, value);
        }
        private readonly IDataBaseConnect _DbConnect;
        public MainWindowViewModel(IDataBaseConnect DBMailSender)
        {
            _DbConnect = DBMailSender;
        }

        #region Commands for database connect servise
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
        private ICommand _AddMessagePattern;
        public ICommand AddMessagePattern => _AddMessagePattern ??= new LambdaCommand(OnAddMessagePatternExecuted);
        private void OnAddMessagePatternExecuted(object p)
        {
            SelectedMessagePattern = null;
            MessagePatterns.Add(_DbConnect.AddDb(new MessagePattern { Subject = "Новая тема", Body = "" }));
            SelectedMessagePattern = MessagePatterns.OrderBy(t => t.Id).LastOrDefault();

        }
        private ICommand _DeleteMessagePattern;
        public ICommand DeleteMessagePattern => _DeleteMessagePattern ??= new LambdaCommand(OnDeleteMessagePatternExecuted, CanDeleteMessagePatternExecuted);
        private bool CanDeleteMessagePatternExecuted(object p)
        {
            return SelectedMessagePattern != null;
        }
        private void OnDeleteMessagePatternExecuted(object p)
        {
            _DbConnect.DeleteDb(SelectedMessagePattern);
            MessagePatterns.Remove(SelectedMessagePattern);
            SelectedMessagePattern = null;
        }
        private ICommand _UpdateMessagePattern;
        public ICommand UpdateMessagePattern => _UpdateMessagePattern ??= new LambdaCommand(OnUpdateMessagePatternExecuted, CanUpdateMessagePatternExecuted);
        private bool CanUpdateMessagePatternExecuted(object p)
        {
            return SelectedMessagePattern != null;
        }
        private void OnUpdateMessagePatternExecuted(object p)
        {
            _DbConnect.UpdateDb(SelectedMessagePattern);
        }
        private ICommand _AddEmailAddress;
        public ICommand AddEmailAddress => _AddEmailAddress ??= new LambdaCommand(OnAddEmailAddressExecuted);
        private void OnAddEmailAddressExecuted(object p)
        {
            SelectedEmailAddress = null;
            EmailAddresses.Add(_DbConnect.AddDb(new EmailAddress { Email = "login@domainname.com" }));
            SelectedEmailAddress = EmailAddresses.OrderBy(t => t.Id).LastOrDefault();

        }
        private ICommand _DeleteEmailAddress;
        public ICommand DeleteEmailAddress => _DeleteEmailAddress ??= new LambdaCommand(OnDeleteEmailAddressExecuted, CanDeleteEmailAddressExecuted);
        private bool CanDeleteEmailAddressExecuted(object p)
        {
            return SelectedEmailAddress != null;
        }
        private void OnDeleteEmailAddressExecuted(object p)
        {
            _DbConnect.DeleteDb(SelectedEmailAddress);
            EmailAddresses.Remove(SelectedEmailAddress);
            SelectedEmailAddress = null;
        }
        private ICommand _UpdateEmailAddress;
        public ICommand UpdateEmailAddress => _UpdateEmailAddress ??= new LambdaCommand(OnUpdateEmailAddressExecuted, CanUpdateEmailAddressExecuted);
        private bool CanUpdateEmailAddressExecuted(object p)
        {
            return SelectedEmailAddress != null;
        }
        private void OnUpdateEmailAddressExecuted(object p)
        {
            _DbConnect.UpdateDb(SelectedEmailAddress);
        }
        private ICommand _AddSmtpServer;
        public ICommand AddSmtpServer => _AddSmtpServer ??= new LambdaCommand(OnAddSmtpServerExecuted);
        private void OnAddSmtpServerExecuted(object p)
        {
            SelectedSmtpServer = null;
            SmtpServers.Add(_DbConnect.AddDb(new SmtpServer { Title = "New SmtpServer" }));
            SelectedSmtpServer = SmtpServers.OrderBy(t => t.Id).LastOrDefault();

        }
        private ICommand _DeleteSmtpServer;
        public ICommand DeleteSmtpServer => _DeleteSmtpServer ??= new LambdaCommand(OnDeleteSmtpServerExecuted, CanDeleteSmtpServerExecuted);
        private bool CanDeleteSmtpServerExecuted(object p)
        {
            return SelectedSmtpServer != null;
        }
        private void OnDeleteSmtpServerExecuted(object p)
        {
            SelectedSmtpAccount = null;
            foreach (SmtpAccount ac in SelectedSmtpServer.SmtpAccounts)
            {
                _DbConnect.DeleteDb(ac);
            }
            _DbConnect.DeleteDb(SelectedSmtpServer);
            SmtpServers.Remove(SelectedSmtpServer);
            SelectedSmtpServer = null;
        }
        private ICommand _UpdateSmtpServer;
        public ICommand UpdateSmtpServer => _UpdateSmtpServer ??= new LambdaCommand(OnUpdateSmtpServerExecuted, CanUpdateSmtpServerExecuted);
        private bool CanUpdateSmtpServerExecuted(object p)
        {
            return SelectedSmtpServer != null;
        }
        private void OnUpdateSmtpServerExecuted(object p)
        {
            _DbConnect.UpdateDb(SelectedSmtpServer);
        }
        private ICommand _AddSmtpAccount;
        public ICommand AddSmtpAccount => _AddSmtpAccount ??= new LambdaCommand(OnAddSmtpAccountExecuted, CanAddSmtpAccountExecuted);
        private bool CanAddSmtpAccountExecuted(object p)
        {
            return SelectedSmtpServer != null;
        }
        private void OnAddSmtpAccountExecuted(object p)
        {
            SelectedSmtpAccount = null;
            SelectedSmtpServer.SmtpAccounts.Add(_DbConnect.AddDb(new SmtpAccount { AccountEmail = "login@domainname.com", SmtpServerId = SelectedSmtpServer.Id }));
            SelectedSmtpAccount = SelectedSmtpServer.SmtpAccounts.OrderBy(t => t.Id).LastOrDefault();

        }
        private ICommand _DeleteSmtpAccount;
        public ICommand DeleteSmtpAccount => _DeleteSmtpAccount ??= new LambdaCommand(OnDeleteSmtpAccountExecuted, CanDeleteSmtpAccountExecuted);
        private bool CanDeleteSmtpAccountExecuted(object p)
        {
            return SelectedSmtpAccount != null;
        }
        private void OnDeleteSmtpAccountExecuted(object p)
        {
            _DbConnect.DeleteDb(SelectedSmtpAccount);
            SelectedSmtpServer.SmtpAccounts.Remove(SelectedSmtpAccount);
            SelectedSmtpAccount = null;
        }
        private ICommand _UpdateSmtpAccount;
        public ICommand UpdateSmtpAccount => _UpdateSmtpAccount ??= new LambdaCommand(OnUpdateSmtpAccountExecuted, CanUpdateSmtpAccountExecuted);
        private bool CanUpdateSmtpAccountExecuted(object p)
        {
            return SelectedSmtpAccount != null;
        }
        private void OnUpdateSmtpAccountExecuted(object p)
        {
            _DbConnect.UpdateDb(SelectedSmtpAccount);
        }
        private ICommand _AddMessageSendContainer;
        public ICommand AddMessageSendContainer => _AddMessageSendContainer ??= new LambdaCommand(OnAddMessageSendContainerExecuted, CanAddMessageSendContainerExecuted);
        private bool CanAddMessageSendContainerExecuted(object p)
        {
            return SelectedSmtpAccountForEmail != null &&
                SelectedSmtpServerForEmail != null &&
                SelectedEmailAddressForEmail != null &&
                SelectedMessagePatternForEmail != null;
        }
        private void OnAddMessageSendContainerExecuted(object p)
        {
            MessageSendContainers.Add(_DbConnect.AddDb(new MessageSendContainer { 
                SmtpServerUse = SelectedSmtpServerForEmail.SmtpServ,
                PortUse = SelectedSmtpServerForEmail.Port,
                SSLUse = SelectedSmtpServerForEmail.UseSSL,
                SmtpAccountEmailUse = SelectedSmtpAccountForEmail.AccountEmail,
                SmtpAccountPasswordUse = SelectedSmtpAccountForEmail.Password,
                SmtpAccountPerson_CompanyUse = SelectedSmtpAccountForEmail.Person_Company,
                EmailAddressesTo = SelectedEmailAddressForEmail,
                Subject = SelectedMessagePatternForEmail.Subject,
                Body = SelectedMessagePatternForEmail.Body,
                SendDate = SelectedDateTimeForEmail,
                Status = "" }));
            SelectedMessageSendContainer = MessageSendContainers.OrderBy(t => t.Id).LastOrDefault();

        }
        private ICommand _DeleteMessageSendContainer;
        public ICommand DeleteMessageSendContainer => _DeleteMessageSendContainer ??= new LambdaCommand(OnDeleteMessageSendContainerExecuted, CanDeleteMessageSendContainerExecuted);
        private bool CanDeleteMessageSendContainerExecuted(object p)
        {
            return SelectedMessageSendContainer != null;
        }
        private void OnDeleteMessageSendContainerExecuted(object p)
        {
            _DbConnect.DeleteDb(SelectedMessageSendContainer);
            MessageSendContainers.Remove(SelectedMessageSendContainer);
            SelectedMessageSendContainer = null;
        }
        private ICommand _UpdateMessageSendContainer;
        public ICommand UpdateMessageSendContainer => _UpdateMessageSendContainer ??= new LambdaCommand(OnUpdateMessageSendContainerExecuted, CanUpdateMessageSendContainerExecuted);
        private bool CanUpdateMessageSendContainerExecuted(object p)
        {
            return SelectedMessageSendContainer != null;
        }
        private void OnUpdateMessageSendContainerExecuted(object p)
        {
            _DbConnect.UpdateDb(SelectedMessageSendContainer);
        }
        #endregion
    }
}
