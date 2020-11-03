using Baskin_Kiosk.Common;
using Baskin_Kiosk.Util;
using MySql.Data.MySqlClient;
using System;
using System.Collections.ObjectModel;

namespace Baskin_Kiosk.Model.DAO
{
    public class FoodDAO
    {
        public ObservableCollection<Food> getFoodList()
        {
            DBConnection connection = new DBConnection();

            ObservableCollection<Food> foodList = new ObservableCollection<Food>();
            connection.getConnection();

            String sql = "Select * from kiosk.menu";
            connection.setCommand(sql);
            MySqlDataReader reader = connection.executeReader();

            int i = 0;
            int categoryIndex = 1;

            while (reader.Read())
            {
                Food food = new Food();
                food.menuId = int.Parse(reader["id"].ToString());
                food.menuName = reader["menu_name"].ToString();
                food.price = int.Parse(reader["menu_price"].ToString());
                food.imageSrc = reader["menu_image"].ToString();
                food.categoryId = int.Parse(reader["category_id"].ToString());
                food.salePrice = int.Parse(reader["menu_sale"].ToString());

                if (categoryIndex != (int) food.categoryId)
                {
                    categoryIndex = (int)food.categoryId;
                    i = 0;
                }

                food.page = i++ / 9 + 1;
                
                foodList.Add(food);
            }

            connection.closeConnection();
            return foodList;
        }
    }
}
