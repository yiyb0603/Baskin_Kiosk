using Baskin_Kiosk.Common;
using Baskin_Kiosk.Model;
using Prism.Mvvm;
using System.Collections.ObjectModel;
using System.Configuration;

namespace Baskin_Kiosk.ViewModel
{
    public class OrderViewModel : BindableBase
    {
        Food food = new Food();
        OrderModel orderModel = new OrderModel();
        
        public ObservableCollection<Food> foodList = new ObservableCollection<Food>()
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
            new Food() { page =  2, category = Category.JUICE, foodName = "디저트15", imageSrc ="https://vo.la/ubbG0", price = 1000},
            new Food() { page =  2, category = Category.JUICE, foodName = "디저트16", imageSrc ="https://vo.la/ubbG0", price = 21000},
        };

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
