using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Baskin_Kiosk.Common
{
    public class Food
    {
        public Category category { get; set; }
        public String imageSrc { get; set; }
        public String foodName { get; set; }
        public int page { get; set; }
    }
}
