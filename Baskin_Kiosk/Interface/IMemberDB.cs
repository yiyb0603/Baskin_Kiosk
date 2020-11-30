using Baskin_Kiosk.Model;

namespace Baskin_Kiosk.Interface
{
    interface IMemberDB
    {
        MemberModel GetMember(int type, string code);
    }
}
