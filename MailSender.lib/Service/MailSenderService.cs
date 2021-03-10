using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MailSender.lib.Entities;
using System.Security;
using MailKit.Net.Smtp;
using MailKit;
using MimeKit;
using MailSender.lib.Interfaces;



namespace MailSender.lib.Service

{
    public class MailSenderService : IMailsender
    {
        private readonly ITextEncoder _TextEncoder;
        public MailSenderService(ITextEncoder textEnc)
        {
            _TextEncoder = textEnc;
        }
        public string SendMessage(MessageSendContainer msc)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(msc.SmtpAccountPerson_CompanyUse, 
                msc.SmtpAccountEmailUse));
            InternetAddressList addresses = new InternetAddressList();
            bool yes = InternetAddressList.TryParse(ParserOptions.Default , msc.EmailAddressesTo, out addresses);
            if (!yes) return $"Не отправлено (не указаны адресаты) письмо: '{msc.Subject}'";
            message.To.AddRange(addresses);
            message.Subject = msc.Subject;
            message.Body = new TextPart("plain") { Text = msc.Body };
                       
            try
            {
                using var client = new SmtpClient();
                client.Connect(msc.SmtpServerUse, msc.PortUse, msc.SSLUse);
                client.Authenticate(msc.SmtpAccountEmailUse, _TextEncoder.Decode(msc.SmtpAccountPasswordUse));
                client.Send(message);
                client.Disconnect(true);
                msc.Status = "2";
                return $"отправлено адресатам.";
            }
            catch (ObjectDisposedException)
            {
                msc.Status = "0";
                return $"не отправлено (утилизировано).";
            }
            catch (ServiceNotConnectedException)
            {
                msc.Status = "0";
                return $"не отправлено (ошибка соединения с сервером).";
            }
            catch (ServiceNotAuthenticatedException)
            {
                msc.Status = "0";
                return $"не отправлено (ошибка аутентификации).";
            }
            catch (InvalidOperationException)
            {
                msc.Status = "0";
                return $"не отправлено (не указан отправитель).";
            }
            catch (OperationCanceledException)
            {
                msc.Status = "0";
                return $"не отправлено (операция отменена).";
            }
            catch (ProtocolException)
            {
                msc.Status = "0";
                return $"не отправлено (ошибка отправки).";
            }
        }

        
    }
}
