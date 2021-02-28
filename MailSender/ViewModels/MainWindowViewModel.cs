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
using MailSender.interfaces;
using System.Windows.Input;
using System.Security;
using System.Windows;
using System.ComponentModel;

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
        private ObservableCollection<MessageSendContainer> _MessageSendOutContainers;
        public ObservableCollection<MessageSendContainer> MessageSendOutContainers
        {
            get => _MessageSendOutContainers;
            set => Set(ref _MessageSendOutContainers, value);
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
        private SecureString _SelectedPassword;
        public SecureString SelectedPassword
        {
            get => _SelectedPassword;
            set => Set(ref _SelectedPassword, value);
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
        private string _SubjectForEmail;
        public string SubjectForEmail
        {
            get => _SubjectForEmail;
            set => Set(ref _SubjectForEmail, value);
        }
        private string _BodyForEmail;
        public string BodyForEmail
        {
            get => _BodyForEmail;
            set => Set(ref _BodyForEmail, value);
        }
        private EmailAddress _SelectedEmailAddressForEmail;
        public EmailAddress SelectedEmailAddressForEmail
        {
            get => _SelectedEmailAddressForEmail;
            set => Set(ref _SelectedEmailAddressForEmail, value);
        }
        private string _EmailAddressesForEmail;
        public string EmailAddressesForEmail
        {
            get => _EmailAddressesForEmail;
            set => Set(ref _EmailAddressesForEmail, value);
        }
        private SmtpAccount _SelectedSmtpAccountForEmail;
        public SmtpAccount SelectedSmtpAccountForEmail
        {
            get => _SelectedSmtpAccountForEmail;
            set => Set(ref _SelectedSmtpAccountForEmail, value);
        }
        private DateTime? _SelectedTimeForEmail;
        public DateTime? SelectedTimeForEmail
        {
            get => _SelectedTimeForEmail;
            set => Set(ref _SelectedTimeForEmail, value);
        }
        private DateTime? _SelectedDateTimeForEmail;
        public DateTime? SelectedDateTimeForEmail
        {
            get => _SelectedDateTimeForEmail;
            set => Set(ref _SelectedDateTimeForEmail, value);
        }
        private readonly IDataBaseMailSender _DbConnect;
        private readonly ITextEncoder _TextEncoder;
        private readonly IMailsender _MailSender;
        public MainWindowViewModel(IDataBaseMailSender DBMailSender, ITextEncoder textEncoder, IMailsender mailsender)
        {
            _DbConnect = DBMailSender;
            _TextEncoder = textEncoder;
            _MailSender = mailsender;
            PropertyChanged += BindPasswordAccount;
        }
        private void BindPasswordAccount(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(SelectedSmtpAccount))
            {
                if (SelectedSmtpAccount == null) SelectedPassword = null;
                else
                {
                    SelectedPassword = _TextEncoder.DecodeSecure(SelectedSmtpAccount.Password);
                }
            }
            if (e.PropertyName == nameof(SelectedSmtpServer))
            {
                SelectedSmtpAccount = null;
                
            }
        }
        #region Commands for database connect servise
        private ICommand _LoadDataCommand;
        public ICommand LoadDataCommand => _LoadDataCommand ??= new LambdaCommand(OnLoadDataCommandExecuted);
        private void OnLoadDataCommandExecuted(object p)
        {
            var accounts = _DbConnect.DBGetSmtpAccounts();
            SmtpServers = new ObservableCollection<SmtpServer>(_DbConnect.DBGetSmtpServers());
            MessagePatterns = new ObservableCollection<MessagePattern>(_DbConnect.DBGetMessagePatterns());
            EmailAddresses = new ObservableCollection<EmailAddress>(_DbConnect.DBGetEmailAddresses());
            var messages = _DbConnect.DBGetMessageSendContainers();
            MessageSendContainers = new ObservableCollection<MessageSendContainer>
                (messages.Where(m => m.SendDate > DateTime.Now));
            MessageSendOutContainers = new ObservableCollection<MessageSendContainer>
                (messages.Where(m => m.SendDate <= DateTime.Now));
        }
        private ICommand _AddMessagePattern;
        public ICommand AddMessagePattern => _AddMessagePattern ??= new LambdaCommand(OnAddMessagePatternExecuted);
        private void OnAddMessagePatternExecuted(object p)
        {
            SelectedMessagePattern = null;
            MessagePatterns.Add(_DbConnect.AddDb(new MessagePattern { 
                Subject = "Новая тема", 
                Body = "Введите текст письма..." }));
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
            return SelectedMessagePattern != null; //&&
                //!MessagePatterns.Contains(SelectedMessagePattern);
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
            EmailAddresses.Add(_DbConnect.AddDb(new EmailAddress { 
                Person_Company = "Введите адресат", 
                Email = "login@domainname.com" }));
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
            return SelectedEmailAddress != null; //&&
                //!EmailAddresses.Contains(SelectedEmailAddress);
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
            SelectedSmtpAccount = null;
            SmtpServers.Add(_DbConnect.AddDb(new SmtpServer { 
                Title = "New SmtpServer", 
                SmtpServ = "smtp.domaimname.com", 
                Port = 587, 
                UseSSL = true }));
            SelectedSmtpServer = SmtpServers.OrderBy(t => t.Id).LastOrDefault();
            SelectedSmtpServer.SmtpAccounts = new ObservableCollection<SmtpAccount>();

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
            return SelectedSmtpServer != null; //&&
                //!SmtpServers.Contains(SelectedSmtpServer);
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
            SmtpAccount newAccount = _DbConnect.AddDb(new SmtpAccount { 
                AccountEmail = "login@domainname.com", 
                Password="********", 
                Person_Company="Введите адресат", 
                SmtpServerId = SelectedSmtpServer.Id });
            SelectedSmtpAccount = SelectedSmtpServer.SmtpAccounts.OrderBy(t => t.Id).LastOrDefault();
            //SelectedPassword = _TextEncoder.DecodeSecure(SelectedSmtpAccount.Password);

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
            return SelectedSmtpAccount != null; //&&
                //SelectedSmtpServer.SmtpAccounts.Contains(SelectedSmtpAccount);
        }
        private void OnUpdateSmtpAccountExecuted(object p)
        {
            SelectedSmtpAccount.Password = _TextEncoder.Encode(SelectedPassword);
            _DbConnect.UpdateDb(SelectedSmtpAccount);
            //SelectedPassword = null;
        }
        private ICommand _AddMessageSendContainer;
        public ICommand AddMessageSendContainer => _AddMessageSendContainer ??= new LambdaCommand(OnAddMessageSendContainerExecuted, CanAddMessageSendContainerExecuted);
        private bool CanAddMessageSendContainerExecuted(object p)
        {
            return SelectedSmtpAccountForEmail != null &&
                SelectedSmtpServerForEmail != null &&
                SelectedEmailAddressForEmail != null &&
                SelectedMessagePatternForEmail != null &&
                SelectedDateTimeForEmail != null &&
                SelectedTimeForEmail != null;
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
                EmailAddressesTo = EmailAddressesForEmail,
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
            return SelectedMessageSendContainer != null; //&&
                //!MessageSendContainers.Contains(SelectedMessageSendContainer) &&
                //!MessageSendOutContainers.Contains(SelectedMessageSendContainer);
        }
        private void OnUpdateMessageSendContainerExecuted(object p)
        {
            _DbConnect.UpdateDb(SelectedMessageSendContainer);
        }
        #endregion

        #region Commands for scheduler

        private ICommand _AddEmailAddressToMessage;
        public ICommand AddEmailAddressToMessage => _AddEmailAddressToMessage ??= new LambdaCommand(OnAddEmailAddressToMessageExecuted, CanAddEmailAddressToMessageExecuted);
        private bool CanAddEmailAddressToMessageExecuted(object p)
        {
            return SelectedEmailAddressForEmail != null;
        }
        private void OnAddEmailAddressToMessageExecuted(object p)
        {
            EmailAddressesForEmail += $"{SelectedEmailAddressForEmail.Person_Company}<{SelectedEmailAddressForEmail.Email}>; ";
            SelectedEmailAddressForEmail = null;
        }
        private ICommand _AddCorrectMessagePattern;
        public ICommand AddCorrectMessagePattern => _AddCorrectMessagePattern ??= new LambdaCommand(OnAddCorrectMessagePatternExecuted, CanAddCorrectMessagePatternExecuted);
        private bool CanAddCorrectMessagePatternExecuted(object p)
        {
            return SubjectForEmail != null &&
                BodyForEmail != null &&
                MessagePatterns.Where(m => m.Subject == SubjectForEmail).Count() == 0 &&
                MessagePatterns.Where(m => m.Body == BodyForEmail).Count() == 0;
        }
        private void OnAddCorrectMessagePatternExecuted(object p)
        {
            MessagePatterns.Add(_DbConnect.AddDb(new MessagePattern { Subject = SubjectForEmail
                , Body = BodyForEmail})) ;
            
        }
        private ICommand _AddSubjectBodyForEmail;
        public ICommand AddSubjectBodyForEmail => _AddSubjectBodyForEmail ??= new LambdaCommand(OnAddSubjectBodyForEmailExecuted, CanAddSubjectBodyForEmailExecuted);
        private bool CanAddSubjectBodyForEmailExecuted(object p)
        {
            return SelectedMessagePatternForEmail != null;
        }
        private void OnAddSubjectBodyForEmailExecuted(object p)
        {
            SubjectForEmail = SelectedMessagePatternForEmail.Subject;
            BodyForEmail = SelectedMessagePatternForEmail.Body;
        }
        private ICommand _SendEmail;
        public ICommand SendEmail => _SendEmail ??= new LambdaCommand(OnSendEmailExecuted, CanSendEmailExecuted);
        private bool CanSendEmailExecuted(object p)
        {
            return SelectedSmtpServerForEmail != null &&
                SelectedSmtpAccountForEmail != null &&
                EmailAddressesForEmail != null &&
                SubjectForEmail != null &&
                BodyForEmail != null;
        }
        private void OnSendEmailExecuted(object p)
        {
            var messageEmailOut = _DbConnect.AddDb(new MessageSendContainer
            {
                SmtpServerUse = SelectedSmtpServerForEmail.SmtpServ,
                PortUse = SelectedSmtpServerForEmail.Port,
                SSLUse = SelectedSmtpServerForEmail.UseSSL,
                SmtpAccountEmailUse = SelectedSmtpAccountForEmail.AccountEmail,
                SmtpAccountPasswordUse = SelectedSmtpAccountForEmail.Password,
                SmtpAccountPerson_CompanyUse = SelectedSmtpAccountForEmail.Person_Company,
                EmailAddressesTo = EmailAddressesForEmail,
                Subject = SelectedMessagePatternForEmail.Subject,
                Body = SelectedMessagePatternForEmail.Body,
                SendDate = DateTime.Now,
                Status = "Письмо отправляется"
            });
            var message = _MailSender.SendMessage(messageEmailOut);
            MessageBox.Show($"Письмо '{messageEmailOut.Subject}', " +
                $"запланированное к отправке {messageEmailOut.SendDate:dd.mm.yyyy hh:mm}, " +
                $"{message}", "Отправка почты", MessageBoxButton.OK, MessageBoxImage.Information);
            _DbConnect.UpdateDb(messageEmailOut);
            MessageSendOutContainers.Add(messageEmailOut);
            if (messageEmailOut.Status == "Отправлено.")
            {
                SelectedSmtpServerForEmail = null;
                SelectedSmtpAccountForEmail = null;
                SelectedEmailAddressForEmail = null;
                SelectedMessagePatternForEmail = null;
            }
            
        }
        #endregion
    }
}
