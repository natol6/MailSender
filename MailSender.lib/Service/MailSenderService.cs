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
        public static string SendMessage(MessageSendContainer msc)
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
            using var client = new SmtpClient();
            client.Connect(msc.SmtpServerUse, msc.PortUse, msc.SSLUse);
            client.Authenticate(msc.SmtpAccountEmailUse, TextEncoder.Decode(msc.SmtpAccountPasswordUse));
            try
            {
                client.Send(message);
                client.Disconnect(true);
                msc.Status = "Письмо отправлено";
                return $"Отправлено адресатам письмо: '{msc.Subject}'";
            }
            catch (ObjectDisposedException)
            {
                msc.Status = "Письмо не отправлено. Утилизировано.";
                return $"Не отправлено (утилизировано) письмо: '{msc.Subject}'";
            }
            catch (ServiceNotConnectedException)
            {
                msc.Status = "Письмо не отправлено. Ошибка соединения с сервером.";
                return $"Не отправлено (ошибка соединения с сервером) письмо: '{msc.Subject}'";
            }
            catch (ServiceNotAuthenticatedException)
            {
                msc.Status = "Письмо не отправлено. Ошибка аутентификации.";
                return $"Не отправлено (ошибка аутентификации) письмо: '{msc.Subject}'";
            }
            catch (InvalidOperationException)
            {
                msc.Status = "Письмо не отправлено. Не указаны отправитель или адресаты.";
                return $"Не отправлено (не указан отправитель) письмо: '{msc.Subject}'";
            }
            catch (OperationCanceledException)
            {
                msc.Status = "Письмо не отправлено. Операция отменена.";
                return $"Не отправлено (операция отменена) письмо: '{msc.Subject}'";
            }
            catch (ProtocolException)
            {
                msc.Status = "Письмо не отправлено. Ошибка отправки.";
                return $"Не отправлено (ошибка отправки) письмо: '{msc.Subject}'";
            }
        }

        
    }
}
