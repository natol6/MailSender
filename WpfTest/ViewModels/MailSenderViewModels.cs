using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Collections.ObjectModel;
using WpfTest.Models;
using System.Security;
using System.Net;
using System.Net.Mail;
using System.Windows;


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
        private MailService selectedMailServiceForEmail;
        public MailService SelectedMailServiceForEmail
        {
            get => selectedMailServiceForEmail;
            set
            {
                selectedMailServiceForEmail = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SelectedMailServiceForEmail)));
            }
        }
        private MessagePattern selectedMessagePatternForEmail;
        public MessagePattern SelectedMessagePatternForEmail
        {
            get => selectedMessagePatternForEmail;
            set
            {
                selectedMessagePatternForEmail = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SelectedMessagePatternForEmail)));
            }
        }
        private EmailAddress selectedEmailAddressForEmail;
        public EmailAddress SelectedEmailAddressForEmail
        {
            get => selectedEmailAddressForEmail;
            set
            {
                selectedEmailAddressForEmail = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SelectedEmailAddressForEmail)));
            }
        }
        private Account selectedAccountForEmail;
        public Account SelectedAccountForEmail
        {
            get => selectedAccountForEmail;
            set
            {
                selectedAccountForEmail = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SelectedAccountForEmail)));
            }
        }
        private DBConnectMailSender dbconnectMS = new DBConnectMailSender();
        private DBConnectAddressBook dbconnectAB;
        public bool UseSSL { get; set; } = true;
        public MailSenderViewModels()
        {
            LoadMailSet();
        }
        public void LoadMailSet()
        {
            
            var accounts = dbconnectMS.DbGetAccounts();
            var mailservices = dbconnectMS.DbGetMailServices();
            foreach (MailService ms in mailservices)
                MailServices.Add(new MailService
                {
                    Id = ms.Id,
                    Title = ms.Title,
                    SmtpServer = ms.SmtpServer,
                    DomainName = ms.DomainName 
                });
            foreach (MailService ms in MailServices)
            {
                var acc = accounts.Where(a => a.MailServiceId == ms.Id);
                foreach(Account a in acc)
                {
                    ms.Accounts.Add(a);
                }
            }
            var messagepatterns = dbconnectMS.DbGetMessagePattern();
            foreach (MessagePattern mp in messagepatterns)
                MessagePatterns.Add(mp);
        }
        public void SendMessage()
        {
            var from = new MailAddress(string.Format(@"{0}{1}", SelectedAccountForEmail.Login, SelectedMailServiceForEmail.DomainName),
                SelectedAccountForEmail.Person);
            var to = new MailAddress(SelectedEmailAddressForEmail.EMail, SelectedEmailAddressForEmail.Person);

            var message = new MailMessage(from, to);
            message.Subject = SelectedMessagePatternForEmail.Subject;
            message.Body = SelectedMessagePatternForEmail.Body;
            var client = new SmtpClient(SelectedMailServiceForEmail.SmtpServer, 25);
                client.EnableSsl = true;
            if (!UseSSL)
            {
                client = new SmtpClient(SelectedMailServiceForEmail.SmtpServer, 587);
                client.EnableSsl = false;
            }
            client.Credentials = new NetworkCredential(string.Format(@"{0}{1}", SelectedAccountForEmail.Login, SelectedMailServiceForEmail.DomainName), SelectedAccountForEmail.SecurePassword);
            
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;
            try
            {
                client.Send(message);

                MessageBox.Show("Почта успешно отправлена!", "Отправка почты", MessageBoxButton.OK, MessageBoxImage.Information);

            }
            catch (SmtpException)
            {
                MessageBox.Show("Ошибка авторизации", "Ошибка отправки почты", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            catch (TimeoutException)
            {
                MessageBox.Show("Ошибка адреса сервера", "Ошибка отправки почты", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        #region InteractionDBMailSender
        public void AddMessagePattern()
        {
            SelectedMessagePattern = null;
            MessagePatterns.Add(dbconnectMS.AddBdMessagePattern(new MessagePattern { Subject = "Новая тема", Body = ""}));
            SelectedMessagePattern = MessagePatterns.OrderBy(t => t.Id).LastOrDefault();

        }
        public void DeleteMessagePattern()
        {
            dbconnectMS.DeleteBdMessagePattern(SelectedMessagePattern.Id);
            MessagePatterns.Remove(SelectedMessagePattern);
            SelectedMessagePattern = null;
        }
        public void UpdateMessagePattern()
        {
            dbconnectMS.UpdateBdMessagePattern(SelectedMessagePattern);

        }
        public void AddAccount()
        {
            SelectedAccount = null;
            Account ac = dbconnectMS.AddBdAccount(new Account { Login = "input_login", MailServiceId = SelectedMailService.Id });
            SelectedMailService.Accounts.Add(ac);
            SelectedAccount = SelectedMailService.Accounts.OrderBy(t => t.Id).LastOrDefault();

        }
        public void DeleteAccount()
        {
            dbconnectMS.DeleteBdAccount(SelectedAccount.Id);
            SelectedMailService.Accounts.Remove(SelectedAccount);
            SelectedAccount = null;
        }
        public void UpdateAccount()
        {
            dbconnectMS.UpdateBdAccount(SelectedAccount);

        }
        public void AddMailServise()
        {
            SelectedAccount = null;
            SelectedMailService = null;
            MailService ms = dbconnectMS.AddBdMailService(new MailService { Title = "New MailService" });
            MailServices.Add(ms);
            SelectedMailService = MailServices.OrderBy(t => t.Id).LastOrDefault();

        }
        public void DeleteMailServise()
        {
            SelectedAccount = null;
            foreach(Account ac in SelectedMailService.Accounts)
            {
                dbconnectMS.DeleteBdAccount(ac.Id);
            }
            dbconnectMS.DeleteBdMailService(SelectedMailService.Id);
            MailServices.Remove(SelectedMailService);
            SelectedMailService = null;
        }
        public void UpdateMailService()
        {
            dbconnectMS.UpdateBdMailService(SelectedMailService);

        }
        #endregion
        #region ConnectAddressBook
        public void LoadAddressBook_DB()
        {
            dbconnectAB = new DBConnectAddressBook(true);
            var emailaddresses = dbconnectAB.DbGetEmailAddress();
            foreach (EmailAddress em in emailaddresses)
                EmailAddresses.Add(em);
        }
        public void LoadAddressBook_Other()
        {
            dbconnectAB = new DBConnectAddressBook(false);
            var emailaddresses = dbconnectAB.DbGetEmailAddressOther();
            foreach (EmailAddress em in emailaddresses)
                EmailAddresses.Add(em);
        }
        public void AddEmailAddress()
        {
            SelectedEmailAddress = null;
            EmailAddress email;
            if (dbconnectAB.InternalDB) email = dbconnectAB.AddBdEmailAddress(new EmailAddress { EMail = "login@domainname.com" });
            else email = new EmailAddress { EMail = "login@domainname.com" };
            EmailAddresses.Add(email);
            SelectedEmailAddress = EmailAddresses.OrderBy(t => t.Id).LastOrDefault();

        }
        public void DeleteEmailAddress()
        {
            if(dbconnectAB.InternalDB) dbconnectAB.DeleteBdEmailAddress(SelectedEmailAddress.Id);
            EmailAddresses.Remove(SelectedEmailAddress);
            SelectedEmailAddress = null;
        }
        public void UpdateEmailAddress()
        {
            if (dbconnectAB.InternalDB) dbconnectAB.UpdateBdEmailAddress(SelectedEmailAddress);

        }
        #endregion
    }
}
