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

        public Order()
        {
            InitializeComponent();
            Loaded += Order_Loaded;
        }

        private void Order_Loaded(object sender, RoutedEventArgs e)
        {
            DataContext = App.orderViewModel;

            categoryMenus.SelectedIndex = viewModel.currentCategory;
            prevButton.Visibility = Visibility.Hidden;

            tbl_totalPrice.Text = viewModel.totalAmountPrice.ToString();
            categoryMenus.ItemsSource = viewModel.categoryList;
            viewModel.selectMenuList.CollectionChanged += CollectionChanged;
        }

        private void CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            int totalCount = 0;
            int totalPrice = 0;

            foreach (Food food in viewModel.selectMenuList)
            {
                totalCount += food.count;
                totalPrice += food.price;
            }

            tbl_totalPrice.Text = totalPrice.ToString();
        }

        private List<Food> GetFoodList(int pageCount)
        {
            return viewModel.foodList.Where((food) => food.page == pageCount && food.categoryId == viewModel.currentCategory).ToList();
        }

        private Food GetFoodObject(ObservableCollection<Food> list, Food selectedFood)
        {
            Food findObject = list.Where((food) => food.menuId == selectedFood.menuId).FirstOrDefault();
            return findObject;
        }

        private void ClickPrev(object sender, EventArgs e)
        {
            --viewModel.pageCount;
            menuList.ItemsSource = GetFoodList(viewModel.pageCount);
            nextButton.Visibility = Visibility.Visible;

            if (GetFoodList(viewModel.pageCount - 1).Count == 0)
            {
                prevButton.Visibility = Visibility.Hidden;
                return;
            }
        }

        private void ClickNext(object sender, EventArgs e)
        {
            ++viewModel.pageCount;
            menuList.ItemsSource = GetFoodList(viewModel.pageCount);
            prevButton.Visibility = Visibility.Visible;

            if (GetFoodList(viewModel.pageCount + 1).Count == 0)
            {
                nextButton.Visibility = Visibility.Hidden;
                return;
            }
        }

        private void CategoryChange(object sender, SelectionChangedEventArgs e)
        {
            int category = categoryMenus.SelectedIndex;
            viewModel.currentCategory = category;

            viewModel.pageCount = 1;
            prevButton.Visibility = Visibility.Hidden;
            nextButton.Visibility = Visibility.Visible;

            if (categoryMenus.SelectedIndex <= -1)
            {
                return;
            }

            menuList.ItemsSource = GetFoodList(viewModel.pageCount);
        }

        private void MenuList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Food selectedFood = (Food)menuList.SelectedItem;

            if (selectedFood != null)
            {
                viewModel.totalAmountPrice += (selectedFood.price - selectedFood.salePrice);

                Food existFood = GetFoodObject(viewModel.selectMenuList, selectedFood);
                if (existFood != null)
                {
                    existFood.count++;
                    existFood.price += selectedFood.price;
                    CollectionChanged(null, null);
                }

                else
                {
                    Category findCategory = viewModel.categoryList.Where((category) => category.categoryId == selectedFood.categoryId).FirstOrDefault();

                    viewModel.selectMenuList.Add(
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

                    selectListView.ItemsSource = viewModel.selectMenuList;
                }

                menuList.UnselectAll();
            }
        }

        private void Button_UpClick(object sender, RoutedEventArgs e)
        {
            // 증가 버튼을 누른 엘리먼트 객체
            Food senderFood = (sender as Button).DataContext as Food;

            Food selectedFood = GetFoodObject(viewModel.foodList, senderFood);

            if (selectedFood != null)
            {
                Food foodItem = GetFoodObject(viewModel.selectMenuList, selectedFood);

                if (foodItem != null)
                {
                    foodItem.count++;
                    foodItem.price += (selectedFood.price - selectedFood.salePrice);
                    viewModel.totalAmountPrice += selectedFood.price;
                }
            }

            CollectionChanged(null, null);
        }

        private void Button_DownClick(object sender, RoutedEventArgs e)
        {
            // 감소 버튼을 누른 엘리먼트 객체
            Food senderFood = (sender as Button).DataContext as Food;
            Food selectedFood = GetFoodObject(viewModel.foodList, senderFood);

            if (selectedFood != null)
            {
                Food foodItem = GetFoodObject(viewModel.selectMenuList, selectedFood);

                if (foodItem != null)
                {
                    foodItem.count--;
                    foodItem.price -= (selectedFood.price - selectedFood.salePrice);
                    viewModel.totalAmountPrice -= selectedFood.price;

                    if (foodItem.count <= 0)
                    {
                        Food deleteItem = GetFoodObject(viewModel.selectMenuList, selectedFood);
                        viewModel.selectMenuList.Remove(deleteItem);
                    }
                }
            }
            CollectionChanged(null, null);
        }

        private void selectMenuDelete(object sender, RoutedEventArgs e)
        {
            if ((sender as Button).DataContext is Food selectedFood)
            {
                Food deleteItem = GetFoodObject(viewModel.selectMenuList, selectedFood);

                viewModel.totalAmountPrice -= deleteItem.price;
                viewModel.selectMenuList.Remove(deleteItem);
            }
        }

        private void clearSelectMenu(object sender, RoutedEventArgs e)
        {
            if (viewModel.selectMenuList.Count > 0)
            {
                viewModel.clearMenuList();
            }
        }

        private void prevPage(object sender, RoutedEventArgs e)
        {
            if (viewModel.selectMenuList.Count > 0)
            {
                var confirmMessage = MessageBox.Show("주문을 취소하시겠습니까?", "잠시만요", MessageBoxButton.YesNo);

                if (confirmMessage == MessageBoxResult.Yes)
                {
                    viewModel.clearMenuList();
                    NavigationService.GoBack();
                }

                return;
            }

            NavigationService.GoBack();
        }

        private void nextPage(object sender, RoutedEventArgs e)
        {
            if (viewModel.selectMenuList.Count <= 0)
            {
                MessageBox.Show("메뉴를 선택해주세요.");
                return;
            }

            foreach (Food food in viewModel.selectMenuList)
            {
                viewModel.orderMenuList.Add(new OrderModel()
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
