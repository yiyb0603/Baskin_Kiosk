using Baskin_Kiosk.Util;
using Baskin_Kiosk.ViewModel;
using System;

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
                String columnSQL = "INSERT INTO kiosk.order (user_id, menu_id, category_id, seat_id, total_price, sale_price, order_type, order_time, order_num) VALUES";
                String valueSQL = "(" + order.userId + ", " + order.menuId + ", " + order.categoryId + ", " + order.seatId + ", " + (order.price + order.salePrice) + ", " + 
                    order.salePrice + ", " + order.orderType + ", " + "'" + order.orderTime.ToString("yyyy-MM-dd HH:mm:ss") + "'" + ", " + order.orderNum + ")";

                connection.setCommand(columnSQL + valueSQL);
                connection.executeNonQuery();
            }

            connection.closeConnection();
        }
    }
}
