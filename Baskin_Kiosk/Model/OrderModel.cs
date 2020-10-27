using Baskin_Kiosk.Common;
using Prism.Mvvm;
using System;
using System.Collections.ObjectModel;
using System.Dynamic;
using System.Windows.Data;

namespace Baskin_Kiosk.Model
{
    public class OrderModel : BindableBase
    {
        public int seatId { get; set; }
        public int orderType { get; set; }
        public String orderTypeName { get; set; }
        public int categoryId { get; set; }
        public String categoryName { get; set; }
        public String userId { get; set; }
        public DateTime orderTime { get; set; }
        public int menuId { get; set; }
        public String menuName { get; set; }
        public String imageSrc { get; set; }
        public int totalCount { get; set; }
        public int totalPrice { get; set; }

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
