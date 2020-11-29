using Baskin_Kiosk.Common;
using Baskin_Kiosk.Util;
using MySql.Data.MySqlClient;
using System.Collections.Generic;

namespace Baskin_Kiosk.Model.DAO
{
    public class CategoryDAO
    {
        readonly DBConnection connection = new DBConnection();

        public List<Category> GetCategories()
        {
            List<Category> categoryList = new List<Category>();
            connection.GetConnection();

            string sql = "Select * from kiosk.category";
            connection.SetCommand(sql);
            MySqlDataReader reader = connection.ExecuteReader();

            while (reader.Read())
            {
                Category category = new Category
                {
                    categoryId = int.Parse(reader["id"].ToString()),
                    categoryName = reader["name"].ToString()
                };

                categoryList.Add(category);
            }

            connection.CloseConnection();
            return categoryList;
        }

    }
}
