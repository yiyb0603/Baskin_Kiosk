using System.Collections.Generic;

namespace Baskin_Kiosk.Network
{
    public class MsgPacket
    {
        public string MSGType = "0";
        public string Id = "2205";
        public string Content = string.Empty;
        public string ShopName = string.Empty;
        public string OrderNumber = string.Empty;
        public List<MsgOrderInfo> Menus = new List<MsgOrderInfo>();
    }

    public class MsgOrderInfo
    {
        public string Name = string.Empty;
        public string Count = string.Empty;
        public string Price = string.Empty;
    }
}
