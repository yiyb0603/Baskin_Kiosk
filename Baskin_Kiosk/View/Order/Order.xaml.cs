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
        private int totalAmountPrice { get; set; }

        private ObservableCollection<Food> selectMenuList = new ObservableCollection<Food>();

        private ObservableCollection<Food> foodList = new ObservableCollection<Food>()
        {
            new Food() { page =  1, category = Category.ICECREAM, foodName = "아이스크림1", imageSrc = "https://vo.la/1GbRW", price = 2000},
            new Food() { page =  1, category = Category.CAKE, foodName = "케이크2", imageSrc = "https://vo.la/1GbRW", price = 3000},
            new Food() { page =  1, category = Category.JUICE, foodName = "디저트3", imageSrc ="https://vo.la/ubbG0", price = 2000},
            new Food() { page =  1, category = Category.JUICE, foodName = "디저트4", imageSrc ="https://vo.la/ubbG0", price = 4000},
            new Food() { page =  1, category = Category.JUICE, foodName = "디저트5", imageSrc ="https://vo.la/ubbG0", price = 3000},
            new Food() { page =  1, category = Category.JUICE, foodName = "디저트6", imageSrc ="https://vo.la/ubbG0", price = 2000},
            new Food() { page =  1, category = Category.JUICE, foodName = "디저트7", imageSrc ="https://vo.la/ubbG0", price = 12000},
            new Food() { page =  1, category = Category.JUICE, foodName = "디저트8", imageSrc ="https://vo.la/ubbG0", price = 22000},
            new Food() { page =  1, category = Category.JUICE, foodName = "디저트9", imageSrc ="https://vo.la/ubbG0", price = 4000},
            new Food() { page =  1, category = Category.JUICE, foodName = "디저트10", imageSrc ="https://vo.la/ubbG0", price = 6000},
            new Food() { page =  1,  category = Category.JUICE, foodName = "디저트11", imageSrc ="https://vo.la/ubbG0", price = 8000},
            new Food() { page =  1, category = Category.JUICE, foodName = "디저트12", imageSrc ="https://vo.la/ubbG0", price = 4000},
            new Food() { page =  2, category = Category.JUICE, foodName = "디저트13", imageSrc ="https://vo.la/ubbG0", price = 6000},
            new Food() { page =  2, category = Category.JUICE, foodName = "디저트14", imageSrc ="https://vo.la/ubbG0", price = 2000},
            new Food() { page =  2,  category = Category.JUICE, foodName = "디저트15", imageSrc ="https://vo.la/ubbG0", price = 1000},
            new Food() { page =  2, category = Category.JUICE, foodName = "디저트16", imageSrc ="https://vo.la/ubbG0", price = 21000},
        };

        public Order()
        {
            InitializeComponent();
            Loaded += Order_Loaded;
        }

        private void Order_Loaded(object sender, RoutedEventArgs e)
        {
            DataContext = this;
            categoryList.SelectedIndex = 0;
            this.prevButton.Visibility = Visibility.Hidden;
            this.tbl_totalPrice.Text = "0";
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
                this.totalAmountPrice += selectedFood.price;
                tbl_totalPrice.Text = this.totalAmountPrice.ToString();
                this.selectMenuList.Add(new Food() { category = selectedFood.category, foodName = selectedFood.foodName, imageSrc = selectedFood.imageSrc, price = selectedFood.price });
                selectListView.ItemsSource = this.selectMenuList;
            }
        }

        private void Button_UpClick(object sender, RoutedEventArgs e)
        {
            Food selectedFood = (sender as Button).DataContext as Food;

            if (selectedFood != null)
            {
                Food foodItem = this.selectMenuList.Where((food) => food.foodName == selectedFood.foodName).FirstOrDefault();

                if (foodItem != null)
                {
                    foodItem.count++;
                    this.totalAmountPrice += selectedFood.price;
                    tbl_totalPrice.Text = this.totalAmountPrice.ToString();
                }
            }
        }

        private void Button_DownClick(object sender, RoutedEventArgs e)
        {
            Food selectedFood = (sender as Button).DataContext as Food;

            if (selectedFood != null)
            {
                Food foodItem = this.selectMenuList.Where((food) => food.foodName == selectedFood.foodName).FirstOrDefault();

                if (foodItem != null)
                {
                    foodItem.count--;
                    this.totalAmountPrice -= selectedFood.price;
                    tbl_totalPrice.Text = this.totalAmountPrice.ToString();
                    if (foodItem.count <= 0)
                    {
                        Food deleteItem = this.selectMenuList.Where((food) => food.foodName == selectedFood.foodName).FirstOrDefault();
                        this.selectMenuList.Remove(deleteItem);
                    }
                }
            }
        }

        private void selectMenuDelete(object sender, RoutedEventArgs e)
        {
            Food selectedFood = (sender as Button).DataContext as Food;

            if (selectedFood != null)
            {
                Food deleteItem = this.selectMenuList.Where((food) => food.foodName == selectedFood.foodName).FirstOrDefault();
                
                this.totalAmountPrice -= deleteItem.price * deleteItem.count;
                this.tbl_totalPrice.Text = this.totalAmountPrice.ToString();
                
                this.selectMenuList.Remove(deleteItem);
            }
        }

        private void clearSelectMenu(object sender, RoutedEventArgs e)
        {
            this.selectMenuList.Clear();
            this.tbl_totalPrice.Text = "0";
        }
    }
}
