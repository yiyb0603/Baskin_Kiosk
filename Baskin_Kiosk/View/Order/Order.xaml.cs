using Baskin_Kiosk.Common;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    public partial class Order : Page
    {
        public int pageCount = 1;
        public Category currentCategory = 0;

        private List<Food> foodList = new List<Food>()
        {
            new Food() { page =  1, category = Category.ICECREAM, foodName = "아이스크림1", imageSrc = "https://i.kinja-img.com/gawker-media/image/upload/c_scale,f_auto,fl_progressive,pg_1,q_80,w_800/cbarctkebvxqyut0tbjt.jpg"},
            new Food() { page =  1, category = Category.CAKE, foodName = "케이크1", imageSrc = "https://vo.la/1GbRW" },
            new Food() { page =  1, category = Category.JUICE, foodName = "디저트1", imageSrc ="https://vo.la/ubbG0" },
            new Food() { page =  1, category = Category.JUICE, foodName = "디저트2", imageSrc ="https://vo.la/ubbG0" },
            new Food() { page =  1, category = Category.JUICE, foodName = "디저트1", imageSrc ="https://vo.la/ubbG0" },
            new Food() { page =  1, category = Category.JUICE, foodName = "디저트2", imageSrc ="https://vo.la/ubbG0" },
            new Food() { page =  1, category = Category.JUICE, foodName = "디저트1", imageSrc ="https://vo.la/ubbG0" },
            new Food() { page =  1, category = Category.JUICE, foodName = "디저트2", imageSrc ="https://vo.la/ubbG0" },
            new Food() { page =  1, category = Category.JUICE, foodName = "디저트1", imageSrc ="https://vo.la/ubbG0" },
            new Food() { page =  1, category = Category.JUICE, foodName = "디저트2", imageSrc ="https://vo.la/ubbG0" },
            new Food() { page =  1,  category = Category.JUICE, foodName = "디저트1", imageSrc ="https://vo.la/ubbG0" },
            new Food() { page =  1, category = Category.JUICE, foodName = "디저트2", imageSrc ="https://vo.la/ubbG0" },
            new Food() { page =  2, category = Category.JUICE, foodName = "디저트1", imageSrc ="https://vo.la/ubbG0" },
            new Food() { page =  2, category = Category.JUICE, foodName = "디저트2", imageSrc ="https://vo.la/ubbG0" },
            new Food() { page =  2,  category = Category.JUICE, foodName = "디저트1", imageSrc ="https://vo.la/ubbG0" },
            new Food() { page =  2, category = Category.JUICE, foodName = "디저트2", imageSrc ="https://vo.la/ubbG0" },
        };

        public Order()
        {
            InitializeComponent();
            categoryList.SelectedIndex = 0;
            this.prevButton.Visibility = Visibility.Hidden;
        }

        public List<Food> getFoodList(int pageCount)
        {
            return foodList.Where(food => food.page == pageCount && food.category == this.currentCategory).ToList();
        }

        private void clickPrev(object sender, EventArgs e)
        {
            --this.pageCount;
            menuList.ItemsSource = getFoodList(this.pageCount);
            this.nextButton.Visibility = Visibility.Visible;

            if (getFoodList(this.pageCount - 1).Count == 0)
            {
                this.prevButton.Visibility = Visibility.Hidden;
                return;
            }
           
        }

        private void clickNext(object sender, EventArgs e)
        {
            ++this.pageCount;
            menuList.ItemsSource = getFoodList(this.pageCount);
            this.prevButton.Visibility = Visibility.Visible;

            if (getFoodList(this.pageCount + 1).Count == 0)
            {
                this.nextButton.Visibility = Visibility.Hidden;
                return;
            }
        }

        private void categoryChange(object sender, SelectionChangedEventArgs e)
        {
            this.pageCount = 1;
            this.nextButton.Visibility = Visibility.Visible;
            this.prevButton.Visibility = Visibility.Hidden;

            if (categoryList.SelectedIndex <= -1)
            {
                return;
            }

            Category category = (Category)categoryList.SelectedIndex;
            currentCategory = category;
            menuList.ItemsSource = getFoodList(this.pageCount);
        }
    }
}
