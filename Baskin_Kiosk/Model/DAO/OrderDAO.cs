using Baskin_Kiosk.Util;
using Baskin_Kiosk.ViewModel;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace Baskin_Kiosk.Model.DAO
{
    public class OrderDAO : IOrderDB
    {
        private readonly OrderViewModel viewModel = App.orderViewModel;
        private readonly DBConnection connection = new DBConnection();

        public void InsertOrderDB()
        {
            connection.GetConnection(Constants.DB_HOST);

            foreach (OrderModel order in viewModel.orderMenuList)
            {
                string columnSQL = "INSERT INTO kiosk.order (user_id, menu_id, category_id, seat_id, total_price, sale_price, order_type, order_time, order_num) VALUES";
                string valueSQL = "(" + order.userId + ", " + order.menuId + ", " + order.categoryId + ", " + order.seatId + ", " + (order.price + order.salePrice) + ", " +
                    order.salePrice + ", " + order.orderType + ", " + "'" + order.orderTime.ToString("yyyy-MM-dd HH:mm:ss") + "'" + ", " + order.orderNum + ")";

                connection.SetCommand(columnSQL + valueSQL);
                connection.ExecuteNonQuery();
            }

            connection.CloseConnection();
        }

        public List<OrderModel> ReadOrderDB()
        {
            string sql = "select * from kiosk.order";

            List<OrderModel> orderModel = new List<OrderModel>();

            connection.GetConnection(Constants.DB_HOST);
            
            connection.SetCommand(sql);
            MySqlDataReader reader = connection.ExecuteReader();

            while (reader.Read())
            {
                orderModel.Add(new OrderModel()
                {
                    userId = int.Parse(reader["user_id"].ToString()),
                    menuId = int.Parse(reader["menu_id"].ToString()),
                    categoryId = int.Parse(reader["category_id"].ToString()),
                    seatId = int.Parse(reader["seat_id"].ToString()),
                    price = int.Parse(reader["total_price"].ToString()),
                    salePrice = int.Parse(reader["sale_price"].ToString()),
                    orderType = int.Parse(reader["order_type"].ToString()),
                    orderTime = DateTime.ParseExact(reader["order_time"].ToString(), "yyyy-MM-dd HH:mm:ss", new CultureInfo("ko-KR")),
                    orderNum = int.Parse(reader["order_num"].ToString())
                });
            }

            return orderModel;
        }

        public string GetTotalPrice()
        {
            int totalPrice = 0;
            connection.GetConnection(Constants.DB_HOST);
            string sql = "select total_price, sale_price from kiosk.order WHERE date(order_time) <= date(now())";

            connection.SetCommand(sql);
            MySqlDataReader reader = connection.ExecuteReader();

            while (reader.Read())
            {
                totalPrice += (int.Parse(reader["total_price"].ToString()) - int.Parse(reader["sale_price"].ToString()));
            }

            return totalPrice.ToString();
        }

        public string GetSalePrice()
        {
            int salePrice = 0;
            connection.GetConnection(Constants.DB_HOST);
            string sql = "select sale_price from kiosk.order";

            connection.SetCommand(sql);
            MySqlDataReader reader = connection.ExecuteReader();

            while (reader.Read())
            {
                salePrice += int.Parse(reader["sale_price"].ToString());
            }

            return salePrice.ToString();
        }

        public string GetPurePrice()
        {
            int salePrice = 0;
            connection.GetConnection(Constants.DB_HOST);
            string sql = "select total_price from kiosk.order";

            connection.SetCommand(sql);
            MySqlDataReader reader = connection.ExecuteReader();

            while (reader.Read())
            {
                salePrice += int.Parse(reader["total_price"].ToString());
            }

            return salePrice.ToString();
        }

        public string GetTypePrice(int type) // 0 = 카드, 1 = 현금
        {
            int salePrice = 0;
            connection.GetConnection(Constants.DB_HOST);
            string sql = "select total_price from kiosk.order where order_type = " + type;

            connection.SetCommand(sql);
            MySqlDataReader reader = connection.ExecuteReader();

            while (reader.Read())
            {
                salePrice += int.Parse(reader["total_price"].ToString());
            }

            return salePrice.ToString();
        }
    }
}
