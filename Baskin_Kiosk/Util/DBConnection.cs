﻿using MySql.Data.MySqlClient;

namespace Baskin_Kiosk.Util
{
    public class DBConnection : IDB
    {
        private MySqlConnection connection = null;
        private MySqlCommand command = null;

        public void SetCommand(string sql)
        {
            command = new MySqlCommand(sql, connection);
        }

        public void ExecuteNonQuery()
        {
            command.ExecuteNonQuery();
        }

        public MySqlDataReader ExecuteReader()
        {
            return command.ExecuteReader();
        }

        public void GetConnection(string address)
        {
            connection = new MySqlConnection(address);

            connection.Open();
        }

        public void CloseConnection()
        {
            connection.Close();
        }
    }
}
