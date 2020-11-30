using Baskin_Kiosk.Interface;
using Baskin_Kiosk.Util;
using MySql.Data.MySqlClient;

namespace Baskin_Kiosk.Model.DAO
{
    public class MemberDAO : IMemberDB
    {
        public MemberModel GetMember(int type, string code)
        {
            MemberModel member = new MemberModel();
            DBConnection connection = new DBConnection();
            connection.GetConnection(Constants.DB_HOST);

            string sql = "select * from kiosk.user where " + (type == 0 ? "qrcode = \"" + code + "\"": "barcode = \"" + code + "\"");
            connection.SetCommand(sql);

            MySqlDataReader reader = connection.ExecuteReader();
            while (reader.Read())
            {
                member.id = int.Parse(reader["id"].ToString());
                member.name = reader["name"].ToString();
                member.qrcode = reader["qrcode"].ToString();
                member.barcode = reader["barcode"].ToString();
            }

            connection.CloseConnection();
            return member;
        }
    }
}
