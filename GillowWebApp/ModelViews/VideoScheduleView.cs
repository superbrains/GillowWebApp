using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GillowWebApp.ModelViews
{
    public class VideoScheduleView
    {
        public int PropertID{ get; set; }

        public int OwnerID { get; set; }

        public int CustomerID { get; set; }

        public DateTime DateTime { get; set; }

        public string Note { get; set; }

        public string Status { get; set; }
    }
}
