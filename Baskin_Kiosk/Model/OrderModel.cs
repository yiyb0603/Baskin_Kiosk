﻿using Baskin_Kiosk.Common;
using Prism.Mvvm;
using System;
using System.Collections.ObjectModel;
using System.Dynamic;
using System.Windows.Data;

namespace Baskin_Kiosk.Model
{
    public class OrderModel
    {
        public int seatId { get; set; }
        public int orderType { get; set; }
        public int categoryId { get; set; }
        public int userId { get; set; }
        public DateTime orderTime { get; set; }
        public int menuId { get; set; }
        public int price { get; set; }
        public int orderNum { get; set; }
    }
}
