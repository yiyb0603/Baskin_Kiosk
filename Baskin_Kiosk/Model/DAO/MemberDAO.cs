using Baskin_Kiosk.Util;
using MySql.Data.MySqlClient;

namespace Baskin_Kiosk.Model.DAO
{
    public class MemberDAO
    {
        public MemberModel getMember(int type, string code)
        {
            MemberModel member = new MemberModel();
            DBConnection connection = new DBConnection();
            connection.getConnection();

            string sql = "select * from kiosk.user where " + (type == 0 ? "qrcode = \"" + code + "\"": "barcode = \"" + code + "\"");
            connection.setCommand(sql);

            MySqlDataReader reader = connection.executeReader();
            while (reader.Read())
            {
                member.id = int.Parse(reader["id"].ToString());
                member.name = reader["name"].ToString();
                member.qrcode = reader["qrcode"].ToString();
                member.barcode = reader["barcode"].ToString();
            }

            connection.closeConnection();
            return member;
        }
    }
}
