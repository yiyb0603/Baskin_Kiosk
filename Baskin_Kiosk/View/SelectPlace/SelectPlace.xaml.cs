using Baskin_Kiosk.Common;
using Baskin_Kiosk.Model;
using Baskin_Kiosk.ViewModel;
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
        private OrderViewModel viewModel = App.orderViewModel;

        public SelectPlace()
        {
            InitializeComponent();
        }

        private void SeatButton_Click(object sender, RoutedEventArgs e)
        {
            // 매장 식사
            foreach(Food food in this.viewModel.selectMenuList)
            {
                food.orderTypeName = "매장";
            }

            Seat seat = new Seat();
            NavigationService.Navigate(seat);
        }

        private void TakeOutButton_Click(object sender, RoutedEventArgs e)
        {
            // 포장 주문
            foreach (Food food in this.viewModel.selectMenuList)
            {
                food.orderTypeName = "포장";
            }

            foreach (OrderModel order in this.viewModel.orderMenuList)
            {
                order.seatId = 0;
            }
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
