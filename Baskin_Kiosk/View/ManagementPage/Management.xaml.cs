using Baskin_Kiosk.Common;
using Baskin_Kiosk.Model;
using Baskin_Kiosk.Model.DAO;
using LiveCharts;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace Baskin_Kiosk.View.ManagementPage
{
    public partial class Management : Page
    {
        private OrderDAO orderDAO = new OrderDAO();

        public Management()
        {
            InitializeComponent();
            Loaded += Management_Loaded;
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
