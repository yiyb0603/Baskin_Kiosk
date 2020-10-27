using Baskin_Kiosk.Util;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Baskin_Kiosk.Model.DAO
{
    public class MemberDAO
    {
        public MemberModel getMember(int type, String code)
        {
            MemberModel member = new MemberModel();
            DBConnection connection = new DBConnection();
            connection.getConnection();

            String sql = "select * from kiosk.user where " + (type == 0 ? "qrcode = \"" + code + "\"": "barcode = \"" + code + "\"");
            connection.setCommand(sql);

            MySqlDataReader reader = connection.executeReader();
            while (reader.Read())
            {
                member.id = int.Parse(reader["id"].ToString());
                member.name = reader["name"].ToString();
                member.qrcode = reader["qrcode"].ToString();
                member.barcode = reader["barcode"].ToString();
            }

            return member;
        }
    }
}
