using Baskin_Kiosk.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Baskin_Kiosk.Interface
{
    interface IMemberDB
    {
        MemberModel GetMember(int type, string code);
    }
}
