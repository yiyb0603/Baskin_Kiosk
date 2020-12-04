using Baskin_Kiosk.Common;
using Baskin_Kiosk.Model;
using Baskin_Kiosk.Model.DAO;
using LiveCharts;
using LiveCharts.Definitions.Series;
using LiveCharts.Helpers;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace Baskin_Kiosk.View.ManagementPage
{
    public partial class CategoryStatistic : Page
    {
        private CategoryDAO categoryDAO = new CategoryDAO();
        private OrderDAO orderDAO = new OrderDAO();
        private List<string> categoryNames = new List<string>();

        public SeriesCollection categoryChart { get; set; }

        public CategoryStatistic()
        {
            InitializeComponent();
            categoryChart = new SeriesCollection { };
            DataContext = this;

            Loaded += CategoryStatistic_Loaded;
        }

        private void CategoryStatistic_Loaded(object sender, RoutedEventArgs e)
        {
            List<OrderModel> orderList = this.orderDAO.ReadOrderDB();
            List<Category> categories = this.categoryDAO.GetCategories();

            foreach (OrderModel order in orderList)
            {
                foreach (Category category in categories)
                {
                    if (order.categoryId == category.categoryId)
                    {

                        if (!this.categoryNames.Contains(category.categoryName))
                        {
                            this.categoryNames.Add(category.categoryName);
                            this.categoryChart.Add(new PieSeries
                            {
                                Title = category.categoryName,
                                Values = new ChartValues<double> { order.price },
                                DataLabels = true,
                            });
                        } else
                        {
                            int existIdx = this.categoryNames.IndexOf(category.categoryName);
                            this.categoryChart.AsChartValues()[existIdx].Values = new ChartValues<double> { order.price };
                        }
                    }
                }
            }
        }

        private void Click_Prev(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}