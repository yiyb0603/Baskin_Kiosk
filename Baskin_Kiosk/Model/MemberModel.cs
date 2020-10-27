using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Baskin_Kiosk.Model
{
    public class MemberModel
    {
        public int id { get; set; }
        public String name { get; set; }
        public String qrcode { get; set; }
        public String barcode { get; set; }
    }
}
