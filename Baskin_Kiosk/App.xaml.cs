using Baskin_Kiosk.Network;
using Baskin_Kiosk.ViewModel;
using System.Windows;

namespace Baskin_Kiosk
{
    public partial class App : Application
    {
        public static OrderViewModel orderViewModel { get; } = new OrderViewModel();
        public static ServerConnection connection { get; } = new ServerConnection();
        public static MessageViewModel messageViewModel = new MessageViewModel();
    }
}
