using System.Collections.Generic;
using System.Net.Mail;

namespace WpfTestMailSender
{
    public class Settings
    {
        public string SmtpAddress { get; set; } = "smtp.yandex.ru";
        public int SmtpPort { get; set; } = 25;
        public MailAddress From { get; set; } = new MailAddress("logovol@yandex.ru", "Mambo");
        public List<MailAddress> To { get; set; } = new List<MailAddress> { new MailAddress("pasha1985@inbox.ru", "Паша") };
        
    }
}
