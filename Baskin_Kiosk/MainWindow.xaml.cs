using Baskin_Kiosk.View.LoginPage;
using Baskin_Kiosk.View.MessagePage;
using Baskin_Kiosk.ViewModel;
using System;
using System.Globalization;
using System.Windows;
using System.Windows.Threading;

namespace Baskin_Kiosk
{
    public partial class MainWindow : Window
    {
        private OrderViewModel viewModel = App.orderViewModel;

        public MainWindow()
        {
            InitializeComponent();
            this.Loaded += MainWindow_Loaded;
            this.Unloaded += MainWindow_Unloaded;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            DispatcherTimer timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromMilliseconds(1000)
            };

            timer.Tick += new EventHandler(TimerTick);
            timer.Start();
            LoginPopup();
        }

        private void MainWindow_Unloaded(object sender, RoutedEventArgs e)
        {
            App.connection.threadEnd();
        }

        private void TimerTick(object sender, EventArgs e)
        {
            string date = DateTime.Now.ToString("yyyy년 MM월 dd일 tt HH시 mm분 ss초 ddd요일", new CultureInfo("ko-KR"));
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
            }
        }

        private void Home_Click(object sender, RoutedEventArgs e)
        {
            Uri uri = new Uri("./View/HomePage/Home.xaml", UriKind.Relative);

            if (viewModel.selectMenuList.Count > 0)
            {
                var confirmDialog = MessageBox.Show("주문을 취소하시겠습니까?", "잠시만요", MessageBoxButton.YesNo);

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
            if (App.connection.isConnected)
            {
                Message message = new Message();
                message.Show();
            } else
            {
                MessageBox.Show("현재 서버가 작동중이지 않습니다.");
            }
        }
    }
}
