using Baskin_Kiosk.Model;
using Baskin_Kiosk.ViewModel;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace Baskin_Kiosk.View.SelectPlace
{
    public partial class Seat : Page
    {
        List<SeatViewModel> lstSeat = new List<SeatViewModel>();

        private OrderViewModel viewModel = App.orderViewModel;
        SeatViewModel selectedSeat;

        public Seat()
        {
            InitializeComponent();

            lstSeat.Add(new SeatViewModel() { seatNumber = 1, time = 5 });
            lstSeat.Add(new SeatViewModel() { seatNumber = 2, time = 3 });
            lstSeat.Add(new SeatViewModel() { seatNumber = 3, time = 4 });
            lstSeat.Add(new SeatViewModel() { seatNumber = 4, time = 2 });
            lstSeat.Add(new SeatViewModel() { seatNumber = 5, time = 1 });
            lstSeat.Add(new SeatViewModel() { seatNumber = 6, time = 5 });
            lstSeat.Add(new SeatViewModel() { seatNumber = 7, time = 3 });
            lstSeat.Add(new SeatViewModel() { seatNumber = 8, time = 4 });
            lstSeat.Add(new SeatViewModel() { seatNumber = 9, time = 2 });

            seatList.ItemsSource = lstSeat;
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
                orderModel.seatId = selectedSeat.seatNumber;
            }

            PaymentPage.Payment payment = new PaymentPage.Payment();
            NavigationService.Navigate(payment);
        }

        private void seatList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedSeat = (SeatViewModel) seatList.SelectedItem;
        }
    }
}
