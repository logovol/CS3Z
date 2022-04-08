using System.Windows;

namespace WpfTestMailSender
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class WpfMailSender : Window
    {
        public WpfMailSender()
        {
            InitializeComponent();
        }

        private void btnSendEmail_Click(object sender, RoutedEventArgs e)
        {

            EmailSendServiceClass em = new EmailSendServiceClass(tbSubject.Text, tbBody.Text, passwordBox.SecurePassword);            
            string result = em.Send();
            if (result != string.Empty)
            {
                SendErrorWindow sw = new SendErrorWindow();
                sw.textInfo.Text = result;
                sw.ShowDialog();
            }
            else
            {
                SendEndWindow sew = new SendEndWindow();
                sew.ShowDialog();
            }

        }
    }
}
