using System;

namespace Baskin_Kiosk.Util
{
    public class Constants
    {
        // TCP 서버 주소
        public const string SERVER_ADDRESS = "10.80.161.176";
        public const int SERVER_PORT = 80;
        public static readonly string CSV_PATH = System.IO.Directory.GetParent(Environment.CurrentDirectory).Parent.FullName + "\\Assets\\Order.csv";

        // 소희, 용빈 사용해야 하는 주소
        public const string DB_HOST = "SERVER = 10.80.162.222; DATABASE = kiosk; UID = root; PWD = root";

        // 지수가 사용해야 하는 주소
        // public const string DB_HOST = "SERVER = localhost; DATABASE = kiosk; UID = root; PWD = root";
    }
}
