using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Baskin_Kiosk.Util
{
    interface IDB
    {
        void SetCommand(string sql);
        void ExecuteNonQuery();
        MySqlDataReader ExecuteReader();
        void GetConnection();
        void CloseConnection();
    }
}
