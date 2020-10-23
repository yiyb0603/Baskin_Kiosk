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

namespace Baskin_Kiosk.View.PaymentPage
{
    public partial class Cash : Page
    {
        private OrderViewModel orderViewModel = App.orderViewModel;

        public Cash()
        {
            InitializeComponent();

            price.Content = "총 금액: " + orderViewModel.totalAmountPrice;
            webcam.CameraIndex = 0;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (NavigationService.CanGoBack)
            {
                NavigationService.GoBack();
            }
        }

        private void webcam_QrDecoded(object sender, string e)
        {
            resultLabel.Content = e;
        }

        private void Page_Unloaded(object sender, RoutedEventArgs e)
        {

        }
    }
}
