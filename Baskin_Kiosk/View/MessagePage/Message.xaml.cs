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
            App.connection.threadStart();
            this.DataContext = App.messageViewModel;
            this.selectListView.ItemsSource = App.messageViewModel.messageList;
            App.messageViewModel.messageList.CollectionChanged += MessageList_CollectionChanged;
        }

        private void MessageList_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            for (int i = 0; i < App.messageViewModel.getMessageList().Count; i++)
            {
                MessageBox.Show(App.messageViewModel.getMessageList()[i]);
            }

            App.Current.Dispatcher.Invoke((Action)delegate
            {
                this.selectListView.ItemsSource = App.messageViewModel.getMessageList();
            });
        }

        private void sendMessage(object sender, RoutedEventArgs e)
        {
            string message = sendContent.Text;
            App.connection.sendMessage(message);
            sendContent.Text = "";
        }
    }
}
