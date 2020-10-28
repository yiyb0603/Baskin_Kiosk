using Baskin_Kiosk.Util;
using Baskin_Kiosk.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Baskin_Kiosk.Model.DAO
{
    public class OrderDAO
    {
        private OrderViewModel viewModel = App.orderViewModel;
        public void orderMenu()
        {
            DBConnection connection = new DBConnection();
            connection.getConnection();

            foreach (OrderModel order in this.viewModel.orderMenuList)
            {
                String columnSQL = "INSERT INTO kiosk.order (user_id, menu_id, category_id, seat_id, total_price, order_type, order_time) VALUES";
                String valueSQL = "(" + order.userId + ", " + order.menuId + ", " + order.categoryId + ", " + "0, " + order.price + ", " + order.orderType + ", " + "'" + order.orderTime.ToString("yyyy-MM-dd HH:mm:ss") + "'" +")";

                connection.setCommand(columnSQL + valueSQL);
                connection.executeNonQuery();
            }

            connection.closeConnection();
        }
    }
}
