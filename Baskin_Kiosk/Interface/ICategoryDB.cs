using Baskin_Kiosk.Common;
using System.Collections.Generic;

namespace Baskin_Kiosk.Interface
{
    interface ICategoryDB
    {
        List<Category> GetCategories();
    }
}
