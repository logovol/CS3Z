using System;
using System.Net;
using System.Net.Mail;

namespace ConsoleTests
{
    class Program
    {
        static void Main(string[] args)
        {
            var to = new MailAddress("email", "");
            var from = new MailAddress("email", "");

            var message = new MailMessage(from, to);
            //var msg = new MailAddress("user@server.ru", "qwe@ASD.ru");

            message.Subject = "Заголовок письма от " + DateTime.Now;
            message.Body = "Текст тестового письма + " + DateTime.Now;

            var client = new SmtpClient("smtp.yandex.ru", 25);
            client.EnableSsl = true;

            client.Credentials = new NetworkCredential
            {
                UserName = "userName",
                Password = "PassWord!"
            };

            client.Send(message);

            Console.WriteLine("!!!");
        }
    }
}
