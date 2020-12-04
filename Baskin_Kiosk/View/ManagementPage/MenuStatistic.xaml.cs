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

    public partial class MenuStatistic : Page
    {
        public SeriesCollection SeriesCollection { get; set; }
        public string[] Labels { get; set; }
        public Func<double, string> Formatter { get; set; }

        private OrderDAO orderDAO = new OrderDAO();
        private FoodDAO foodDAO = new FoodDAO();

        List<string> foodNames = new List<string>();
        List<int> purePrices = new List<int>();
        List<int> salePrices = new List<int>();

        public MenuStatistic()
        {
            InitializeComponent();
            Loaded += Management_Loaded;
        }

        private void Management_Loaded(object sender, RoutedEventArgs e)
        {
            List<OrderModel> orderList = this.orderDAO.ReadOrderDB();
            ObservableCollection<Food> foodList = this.foodDAO.GetFoodList();

            foreach (OrderModel order in orderList)
            {
                foreach (Food food in foodList)
                {
                    if (order.menuId == food.menuId)
                    {
                        if (!this.foodNames.Contains(food.menuName))
                        {
                            this.foodNames.Add(food.menuName);
                            this.purePrices.Add(order.price);
                            this.salePrices.Add(order.salePrice);
                        } else
                        {
                            int existIndex = this.foodNames.IndexOf(food.menuName);

                            this.purePrices[existIndex] += order.price;
                            this.salePrices[existIndex] += order.salePrice;
                        }
                    }
                }
            }

            SeriesCollection = new SeriesCollection
            {
                new ColumnSeries
                {
                    Title = "할인 매출금액",
                    Values = new ChartValues<int>(this.salePrices.ToArray()),
                }
            };

            //adding series will update and animate the chart automatically
            SeriesCollection.Add(new ColumnSeries
            {
                Title = "순수 매출금액",
                Values = new ChartValues<int>(this.purePrices.ToArray()),
            });

            Labels = this.foodNames.ToArray();
            Formatter = value => value.ToString("N");

            DataContext = this;
        }

        private void Click_Prev(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
