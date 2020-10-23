using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Baskin_Kiosk.Util
{
    public class DBConnection
    {
        private MySqlConnection connection = null;
        private MySqlCommand command = null;

        public void setCommand(String sql)
        {
            this.command = new MySqlCommand(sql, this.connection);
        }

        public int executeQuery()
        {
            return command.ExecuteNonQuery();
        }

        public MySqlDataReader executeReader()
        {
            return command.ExecuteReader();
        }

        public void getConnection()
        {
            this.connection = new MySqlConnection(Constants.DB_HOST);

            this.connection.Open();
        }

        public void closeConnection()
        {
            this.connection.Close();
        }
    }
}
