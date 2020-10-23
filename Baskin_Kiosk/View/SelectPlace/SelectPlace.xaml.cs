using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Baskin_Kiosk.View.SelectPlace
{
    /// <summary>
    /// Interaction logic for SelectPlace.xaml
    /// </summary>
    public partial class SelectPlace : Page
    {
        public SelectPlace()
        {
            InitializeComponent();
        }

        private void SeatButton_Click(object sender, RoutedEventArgs e)
        {
            Seat seat = new Seat();
            NavigationService.Navigate(seat);
        }

        private void TakeOutButton_Click(object sender, RoutedEventArgs e)
        {
            PaymentPage.Payment payment = new PaymentPage.Payment();
            NavigationService.Navigate(payment);
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            if (NavigationService.CanGoBack)
            {
                NavigationService.GoBack();
            }
        }
    }
}
