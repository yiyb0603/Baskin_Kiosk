using Baskin_Kiosk.Common;
using Prism.Mvvm;
using System.Collections.ObjectModel;
using System.Dynamic;
using System.Windows.Data;

namespace Baskin_Kiosk.Model
{
    public class OrderModel
    {
        public string foodName { get; set; }
        public string imageSrc { get; set; }
        public int count { get; set; }
        public int price { get; set; }
        public int totalCount { get; set; }
        public int totalPrice { get; set; }
    }
}
