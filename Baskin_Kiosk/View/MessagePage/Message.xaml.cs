using System;
using System.Collections.Specialized;
using System.Windows;
using System.Windows.Controls;

namespace Baskin_Kiosk.View.MessagePage
{
    /// <summary>
    /// Interaction logic for Message.xaml
    /// </summary>
    public partial class Message : Page
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
            App.connection.sendMessage(message);
            sendContent.Text = "";
        }
    }
}
