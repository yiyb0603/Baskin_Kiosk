using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Baskin_Kiosk.Common
{
    public class Food : BindableBase
    {
        public Category category { get; set; }
        public String imageSrc { get; set; }
        public String foodName { get; set; }
        public int page { get; set; }
        public int price { get; set; }


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
            new Food() { page =  2,  category = Category.JUICE, foodName = "디저트15", imageSrc ="https://vo.la/ubbG0", price = 1000},
            new Food() { page =  2, category = Category.JUICE, foodName = "디저트16", imageSrc ="https://vo.la/ubbG0", price = 21000},
        };

        private int _count = 1;
        public int count
        {
            get => _count;
            set => SetProperty(ref _count, value);
        }
    }
}
