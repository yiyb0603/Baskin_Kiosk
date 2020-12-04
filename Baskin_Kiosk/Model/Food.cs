﻿using Prism.Mvvm;

namespace Baskin_Kiosk.Common
{
    public class Food : BindableBase
    {
        public int menuId { get; set; }
        public int categoryId { get; set; }
        public string categoryName { get; set; }
        public string imageSrc { get; set; }
        public string menuName { get; set; }
        public string orderTypeName { get; set; }
        // 결제 방식 화면 선택리스트에서 매장/포장인지 보여줌
        public int orderType { get; set; }
        public int salePrice { get; set; }
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
