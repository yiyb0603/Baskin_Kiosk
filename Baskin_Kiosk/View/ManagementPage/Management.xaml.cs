using Baskin_Kiosk.Common;
using Baskin_Kiosk.ViewModel;
using Baskin_Kiosk.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using MySql.Data.MySqlClient;
using System.Data;
using System.Windows.Threading;

namespace Baskin_Kiosk.View.ManagementPage
{
    /// <summary>
    /// Management.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class Management : Page
    {
        int sec = 0;
        int min = 0;
        int hour = 0;
        public DispatcherTimer timer = new DispatcherTimer();

        public Management()
        {
            InitializeComponent();

            timer.Interval = TimeSpan.FromTicks(10000000);
            timer.Tick += new EventHandler(timerTick);
            timer.Start();
        }

        public void timerTick(object sender, EventArgs e)
        {
            sec++;
            uptime_sec.Content = sec.ToString();

            if (sec == 60)
            {
                sec = 0;
                min++;
                uptime_min.Content = min.ToString();
            }
            else if (min == 60)
            {
                min = 0;
                hour++;
                uptime_hour.Content = hour.ToString();
            }
        }
    }
}
