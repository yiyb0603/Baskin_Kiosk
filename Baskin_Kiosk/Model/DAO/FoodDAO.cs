using Baskin_Kiosk.Common;
using Baskin_Kiosk.Interface;
using Baskin_Kiosk.Util;
using MySql.Data.MySqlClient;
using System.Collections.ObjectModel;

namespace Baskin_Kiosk.Model.DAO
{
    public class FoodDAO : IFoodDB
    {
        public ObservableCollection<Food> GetFoodList()
        {
            DBConnection connection = new DBConnection();

            ObservableCollection<Food> foodList = new ObservableCollection<Food>();
            connection.GetConnection();

            string sql = "Select * from kiosk.menu";
            connection.SetCommand(sql);
            MySqlDataReader reader = connection.ExecuteReader();

            int i = 0;
            int categoryIndex = 1;

            while (reader.Read())
            {
                Food food = new Food
                {
                    menuId = int.Parse(reader["id"].ToString()),
                    menuName = reader["menu_name"].ToString(),
                    price = int.Parse(reader["menu_price"].ToString()),
                    imageSrc = reader["menu_image"].ToString(),
                    categoryId = int.Parse(reader["category_id"].ToString()),
                    salePrice = int.Parse(reader["menu_sale"].ToString())
                };

                if (categoryIndex != food.categoryId)
                {
                    categoryIndex = food.categoryId;
                    i = 0;
                }

                food.page = i++ / 9 + 1;
                
                foodList.Add(food);
            }

            connection.CloseConnection();
            return foodList;
        }
    }
}
