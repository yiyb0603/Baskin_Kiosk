using Baskin_Kiosk.Model;
using Baskin_Kiosk.ViewModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace Baskin_Kiosk.View.SelectPlace
{
    public partial class Seat : Page
    {
        private readonly OrderViewModel viewModel = App.orderViewModel;

        public Seat()
        {
            InitializeComponent();

            NextButton.IsEnabled = false;
            seatList.ItemsSource = App.lstSeat;
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            if (NavigationService.CanGoBack)
            {
                NavigationService.GoBack();
            }
        }

        private void NextButton_Click(object sender, RoutedEventArgs e)
        {
            foreach (OrderModel orderModel in viewModel.orderMenuList)
            {
                orderModel.seatId = App.selectedSeat.seatNumber;
            }

            PaymentPage.Payment payment = new PaymentPage.Payment();
            NavigationService.Navigate(payment);
        }

        private void SeatList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            App.selectedSeat = (SeatModel)seatList.SelectedItem;
            NextButton.IsEnabled = true;
        }
    }
}