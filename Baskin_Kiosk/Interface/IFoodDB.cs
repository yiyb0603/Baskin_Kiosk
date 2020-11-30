using Baskin_Kiosk.Common;
using System.Collections.ObjectModel;

namespace Baskin_Kiosk.Interface
{
    interface IFoodDB
    {
        ObservableCollection<Food> GetFoodList();
    }
}
