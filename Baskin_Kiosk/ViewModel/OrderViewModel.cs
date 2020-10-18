using Baskin_Kiosk.Common;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Baskin_Kiosk.ViewModel
{
    public class OrderViewModel : BindableBase
    {
        public ObservableCollection<Food> selectMenuList;
        Food food = new Food();

        public OrderViewModel()
        {
            selectMenuList = food.foodList;
        }

        private int _pageCount = 1;
        private Category _currentCategory = 0;
        private int _totalAmountPrice = 0;

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

        public int totalAmountPrice
        {
            get => _totalAmountPrice;
            set => SetProperty(ref _totalAmountPrice, value);
        }

    }
}
