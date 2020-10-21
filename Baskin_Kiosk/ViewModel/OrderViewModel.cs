using Baskin_Kiosk.Common;
using Baskin_Kiosk.Model;
using Baskin_Kiosk.Model.DAO;
using Prism.Mvvm;
using System.Collections.ObjectModel;
using System.Configuration;

namespace Baskin_Kiosk.ViewModel
{
    public class OrderViewModel : BindableBase
    {
        Food food = new Food();
        OrderModel orderModel = new OrderModel();

        public ObservableCollection<Food> foodList = new FoodDAO().getFoodList();

        private int _pageCount = 1;
        private Category _currentCategory = 0;

        public int pageCount
        {
            get => _pageCount;
            set => SetProperty(ref _pageCount, value);
        }

        public Category currentCategory
        {
            get => _currentCategory;
            set => SetProperty(ref _currentCategory, value);
        }

        private int _totalAmountPrice = 0;
        public int totalAmountPrice {
            get => _totalAmountPrice;
            set => SetProperty(ref _totalAmountPrice, value);
        }

        private ObservableCollection<Food> _selectMenuList = new ObservableCollection<Food>();
        public ObservableCollection<Food> selectMenuList
        {
            get => _selectMenuList;
            set => SetProperty(ref _selectMenuList, value);
        }
    }
}
