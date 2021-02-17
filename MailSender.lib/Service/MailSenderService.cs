using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MailSender.lib.Models;
using System.Security;
using MailKit.Net.Smtp;
using MailKit;
using MimeKit;


namespace MailSender.lib.Service
{
    public static class MailSenderService
    {
        public static void SendMessage(MessageSendContainer msc)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(msc.SmtpAccountUse.Person_Company, 
                $"{msc.SmtpAccountUse.Login}{msc.SmtpServerUse.DomainName}"));
            foreach (EmailAddress email in msc.EmailAddresses)
                message.To.Add(new MailboxAddress(email.Person_Company, email.Email));
            message.Subject = msc.Subject;
            message.Body = new TextPart("plain") { Text = msc.Body };
            using var client = new SmtpClient();
            client.Connect(msc.SmtpServerUse.SmtpServ, msc.SmtpServerUse.Port, msc.SmtpServerUse.UseSSL);
            client.Authenticate($"{msc.SmtpAccountUse.Login}{msc.SmtpServerUse.DomainName}", TextEncoder.Decode(msc.SmtpAccountUse.Password));
            try
            {
                client.Send(message);
                client.Disconnect(true);
                msc.Status = "Письмо отправлено";
            }
            catch (ObjectDisposedException)
            {
                msc.Status = "Письмо не отправлено. Утилизировано.";
            }
            catch (ServiceNotConnectedException)
            {
                msc.Status = "Письмо не отправлено. Ошибка соединения с сервером.";
            }
            catch (ServiceNotAuthenticatedException)
            {
                msc.Status = "Письмо не отправлено. Ошибка аутентификации.";
            }
            catch (InvalidOperationException)
            {
                msc.Status = "Письмо не отправлено. Не указаны отправитель или адресаты.";
            }
            catch (OperationCanceledException)
            {
                msc.Status = "Письмо не отправлено. Операция отменена.";
            }
            catch (ProtocolException)
            {
                msc.Status = "Письмо не отправлено. Ошибка отправки.";
            }
        }
    }
}
