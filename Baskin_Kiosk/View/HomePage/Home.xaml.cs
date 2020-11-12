using Baskin_Kiosk.View.OrderPage;
using System;
using System.Windows;
using System.Windows.Controls;

namespace Baskin_Kiosk.View.HomePage
{
    /// <summary>
    /// Home.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class Home : Page
    {
        public Home()
        {
            InitializeComponent();

            BaskinCf.Play();
        }

        private void RepeatMedia(object sender, RoutedEventArgs e)
        {
            BaskinCf.Position = new TimeSpan(0, 0, 1);

            BaskinCf.Play();
        }

        private void nextPage(object sender, RoutedEventArgs e)
        {
            Order order = new Order();
            this.NavigationService.Navigate(order);
        }
    }
}
