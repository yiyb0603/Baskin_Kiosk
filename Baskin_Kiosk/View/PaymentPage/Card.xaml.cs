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
        public Card()
        {
            InitializeComponent();

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
    }
}
