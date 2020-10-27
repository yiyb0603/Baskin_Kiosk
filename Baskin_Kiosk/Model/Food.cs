using Prism.Mvvm;
using System;

namespace Baskin_Kiosk.Common
{
    public class Food : BindableBase
    {
        public int menuId { get; set; }
        public int categoryId { get; set; }
        public String categoryName { get; set; }
        public String imageSrc { get; set; }
        public String menuName { get; set; }
        public String orderTypeName { get; set; }
        public int orderType { get; set; }
        public int page { get; set; }
        private int _price = 0;
        public int price
        {
            get => _price;
            set => SetProperty(ref _price, value);
        }

        private int _count = 1;
        public int count
        {
            get => _count;
            set => SetProperty(ref _count, value);
        }
    }
}
