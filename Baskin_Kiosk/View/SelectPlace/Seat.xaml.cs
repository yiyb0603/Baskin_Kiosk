using Baskin_Kiosk.Common;
using Baskin_Kiosk.Model;
using Baskin_Kiosk.ViewModel;
using Google.Protobuf;
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
using System.Windows.Threading;

namespace Baskin_Kiosk.View.SelectPlace
{
    public partial class Seat : Page
    {
        List<SeatViewModel> lstSeat = new List<SeatViewModel>();

        Dictionary<Button, int> buttonPair;
        private OrderViewModel viewModel = App.orderViewModel;

        int temp = 0;

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
        
        private void SeatButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;

            MessageBoxResult result = MessageBox.Show(buttonPair[button] + "번 자리를 선택하시겠어요?", "확인", MessageBoxButton.YesNo);

            if(result==MessageBoxResult.Yes)
            {
                foreach(OrderModel orderModel in viewModel.orderMenuList)
                {
                    orderModel.seatId = buttonPair[button];
                    MessageBox.Show(orderModel.seatId + ", " + buttonPair[button].ToString());
                }

                PaymentPage.Payment payment = new PaymentPage.Payment();
                NavigationService.Navigate(payment);
            }
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            if (NavigationService.CanGoBack)
            {
                NavigationService.GoBack();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void seatList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SeatViewModel seatViewModel = (SeatViewModel) seatList.SelectedItem;
            MessageBox.Show(seatViewModel.seatNumber.ToString());
        }
    }
}
