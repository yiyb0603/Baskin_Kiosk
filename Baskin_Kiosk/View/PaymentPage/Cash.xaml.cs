using Baskin_Kiosk.ViewModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace Baskin_Kiosk.View.PaymentPage
{
    public partial class Cash : Page
    {
        private OrderViewModel orderViewModel = App.orderViewModel;

        public Cash()
        {
            InitializeComponent();

            price.Content = "총 금액: " + orderViewModel.totalAmountPrice;
        }

        private void Prevbutton_Click(object sender, RoutedEventArgs e)
        {
            if (NavigationService.CanGoBack)
            {
                NavigationService.GoBack();
            }
        }

        private void Nextbutton_Click(object sender, RoutedEventArgs e)
        {
            if (resultLabel.Text != "")
            {
                NavigationService.Navigate(new PayComplete(1, resultLabel.Text));
            }
        }
    }
}
