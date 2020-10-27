using Prism.Mvvm;
using System;

namespace Baskin_Kiosk.Common
{
    public class Food
    {
        public int menuId { get; set; }
        public int categoryId { get; set; }
        public String categoryName { get; set; }
        public String imageSrc { get; set; }
        public String menuName { get; set; }
        public int page { get; set; }
        public int price { get; set; }
        public int count { get; set; }
    }
}
