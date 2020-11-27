using Baskin_Kiosk.Util;
using Baskin_Kiosk.ViewModel;
using MySql.Data.MySqlClient;

namespace Baskin_Kiosk.Model.DAO
{
    public class OrderDAO
    {
        private OrderViewModel viewModel = App.orderViewModel;
        DBConnection connection = new DBConnection();

        public void orderMenu()
        {
            connection.getConnection();

            foreach (OrderModel order in viewModel.orderMenuList)
            {
                string columnSQL = "INSERT INTO kiosk.order (user_id, menu_id, category_id, seat_id, total_price, sale_price, order_type, order_time, order_num) VALUES";
                string valueSQL = "(" + order.userId + ", " + order.menuId + ", " + order.categoryId + ", " + order.seatId + ", " + (order.price + order.salePrice) + ", " +
                    order.salePrice + ", " + order.orderType + ", " + "'" + order.orderTime.ToString("yyyy-MM-dd HH:mm:ss") + "'" + ", " + order.orderNum + ")";

                connection.setCommand(columnSQL + valueSQL);
                connection.executeNonQuery();
            }

            connection.closeConnection();
        }

        public string getTotalPrice()
        {
            int totalPrice = 0;
            connection.getConnection();
            string sql = "select total_price from kiosk.order WHERE date(order_time) <= date(now())";

            connection.setCommand(sql);
            MySqlDataReader reader = connection.executeReader();

            while (reader.Read())
            {
                totalPrice += int.Parse(reader["total_price"].ToString());
            }

            return totalPrice.ToString();
        }
    }
}
