using System;
using System.Net;
using System.Net.Mail;
using System.Windows;

namespace ConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {
            // отправитель - устанавливаем адрес и отображаемое в письме имя
            MailAddress from = new MailAddress("olnestest@mail.ru", "Oleg");
            // кому отправляем
            MailAddress to = new MailAddress("olnestest@yandex.ru");
            // создаем объект сообщения
            MailMessage m = new MailMessage(from, to);
            // тема письма
            m.Subject = "Тест";
            // текст письма
            m.Body = "<h2>Письмо-тест работы smtp-клиента</h2>";
            // письмо представляет код html
            m.IsBodyHtml = true;
            // адрес smtp-сервера и порт, с которого будем отправлять письмо
            SmtpClient smtp = new SmtpClient("smtp.mail.ru", 587);
            // логин и пароль
            smtp.Credentials = new NetworkCredential("olnestest@mail.ru", "Ytcnthjd72");
            smtp.EnableSsl = true;
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.Send(m);
            Console.Read();
        }
    }
}
