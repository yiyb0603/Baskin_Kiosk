using Baskin_Kiosk.Model.DAO;
using Baskin_Kiosk.View.PaymentPage;
using Baskin_Kiosk.ViewModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace Baskin_Kiosk.View.Payment
{
    /// <summary>
    /// Interaction logic for Card.xaml
    /// </summary>
    public partial class Card : Page
    {
        private OrderViewModel orderViewModel = App.orderViewModel;
        private OrderDAO orderDAO = new OrderDAO();
        private MemberDAO memberDAO = new MemberDAO();

        public Card()
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
            resultLabel.Text = e;

            PayComplete payComplete = new PayComplete(0, e);
            
            if(payComplete!=null)
            {
                NavigationService.Navigate(payComplete);
            }
            else
            {
                resultLabel.Text = "해당 회원번호가 없습니다.";
            }
        }

    }
}
