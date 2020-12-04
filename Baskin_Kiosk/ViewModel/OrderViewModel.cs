﻿using Baskin_Kiosk.Common;
using Baskin_Kiosk.Model;
using Baskin_Kiosk.Model.DAO;
using Prism.Mvvm;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Baskin_Kiosk.ViewModel
{
    public class OrderViewModel : BindableBase
    {
        public ObservableCollection<Food> foodList = new FoodDAO().GetFoodList();
        public List<Category> categoryList = new CategoryDAO().GetCategories();

        private int _pageCount = 1;
        private int _currentCategory = 0;

        public int pageCount
        {
            get => _pageCount;
            set => SetProperty(ref _pageCount, value);
        }

        public int currentCategory
        {
            get => _currentCategory;
            set => SetProperty(ref _currentCategory, value);
        }

        private int _totalAmountPrice = 0;
        public int totalAmountPrice
        {
            get => _totalAmountPrice;
            set => SetProperty(ref _totalAmountPrice, value);
        }

        private ObservableCollection<Food> _selectMenuList = new ObservableCollection<Food>();
        public ObservableCollection<Food> selectMenuList
        {
            get => _selectMenuList;
            set => SetProperty(ref _selectMenuList, value);
        }

        private ObservableCollection<OrderModel> _orderMenuList = new ObservableCollection<OrderModel>();
        public ObservableCollection<OrderModel> orderMenuList
        {
            get => _orderMenuList;
            set => SetProperty(ref _orderMenuList, value);
        }

        public void clearMenuList()
        {
            _selectMenuList.Clear();
            _orderMenuList.Clear();
            _totalAmountPrice = 0;
        }
    }
}
