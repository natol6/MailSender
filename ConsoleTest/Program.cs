using System;
//using System.Net;
//using System.Net.Mail;
using System.Windows;
using MailKit;
using MimeKit;
using MailKit.Net.Smtp;

namespace ConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {
            //SendMessageNetMail();
            SendMessageMailKit();
            Console.Read();
        }
        public static void SendMessageMailKit()
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Олег Нестеров",
                "olnestest@mail.ru"));
            message.To.Add(new MailboxAddress("Oleg", "olnestest@yandex.ru"));
            message.Subject = "Proba ot MailKit";
            message.Body = new TextPart("plain") { Text = "Это пробное письмо......." };
            using var client = new SmtpClient();
            client.Connect("smtp.mail.ru", 465, true);
            client.Authenticate("olnestest@mail.ru", "Ytcnthjd72");
            try
            {
                client.Send(message);
                client.Disconnect(true);
                Console.WriteLine("Письмо отправлено");
            }
            catch (ObjectDisposedException)
            {
                Console.WriteLine("Письмо не отправлено. Утилизировано.");
            }
            catch (ServiceNotConnectedException)
            {
                Console.WriteLine("Письмо не отправлено. Ошибка соединения с сервером.");
            }
            catch (ServiceNotAuthenticatedException)
            {
                Console.WriteLine("Письмо не отправлено. Ошибка аутентификации.");
            }
            catch (InvalidOperationException)
            {
                Console.WriteLine("Письмо не отправлено. Не указаны отправитель или адресаты.");
            }
            catch (OperationCanceledException)
            {
                Console.WriteLine("Письмо не отправлено. Операция отменена.");
            }
            catch (ProtocolException)
            {
                Console.WriteLine("Письмо не отправлено. Ошибка отправки.");
            }
        }
        private static void SendMessageNetMail()
        {
            /*// отправитель - устанавливаем адрес и отображаемое в письме имя
            MailAddress from = new MailAddress("natol654@yandex.ru", "Олег Нестеров");
            // кому отправляем
            MailAddress to = new MailAddress("olnestest@mail.ru");
            // создаем объект сообщения
            MailMessage m = new MailMessage(from, to);
            // тема письма
            m.Subject = "Тест";
            // текст письма
            m.Body = "<h2>Письмо-тест работы smtp-клиента</h2>";
            // письмо представляет код html
            m.IsBodyHtml = true;
            // адрес smtp-сервера и порт, с которого будем отправлять письмо
            SmtpClient smtp = new SmtpClient("smtp.yandex.ru", 587);
            // логин и пароль
            smtp.Credentials = new NetworkCredential("natol654@yandex.ru", "Ytcnthjd72");
            smtp.EnableSsl = true;
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.UseDefaultCredentials = false;
            smtp.Send(m);*/
        }
    }
}
