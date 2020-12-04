using Baskin_Kiosk.View.OrderPage;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Baskin_Kiosk.View.HomePage
{
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

        private void NextPage(object sender, RoutedEventArgs e)
        {
            Order order = new Order();
            NavigationService.Navigate(order);
        }
    }
}
