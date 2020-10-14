using Baskin_Kiosk.Common;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        private int pageCount = 1;
        private Category currentCategory = 0;

        private ObservableCollection<Food> selectMenuList = new ObservableCollection<Food>();

        private List<Food> foodList = new List<Food>()
        {
            new Food() { page =  1, category = Category.JUICE, foodName = "디저트1", imageSrc ="https://vo.la/ubbG0" },
            new Food() { page =  1, category = Category.ICECREAM, foodName = "아이스크림1", imageSrc = "https://vo.la/1GbRW", price = 2000 },
            new Food() { page =  1, category = Category.CAKE, foodName = "케이크1", imageSrc = "https://vo.la/1GbRW", price = 3000  },
            new Food() { page =  1, category = Category.JUICE, foodName = "디저트1", imageSrc ="https://vo.la/ubbG0", price = 2000  },
            new Food() { page =  1, category = Category.JUICE, foodName = "디저트2", imageSrc ="https://vo.la/ubbG0", price = 4000  },
            new Food() { page =  1, category = Category.JUICE, foodName = "디저트1", imageSrc ="https://vo.la/ubbG0", price = 3000  },
            new Food() { page =  1, category = Category.JUICE, foodName = "디저트2", imageSrc ="https://vo.la/ubbG0", price = 2000  },
            new Food() { page =  1, category = Category.JUICE, foodName = "디저트1", imageSrc ="https://vo.la/ubbG0", price = 12000  },
            new Food() { page =  1, category = Category.JUICE, foodName = "디저트2", imageSrc ="https://vo.la/ubbG0", price = 22000  },
            new Food() { page =  1, category = Category.JUICE, foodName = "디저트1", imageSrc ="https://vo.la/ubbG0", price = 4000  },
            new Food() { page =  1, category = Category.JUICE, foodName = "디저트2", imageSrc ="https://vo.la/ubbG0", price = 6000  },
            new Food() { page =  1,  category = Category.JUICE, foodName = "디저트1", imageSrc ="https://vo.la/ubbG0", price = 8000  },
            new Food() { page =  1, category = Category.JUICE, foodName = "디저트2", imageSrc ="https://vo.la/ubbG0", price = 4000  },
            new Food() { page =  2, category = Category.JUICE, foodName = "디저트1", imageSrc ="https://vo.la/ubbG0", price = 6000  },
            new Food() { page =  2, category = Category.JUICE, foodName = "디저트2", imageSrc ="https://vo.la/ubbG0", price = 2000  },
            new Food() { page =  2,  category = Category.JUICE, foodName = "디저트1", imageSrc ="https://vo.la/ubbG0", price = 1000  },
            new Food() { page =  2, category = Category.JUICE, foodName = "디저트2", imageSrc ="https://vo.la/ubbG0", price = 21000  },
        };

        public Order()
        {
            InitializeComponent();
            this.DataContext = selectMenuList;
            categoryList.SelectedIndex = 0;
            this.prevButton.Visibility = Visibility.Hidden;
        }

        private List<Food> getFoodList(int pageCount)
        {
            return this.foodList.Where(food => food.page == pageCount && food.category == this.currentCategory).ToList();
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
            this.menuList.ItemsSource = getFoodList(this.pageCount);
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
            this.currentCategory = category;
            this.menuList.ItemsSource = getFoodList(this.pageCount);
        }

        private void menuList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Food selectedFood = (Food)menuList.SelectedItem;
            
            if (selectedFood != null)
            {
                this.selectMenuList.Add(new Food() { category = selectedFood.category, foodName = selectedFood.foodName, imageSrc = selectedFood.imageSrc, price = selectedFood.price });
                selectListView.ItemsSource = this.selectMenuList;
            }
        }

        private void menuCountUp(object sender, EventArgs e)
        {

        }

        private void menuCountDown(object sender, EventArgs e)
        {

        }
    }
}
