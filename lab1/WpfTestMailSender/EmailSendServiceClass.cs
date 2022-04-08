using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Security;

namespace WpfTestMailSender
{
    public class EmailSendServiceClass
    {

        public string Subject { get; set; }
        public string Body { get; set; }
        
        public SecureString SecurePassword { get; set; }

        public EmailSendServiceClass(string _subject, string _body, SecureString _password)
        {
            Subject        = _subject;
            Body           = _body;
            SecurePassword = _password;
        }

        public string Send()
        {
            Settings st = new Settings();
            List<MailAddress> listStrMails = st.To;// Список email'ов //кому мы отправляем письмо
            foreach (var mail in listStrMails)
            {
                // Используем using, чтобы гарантированно удалить объект MailMessage после использования                
                using (MailMessage mm = new MailMessage(st.From, mail))
                {
                    // Формируем письмо
                    mm.Subject = Subject; // Заголовок письма
                    mm.Body = Body;       // Тело письма
                    mm.IsBodyHtml = false;           // Не используем html в теле письма
                                                     // Авторизуемся на smtp-сервере и отправляем письмо
                                                     // Оператор using гарантирует вызов метода Dispose, даже если при вызове 
                                                     // методов в объекте происходит исключение.
                    using (SmtpClient sc = new SmtpClient(st.SmtpAddress, st.SmtpPort))
                    {
                        sc.EnableSsl = true;
                        sc.Credentials = new NetworkCredential(st.From.Address, SecurePassword);
                        try
                        {
                            sc.Send(mm);
                        }
                        catch (Exception ex)
                        {
                            return $"Невозможно отправить письмо {ex.ToString()}";
                        }
                    }
                }
            }
            return "";
        }
    }
}
