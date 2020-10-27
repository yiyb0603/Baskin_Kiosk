using Baskin_Kiosk.Util;
using Baskin_Kiosk.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Baskin_Kiosk.Model.DAO
{
    public class OrderDAO
    {
        public void orderMenu()
        {
            OrderViewModel viewModel = App.orderViewModel;
            DBConnection connection = new DBConnection();
            connection.getConnection();

            foreach (OrderModel order in viewModel.selectMenuList) {
                String columnSQL = "INSERT INTO kiosk.order (user_id, menu_id, category_id, seat_id, total_price, order_type, order_time) VALUES";
                //String valueSQL = "(" + order.userId +", " +  +")";
            }
        }
    }
}
