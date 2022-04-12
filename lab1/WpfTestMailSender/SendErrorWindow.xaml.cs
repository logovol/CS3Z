using System.Windows;

namespace WpfTestMailSender
{
    /// <summary>
    /// Логика взаимодействия для SendErrorWindow.xaml
    /// </summary>
    public partial class SendErrorWindow : Window
    {
        public SendErrorWindow()
        {
            InitializeComponent();
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
