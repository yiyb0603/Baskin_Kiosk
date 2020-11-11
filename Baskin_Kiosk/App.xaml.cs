using Baskin_Kiosk.Network;
using Baskin_Kiosk.ViewModel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Baskin_Kiosk
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static OrderViewModel orderViewModel { get; } = new OrderViewModel();
        public static ServerConnection connection { get; } = new ServerConnection();
    }
}
