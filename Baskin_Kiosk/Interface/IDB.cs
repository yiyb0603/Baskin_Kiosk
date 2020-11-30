namespace Baskin_Kiosk.Util
{
    interface IDB
    {
        void GetConnection(string address);
        void CloseConnection();
    }
}