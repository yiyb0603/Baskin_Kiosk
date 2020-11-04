using Baskin_Kiosk.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Baskin_Kiosk.ViewModel;
using Baskin_Kiosk.Model;
using System.Collections.Specialized;
using System.Collections.ObjectModel;

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
            
            categoryMenus.SelectedIndex = (int)viewModel.currentCategory;
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
        }

        private List<Food> getFoodList(int pageCount)
        {
            return this.viewModel.foodList.Where((food) => food.page == pageCount && food.categoryId == this.viewModel.currentCategory).ToList();
        }

        private Food getFoodObject(ObservableCollection<Food> list, Food selectedFood)
        {
            Food findObject = list.Where((food) => food.menuId == selectedFood.menuId).FirstOrDefault();
            return findObject;
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
            int category = categoryMenus.SelectedIndex;
            this.viewModel.currentCategory = category;
            
             this.viewModel.pageCount = 1;
             this.prevButton.Visibility = Visibility.Hidden;
            this.nextButton.Visibility = Visibility.Visible;

            if (categoryMenus.SelectedIndex <= -1)
            {
                return;
            }
            
            this.menuList.ItemsSource = getFoodList(this.viewModel.pageCount);
        }

        private void menuList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Food selectedFood = (Food)menuList.SelectedItem;

            if (selectedFood != null)
            {
                this.viewModel.totalAmountPrice += (selectedFood.price - selectedFood.salePrice);

                Food existFood = getFoodObject(this.viewModel.selectMenuList, selectedFood);
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
                        menuId = selectedFood.menuId,
                        menuName = selectedFood.menuName,
                        imageSrc = selectedFood.imageSrc,
                        price = selectedFood.price - selectedFood.salePrice,
                        salePrice = selectedFood.salePrice,
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

            Food selectedFood = this.getFoodObject(this.viewModel.foodList, senderFood);

            if (selectedFood != null)
            {
                Food foodItem = this.getFoodObject(this.viewModel.selectMenuList, selectedFood);

                if (foodItem != null)
                {
                    foodItem.count++;
                    foodItem.price += (selectedFood.price - selectedFood.salePrice);
                    this.viewModel.totalAmountPrice += selectedFood.price;
                }
            }

            this.collectionChanged(null, null);
        }

        private void Button_DownClick(object sender, RoutedEventArgs e)
        {
            // 감소 버튼을 누른 엘리먼트 객체
            Food senderFood = (sender as Button).DataContext as Food;
            Food selectedFood = this.getFoodObject(this.viewModel.foodList, senderFood);

            if (selectedFood != null)
            {
                Food foodItem = this.getFoodObject(this.viewModel.selectMenuList, selectedFood);

                if (foodItem != null)
                {
                    foodItem.count--;
                    foodItem.price -= (selectedFood.price - selectedFood.salePrice);
                    this.viewModel.totalAmountPrice -= selectedFood.price;

                    if (foodItem.count <= 0)
                    {
                        Food deleteItem = this.getFoodObject(this.viewModel.selectMenuList, selectedFood);
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
                Food deleteItem = this.getFoodObject(this.viewModel.selectMenuList, selectedFood);
                
                this.viewModel.totalAmountPrice -= deleteItem.price;
                this.viewModel.selectMenuList.Remove(deleteItem);
            }
        }

        private void clearSelectMenu(object sender, RoutedEventArgs e)
        {
            if (this.viewModel.selectMenuList.Count > 0)
            {
                this.viewModel.clearMenuList();
            }
        }

        private void prevPage(object sender, RoutedEventArgs e)
        {
            if (this.viewModel.selectMenuList.Count > 0)
            {
                var confirmMessage = MessageBox.Show("주문을 취소하시겠습니까?", "잠시만요", MessageBoxButton.YesNo);

                if (confirmMessage == MessageBoxResult.Yes)
                {
                    this.viewModel.clearMenuList();
                    this.NavigationService.GoBack();
                }

                return;
            }

            this.NavigationService.GoBack();
        }

        private void nextPage(object sender, RoutedEventArgs e)
        {
            if (this.viewModel.selectMenuList.Count <= 0)
            {
                MessageBox.Show("메뉴를 선택해주세요.");
                return;
            }
            
            foreach (Food food in this.viewModel.selectMenuList)
            {
                this.viewModel.orderMenuList.Add(new OrderModel()
                {
                    categoryId = food.categoryId,
                    menuId = food.menuId,
                    price = food.price,
                    salePrice = food.salePrice,
                });
            }

            SelectPlace.SelectPlace selectPlace = new SelectPlace.SelectPlace();
            NavigationService.Navigate(selectPlace);
        }
    }
}
