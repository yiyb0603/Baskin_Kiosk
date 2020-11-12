using System;
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

        }

        private void sendMessage(object sender, RoutedEventArgs e)
        {
            String message = sendContent.Text;
            App.connection.sendMessage(message);
            
            ////ServerConnection connection = new ServerConnection();

            //String response = connection.sendMessage(message);

            //if (response == "200")
            //{
            //    MessageBox.Show("메시지 전송 성공.");
            //    sendContent.Text = "";
            //}
        }
    }
}
