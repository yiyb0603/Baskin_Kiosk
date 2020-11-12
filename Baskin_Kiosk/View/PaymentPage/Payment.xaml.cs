using Baskin_Kiosk.Model;
using Baskin_Kiosk.View.Payment;
using Baskin_Kiosk.ViewModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace Baskin_Kiosk.View.PaymentPage
{
    public partial class Payment : Page
    {
        private OrderViewModel orderViewModel = App.orderViewModel;

        public Payment()
        {
            InitializeComponent();
            Loaded += Payment_Loaded;
        }

        private void Payment_Loaded(object sender, RoutedEventArgs e)
        {
            DataContext = this.orderViewModel;
            this.itemList.ItemsSource = this.orderViewModel.selectMenuList;
            this.totalPrice.Text = "총금액: " + this.orderViewModel.totalAmountPrice + "원";
        }

        private void CardButton_Click(object sender, RoutedEventArgs e)
        {
            foreach (OrderModel order in this.orderViewModel.orderMenuList)
            {
                order.orderType = 0;
            }
            Card card = new Card();
            NavigationService.Navigate(card);
        }

        private void CashButton_Click(object sender, RoutedEventArgs e)
        {
            foreach (OrderModel order in this.orderViewModel.orderMenuList)
            {
                order.orderType = 1;
            }

            Cash cash = new Cash();
            NavigationService.Navigate(cash);
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
