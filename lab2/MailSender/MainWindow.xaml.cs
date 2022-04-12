using System;
using System.Net.Mail;
using System.Windows;
using System.Windows.Threading;
using MailSender.lib;
using MailSender.Models;

namespace MailSender
{
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();

            //ServersList.ItemsSource = TestData.Servers;

            DispatcherTimer timer = new DispatcherTimer(TimeSpan.FromSeconds(1), DispatcherPriority.Normal,
                                                        delegate
                                                        {
                                                            int newvalue = 0;

                                                            if (Counter == int.MaxValue)
                                                            {
                                                                newvalue = 0;
                                                            }
                                                            else
                                                            {
                                                                newvalue = Counter + 1;
                                                            }

                                                            SetValue(CounterProperty, newvalue);
                                                        }, Dispatcher);

        }

        public int Counter
        {
            get { return (int)GetValue(CounterProperty); }
            set { SetValue(CounterProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CounterProperty =
            DependencyProperty.Register("Counter", typeof(int), typeof(MainWindow), new PropertyMetadata(0));

        private void OnSendButtonClick(object Sender, RoutedEventArgs E)
        {
            var sender = SendersList.SelectedItem as Sender;
            if (sender is null) return;

            if (!(RecipientsList.SelectedItem is Recipient recipient)) return;
            if (!(ServersList.SelectedItem is Server server)) return;
            if (!(MessagesList.SelectedItem is Message message)) return;

            var send_service = new MailSenderService
            {
                ServerAddress = server.Address,
                ServerPort = server.Port,
                UseSSL = server.UseSSL,
                Login = server.Login,
                Password = server.Password,
            };

            try
            {
                send_service.SendMessage(sender.Address, recipient.Address, message.Subject, message.Body);
            }
            catch (SmtpException error)
            {
                MessageBox.Show(
                    "Ошибка при отправке почты " + error.Message, "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void tscTabSwitcher_btnNextClick(object sender, RoutedEventArgs e)
        {
            DateTime? selectedDate = Calendar.SelectedDate is null ? DateTime.Today : Calendar.SelectedDate;
            var date = selectedDate.Value;
            int daysInMonth = DateTime.DaysInMonth(date.Year, date.Month);

            if (date.Day == daysInMonth && date.Month == 12)
            {
                Calendar.SelectedDate = new DateTime(date.Year + 1, 1, 1);
                Calendar.DisplayDate = new DateTime(date.Year + 1, 1, 1);
            }
            else if (date.Day == daysInMonth)
            {
                Calendar.SelectedDate = new DateTime(date.Year, date.Month + 1, 1);
                Calendar.DisplayDate = new DateTime(date.Year, date.Month + 1, 1);
            }

            else
                Calendar.SelectedDate = new DateTime(date.Year, date.Month, date.Day + 1);
        }

        private void tscTabSwitcher_btnPreviousClick(object sender, RoutedEventArgs e)
        {
            DateTime? selectedDate = Calendar.SelectedDate is null ? DateTime.Today : Calendar.SelectedDate;
            var date = selectedDate.Value;
            int daysInMonth = DateTime.DaysInMonth(date.Year, date.Month);

            if (date.Day == 1 && date.Month == 1)
            {
                Calendar.SelectedDate = new DateTime(date.Year - 1, 12, DateTime.DaysInMonth(date.Year, date.Month - 1));
                Calendar.DisplayDate = new DateTime(date.Year - 1, 12, DateTime.DaysInMonth(date.Year, date.Month - 1));
            }
            else if (date.Day == 1)
            {
                Calendar.SelectedDate = new DateTime(date.Year, date.Month - 1, DateTime.DaysInMonth(date.Year, date.Month - 1));
                Calendar.DisplayDate = new DateTime(date.Year, date.Month - 1, DateTime.DaysInMonth(date.Year, date.Month - 1));
            }
            else
                Calendar.SelectedDate = new DateTime(date.Year, date.Month, date.Day - 1);

        }
    }
}
