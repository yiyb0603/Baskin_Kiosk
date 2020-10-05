using Baskin_Kiosk.Common;
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
        //public string mealName { get; set; }
        //public int mealPrice { get; set; }
        //public int mealCount { get; set; }
        private List<Food> foodList = new List<Food>()
        {
            new Food() { category = Category.ICECREAM, foodName = "아이스크림1", imageSrc = "https://i.kinja-img.com/gawker-media/image/upload/c_scale,f_auto,fl_progressive,pg_1,q_80,w_800/cbarctkebvxqyut0tbjt.jpg"},
            new Food() { category = Category.CAKE, foodName = "케이크1", imageSrc = "https://vo.la/1GbRW" },
            new Food() { category = Category.JUICE, foodName = "디저트1", imageSrc ="https://vo.la/ubbG0" },
            new Food() { category = Category.JUICE, foodName = "디저트2", imageSrc ="https://vo.la/ubbG0" },
            new Food() { category = Category.JUICE, foodName = "디저트1", imageSrc ="https://vo.la/ubbG0" },
            new Food() { category = Category.JUICE, foodName = "디저트2", imageSrc ="https://vo.la/ubbG0" },
            new Food() { category = Category.JUICE, foodName = "디저트1", imageSrc ="https://vo.la/ubbG0" },
            new Food() { category = Category.JUICE, foodName = "디저트2", imageSrc ="https://vo.la/ubbG0" },
            new Food() { category = Category.JUICE, foodName = "디저트1", imageSrc ="https://vo.la/ubbG0" },
            new Food() { category = Category.JUICE, foodName = "디저트2", imageSrc ="https://vo.la/ubbG0" },
            new Food() { category = Category.JUICE, foodName = "디저트1", imageSrc ="https://vo.la/ubbG0" },
            new Food() { category = Category.JUICE, foodName = "디저트2", imageSrc ="https://vo.la/ubbG0" },
        };

        public Order()
        {
            InitializeComponent();
            DispatcherTimer timer = new DispatcherTimer();
            categoryList.SelectedIndex = 0;

            timer.Interval = TimeSpan.FromMilliseconds(1000);
            timer.Tick += new EventHandler(timerTick);
            timer.Start();
        }

        private void categoryChange(object sender, SelectionChangedEventArgs e)
        {
            if (categoryList.SelectedIndex <= -1)
            {
                return;
            }

            Category category = (Category)categoryList.SelectedIndex;
            menuList.ItemsSource = foodList.Where(food => food.category == category).ToList();
        }

        private void timerTick(object sender, EventArgs e)
        {
            CurrentTime.Content = DateTime.Now.ToString("yyyy년 MM월 dd일 HH시 mm분 ss초");
        }
    }
}
