using Baskin_Kiosk.Model.DAO;
using System;
using System.Windows.Controls;
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

        private OrderDAO orderDAO = new OrderDAO();

        public Management()
        {
            InitializeComponent();
            this.Loaded += Management_Loaded;
        }

        private void Management_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            SalePrice.Text = this.orderDAO.GetSalePrice();
            PureTotal.Text = this.orderDAO.GetPurePrice();
            TotalPrice.Text = this.orderDAO.GetTotalPrice();
            CardPrice.Text = this.orderDAO.GetTypePrice(0);
            CashPrice.Text = this.orderDAO.GetTypePrice(1);

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
