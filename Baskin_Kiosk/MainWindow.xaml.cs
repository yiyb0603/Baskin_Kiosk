using Baskin_Kiosk.View.LoginPage;
using Baskin_Kiosk.View.MessagePage;
using Baskin_Kiosk.ViewModel;
using System;
using System.Globalization;
using System.Windows;
using System.Windows.Threading;
using System.Windows.Media;
using Baskin_Kiosk.Network;
using System.Threading;

namespace Baskin_Kiosk
{
    public partial class MainWindow : Window
    {
        private OrderViewModel viewModel = App.orderViewModel;

        BrushConverter converter = new BrushConverter();
        Brush red = null;
        Brush green = null;

        public MainWindow()
        {
            InitializeComponent();
            this.Loaded += MainWindow_Loaded;
            this.Unloaded += MainWindow_Unloaded;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            red = (Brush) converter.ConvertFromString("#e74c3c");
            green = (Brush) converter.ConvertFromString("#2ecc71");

            DispatcherTimer timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromMilliseconds(1000)
            };

            timer.Tick += new EventHandler(TimerTick);
            timer.Start();
            LoginPopup();
            startState();
        }

        private void startState()
        {
            Thread networkThread = new Thread(new ThreadStart(connectState));
            networkThread.IsBackground = true;
            networkThread.Start();
        }

        private void connectState()
        {
            while (true)
            {
                Dispatcher.Invoke(DispatcherPriority.Normal, new Action(delegate
                {
                    serverConnected.Text = ServerConnection.isConnected ? "현재 서버와 연결 되어있습니다." : "현재 서버와 연결 되어있지 않습니다.";
                    serverConnectedDot.Background = ServerConnection.isConnected ? this.green : this.red;
                    connectionBtn.IsEnabled = !ServerConnection.isConnected;
                 }));
            }
        }

        private void MainWindow_Unloaded(object sender, RoutedEventArgs e)
        {
            App.connection.threadEnd();
        }

        private void TimerTick(object sender, EventArgs e)
        {
            string date = DateTime.Now.ToString("yyyy년 MM월 dd일 ddd요일 tt HH시 mm분 ss초", new CultureInfo("ko-KR"));
            CurrentTime.Text = date;
        }

        private void LoginPopup()
        {
            Login login = new Login
            {
                Owner = this
            };

            bool? bResult = login.ShowDialog();
            if (bResult == true)
            {
                new Uri("./View/HomePage/Home.xaml", UriKind.Relative);
                connectedTime.Text = ServerConnection.isConnected ? "최근 서버 접속 시간: " + DateTime.Now.ToString("yyyy년 MM월 dd일 HH시 mm분 ss초") : "";
            }
        }

        private void Home_Click(object sender, RoutedEventArgs e)
        {
            const string HOME_URI = "./View/HomePage/Home.xaml";
            Uri uri = new Uri(HOME_URI, UriKind.Relative);

            if (viewModel.selectMenuList.Count > 0)
            {
                MessageBoxResult confirmDialog = MessageBox.Show("주문을 취소하시겠습니까?", "잠시만요", MessageBoxButton.YesNo);

                if (confirmDialog == MessageBoxResult.Yes)
                {
                    viewModel.clearMenuList();
                    frame.Source = uri;
                }

                return;
            }

            frame.Source = uri;
        }

        private void Message_Click(object sender, RoutedEventArgs e)
        {
            if (ServerConnection.isConnected)
            {
                Message message = new Message();
                message.Show();
            } else
            {
                MessageBox.Show("현재 서버가 작동중이지 않습니다.");
            }
        }

        private void TryConnection(object sender, RoutedEventArgs e)
        {
            string response = App.connection.sendMessage();

            if (response == "200")
            {
                App.connection.threadStart();
                ServerConnection.isConnected = true;
                connectedTime.Text = ServerConnection.isConnected ? "최근 서버 접속 시간: " + DateTime.Now.ToString("yyyy년 MM월 dd일 HH시 mm분 ss초") : "";
            }
        }
    }
}
