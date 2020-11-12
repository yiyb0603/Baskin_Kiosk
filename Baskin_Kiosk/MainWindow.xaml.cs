using Baskin_Kiosk.View.LoginPage;
using Baskin_Kiosk.ViewModel;
using System;
using System.Windows;
using System.Windows.Threading;

namespace Baskin_Kiosk
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private OrderViewModel viewModel = App.orderViewModel;

        public MainWindow()
        {
            InitializeComponent();
            DispatcherTimer timer = new DispatcherTimer();

            timer.Interval = TimeSpan.FromMilliseconds(1000);
            timer.Tick += new EventHandler(timerTick);
            timer.Start();

            this.Loaded += MainWindow_Loaded;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            LoginPopup();
        }

        private void timerTick(object sender, EventArgs e)
        {
            CurrentTime.Content = DateTime.Now.ToString("yyyy년 MM월 dd일 HH시 mm분 ss초");
        }

        private void LoginPopup()
        {
            Login login = new Login();
            login.Owner = this;
            bool? bResult = login.ShowDialog();

            if(bResult == true)
            {
                Uri uri = new Uri("./View/HomePage/Home.xaml", UriKind.Relative);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Uri uri = new Uri("./View/HomePage/Home.xaml", UriKind.Relative); //Line 1

            if (this.viewModel.selectMenuList.Count > 0)
            {
                var confirmDialog = MessageBox.Show("주문을 취소하시겠습니까?", "잠시만요", MessageBoxButton.YesNo);
                
                if (confirmDialog == MessageBoxResult.Yes)
                {
                    this.viewModel.clearMenuList();
                    this.frame.Source = uri;
                }

                return;
            }

            this.frame.Source = uri;
        }
    }
}
