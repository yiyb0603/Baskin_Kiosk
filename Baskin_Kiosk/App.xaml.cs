using Baskin_Kiosk.Network;
using Baskin_Kiosk.ViewModel;
using System.Collections.Generic;
using System.Windows;

namespace Baskin_Kiosk
{
    public partial class App : Application
    {
        public static OrderViewModel orderViewModel { get; } = new OrderViewModel();
        public static TcpCommunication connection { get; } = new TcpCommunication();
        public static MessageViewModel messageViewModel = new MessageViewModel();

        public static List<SeatModel> lstSeat = new List<SeatModel>()
        {
            new SeatModel() { seatNumber = 1, time = 0 },
            new SeatModel() { seatNumber = 2, time = 0 },
            new SeatModel() { seatNumber = 3, time = 0 },
            new SeatModel() { seatNumber = 4, time = 0 },
            new SeatModel() { seatNumber = 5, time = 0 },
            new SeatModel() { seatNumber = 6, time = 0 },
            new SeatModel() { seatNumber = 7, time = 0 },
            new SeatModel() { seatNumber = 8, time = 0 },
            new SeatModel() { seatNumber = 9, time = 0 }
        };
        public static SeatModel selectedSeat = null;
    }
}
