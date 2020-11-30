using Baskin_Kiosk.Model;
using System.Collections.Generic;

namespace Baskin_Kiosk.Util
{
    interface IOrderDB
    {
        void InsertOrderDB();
        List<OrderModel> ReadOrderDB();
    }
}
