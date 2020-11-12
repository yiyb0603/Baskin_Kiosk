using Baskin_Kiosk.Common;
using Baskin_Kiosk.Util;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace Baskin_Kiosk.Model.DAO
{
    public class CategoryDAO
    {
        DBConnection connection = new DBConnection();

        public List<Category> getCategories()
        {
            List<Category> categoryList = new List<Category>();
            connection.getConnection();

            String sql = "Select * from kiosk.category";
            connection.setCommand(sql);
            MySqlDataReader reader = connection.executeReader();

            while (reader.Read())
            {
                Category category = new Category();
                category.categoryId = int.Parse(reader["id"].ToString());
                category.categoryName = reader["name"].ToString();

                categoryList.Add(category);
            }

            connection.closeConnection();
            return categoryList;
        }

    }
}
