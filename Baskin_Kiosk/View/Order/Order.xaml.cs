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
using System.Windows.Threading;

namespace Baskin_Kiosk.View.Order
{
    /// <summary>
    /// Interaction logic for Order.xaml
    /// </summary>
    public partial class Order : Page
    {
        public string mealName { get; set; }
        public int mealPrice { get; set; }
        public int mealCount { get; set; }

        private static List<Order> instance;

        public static List<Order> GetInstance()
        {
            if (instance == null)
            {
                instance = new List<Order>();
            }
            return instance;
        }

        public Order()
        {
            InitializeComponent();
            DispatcherTimer timer = new DispatcherTimer();

            timer.Interval = TimeSpan.FromMilliseconds(1000);
            timer.Tick += new EventHandler(timerTick);
            timer.Start();
        }

        private void timerTick(object sender, EventArgs e)
        {
            CurrentTime.Content = DateTime.Now.ToString("yyyy년 MM월 dd일 HH시 mm분 ss초");
        }

        private void mealsClick(object sender, RoutedEventArgs e)
        {
            Order.GetInstance().Add(new Order() { mealName = Meal.Content.ToString(), mealPrice = 2000, mealCount = 1 });

            itemList.Items.Add(Order.GetInstance());
        }
    }
}
