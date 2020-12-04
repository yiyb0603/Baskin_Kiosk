using System.Windows;

namespace Baskin_Kiosk.View.MessagePage
{
    /// <summary>
    /// Interaction logic for Message.xaml
    /// </summary>
    public partial class Message : Window
    {
        public Message()
        {
            InitializeComponent();
            this.Loaded += Message_Loaded;
        }

        private void Message_Loaded(object sender, RoutedEventArgs e)
        {
            this.DataContext = App.messageViewModel;
            this.selectListView.ItemsSource = App.messageViewModel.messageList;
        }

        private void sendMessage(object sender, RoutedEventArgs e)
        {
            string message = sendContent.Text;
            bool? isGroup = sendType.IsChecked;
            App.connection.SendMessage(message, isGroup);

            App.messageViewModel.setMessageList(message);
            sendContent.Text = "";
        }
    }
}
