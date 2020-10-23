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
    /// Interaction logic for Seat.xaml
    /// </summary>
    public partial class Seat : Page
    {
        Button[] seatButtonArray;

        public Seat()
        {
            InitializeComponent();

            seatButtonArray = new Button[9] { button1, button2, button3, button4, button5, button6, button7, button8, button9 };
        }
        
        private void SeatButton_Click(object sender, RoutedEventArgs e)
        {
            button1.IsEnabled = false;
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
