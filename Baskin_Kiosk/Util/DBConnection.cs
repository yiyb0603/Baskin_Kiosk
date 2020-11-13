using MySql.Data.MySqlClient;

namespace Baskin_Kiosk.Util
{
    public class DBConnection
    {
        private MySqlConnection connection = null;
        private MySqlCommand command = null;

        public void setCommand(string sql)
        {
            command = new MySqlCommand(sql, connection);
        }

        public void executeNonQuery()
        {
            command.ExecuteNonQuery();
        }

        public MySqlDataReader executeReader()
        {
            return command.ExecuteReader();
        }

        public void getConnection()
        {
            connection = new MySqlConnection(Constants.DB_HOST);

            connection.Open();
        }

        public void closeConnection()
        {
            connection.Close();
        }
    }
}
