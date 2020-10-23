using Baskin_Kiosk.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Baskin_Kiosk.ViewModel;
using Baskin_Kiosk.Model;
using System.Collections.Specialized;

namespace Baskin_Kiosk.View.OrderPage
{
    public partial class Order : Page
    {
        private OrderViewModel viewModel = App.orderViewModel;
        OrderModel orderModel = new OrderModel();

        public Order()
        {
            InitializeComponent();
            Loaded += Order_Loaded;
        }

        private void Order_Loaded(object sender, RoutedEventArgs e)
        {
            DataContext = App.orderViewModel;

            categoryMenus.SelectedIndex = (int) viewModel.currentCategory;
            this.prevButton.Visibility = Visibility.Hidden;

            this.tbl_totalPrice.Text = this.viewModel.totalAmountPrice.ToString();
            categoryMenus.ItemsSource = this.viewModel.categoryList;
            this.viewModel.selectMenuList.CollectionChanged += this.collectionChanged;
        }

        private void collectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            int totalCount = 0;
            int totalPrice = 0;

            foreach (Food food in this.viewModel.selectMenuList)
            {
                totalCount += food.count;
                totalPrice += food.price;
            }

            tbl_totalPrice.Text = totalPrice.ToString();
            tbl_totalCount.Text = totalCount.ToString();
        }

        private List<Food> getFoodList(int pageCount)
        {
            return this.viewModel.foodList.Where((food) => food.page == pageCount && food.categoryId == this.viewModel.currentCategory).ToList();
        }

        private void clickPrev(object sender, EventArgs e)
        {
            --this.viewModel.pageCount;
            menuList.ItemsSource = getFoodList(this.viewModel.pageCount);
            this.nextButton.Visibility = Visibility.Visible;

            if (getFoodList(this.viewModel.pageCount - 1).Count == 0)
            {
                this.prevButton.Visibility = Visibility.Hidden;
                return;
            }
        }

        private void clickNext(object sender, EventArgs e)
        {
            ++this.viewModel.pageCount;
            this.menuList.ItemsSource = getFoodList(this.viewModel.pageCount);
            this.prevButton.Visibility = Visibility.Visible;

            if (getFoodList(this.viewModel.pageCount + 1).Count == 0)
            {
                this.nextButton.Visibility = Visibility.Hidden;
                return;
            }
        }

        private void categoryChange(object sender, SelectionChangedEventArgs e)
        {
            this.viewModel.pageCount = 1;
            this.nextButton.Visibility = Visibility.Visible;
            this.prevButton.Visibility = Visibility.Hidden;

            if (categoryMenus.SelectedIndex <= -1)
            {
                return;
            }

            int category = categoryMenus.SelectedIndex + 1;
            this.viewModel.currentCategory = category;
            this.menuList.ItemsSource = getFoodList(this.viewModel.pageCount);
        }

        private void menuList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Food selectedFood = (Food)menuList.SelectedItem;

            if (selectedFood != null)
            {
                this.viewModel.totalAmountPrice += selectedFood.price;

                Food existFood = this.viewModel.selectMenuList.Where((food) => food.foodName == selectedFood.foodName).FirstOrDefault();
                if (existFood != null)
                {
                    existFood.count++;
                    existFood.price += selectedFood.price;
                    this.collectionChanged(null, null);
                }

                else
                {
                    Category findCategory = this.viewModel.categoryList.Where((category) => category.categoryId == selectedFood.categoryId).FirstOrDefault();

                    this.viewModel.selectMenuList.Add(
                    new Food()
                    {
                        categoryId = selectedFood.categoryId,
                        categoryName = findCategory.categoryName,
                        foodName = selectedFood.foodName,
                        imageSrc = selectedFood.imageSrc,
                        price = selectedFood.price,
                    });

                    selectListView.ItemsSource = this.viewModel.selectMenuList;
                }

                menuList.UnselectAll();
            }
        }

        private void Button_UpClick(object sender, RoutedEventArgs e)
        {
            // 증가 버튼을 누른 엘리먼트 객체
            Food senderFood = (sender as Button).DataContext as Food;

            Food selectedFood = this.viewModel.foodList.Where((food) => food.foodName == senderFood.foodName).FirstOrDefault();

            if (selectedFood != null)
            {
                Food foodItem = this.viewModel.selectMenuList.Where((food) => food.foodName == selectedFood.foodName).FirstOrDefault();

                if (foodItem != null)
                {
                    foodItem.count++;
                    foodItem.price += selectedFood.price;
                    this.viewModel.totalAmountPrice += selectedFood.price;
                }
            }

            this.collectionChanged(null, null);
        }

        private void Button_DownClick(object sender, RoutedEventArgs e)
        {
            // 감소 버튼을 누른 엘리먼트 객체
            Food senderFood = (sender as Button).DataContext as Food;
            Food selectedFood = this.viewModel.foodList.Where((food) => food.foodName == senderFood.foodName).FirstOrDefault();

            if (selectedFood != null)
            {
                Food foodItem = this.viewModel.selectMenuList.Where((food) => food.foodName == selectedFood.foodName).FirstOrDefault();

                if (foodItem != null)
                {
                    foodItem.count--;
                    foodItem.price -= selectedFood.price;
                    this.viewModel.totalAmountPrice -= selectedFood.price;

                    if (foodItem.count <= 0)
                    {
                        Food deleteItem = this.viewModel.selectMenuList.Where((food) => food.foodName == selectedFood.foodName).FirstOrDefault();
                        this.viewModel.selectMenuList.Remove(deleteItem);
                    }
                }
            }
            this.collectionChanged(null, null);
        }

        private void selectMenuDelete(object sender, RoutedEventArgs e)
        {
            Food selectedFood = (sender as Button).DataContext as Food;

            if (selectedFood != null)
            {
                Food deleteItem = this.viewModel.selectMenuList.Where((food) => food.foodName == selectedFood.foodName).FirstOrDefault();
                
                this.viewModel.totalAmountPrice -= deleteItem.price;
                this.viewModel.selectMenuList.Remove(deleteItem);
            }
        }

        private void clearSelectMenu(object sender, RoutedEventArgs e)
        {
            if (this.viewModel.selectMenuList.Count > 0)
            {
                this.viewModel.selectMenuList.Clear();
                this.viewModel.totalAmountPrice = 0;
            }
        }

        private void prevPage(object sender, RoutedEventArgs e)
        {
            this.NavigationService.GoBack();
        }

        private void nextPage(object sender, RoutedEventArgs e)
        {
            if (this.viewModel.selectMenuList.Count <= 0)
            {
                MessageBox.Show("메뉴를 선택해주세요.");
                return;
            }

            PaymentPage.Payment payment = new PaymentPage.Payment();
            this.NavigationService.Navigate(payment);
        }
    }
}
