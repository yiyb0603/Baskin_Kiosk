using Baskin_Kiosk.Model.DAO;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace Baskin_Kiosk.View.ManagementPage
{
    public partial class Management : Page
    {
        public DispatcherTimer timer = new DispatcherTimer();
        private OrderDAO orderDAO = new OrderDAO();

        public Management()
        {
            InitializeComponent();
            Loaded += Management_Loaded;
            timer.Interval = TimeSpan.FromTicks(10000000);
            timer.Tick += new EventHandler(timerTick);
            timer.Start();
        }

        public void timerTick(object sender, EventArgs e)
        {
            uptime_sec.Content = App.second.ToString();
            uptime_min.Content = App.minute.ToString();
            uptime_hour.Content = App.hour.ToString();
        }

        private void Management_Loaded(object sender, RoutedEventArgs e)
        {
            SalePrice.Text = orderDAO.GetSalePrice();
            PureTotal.Text = orderDAO.GetPurePrice();
            TotalPrice.Text = orderDAO.GetTotalPrice();

            CardPrice.Text = orderDAO.GetTypePrice(0);
            CashPrice.Text = orderDAO.GetTypePrice(1);
        }

        private void menu_Click(object sender, RoutedEventArgs e)
        {
            MenuStatistic menu = new MenuStatistic();
            NavigationService.Navigate(menu);
        }

        private void category_Click(object sender, RoutedEventArgs e)
        {
            CategoryStatistic category = new CategoryStatistic();
            NavigationService.Navigate(category);
        }
    }
}
