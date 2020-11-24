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
        private OrderViewModel viewModel = App.orderViewModel;
        public static List<SeatModel> lstSeat = new List<SeatModel>();
        public static SeatModel selectedSeat;

        public Seat()
        {
            InitializeComponent();

            lstSeat.Add(new SeatModel() { seatNumber = 1, time = 0 });
            lstSeat.Add(new SeatModel() { seatNumber = 2, time = 0 });
            lstSeat.Add(new SeatModel() { seatNumber = 3, time = 0 });
            lstSeat.Add(new SeatModel() { seatNumber = 4, time = 0 });
            lstSeat.Add(new SeatModel() { seatNumber = 5, time = 0 });
            lstSeat.Add(new SeatModel() { seatNumber = 6, time = 0 });
            lstSeat.Add(new SeatModel() { seatNumber = 7, time = 0 });
            lstSeat.Add(new SeatModel() { seatNumber = 8, time = 0 });
            lstSeat.Add(new SeatModel() { seatNumber = 9, time = 0 });

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

        private void SeatList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedSeat = (SeatModel)seatList.SelectedItem;
        }
    }
}