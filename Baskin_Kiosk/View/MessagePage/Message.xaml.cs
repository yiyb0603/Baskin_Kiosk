using Baskin_Kiosk.Util;
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
        }

        private void sendMessage(object sender, RoutedEventArgs e)
        {
            String message = sendContent.Text;
            ServerConnection connection = new ServerConnection();

            String response = connection.sendMessage(message);

            if (response == "200")
            {
                MessageBox.Show("메시지 전송 성공.");
                message = "";
            }
        }
    }
}
