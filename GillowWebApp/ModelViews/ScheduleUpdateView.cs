using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GillowWebApp.ModelViews
{
    public class ScheduleUpdateView
    {
        public int ScheduleID { get; set; }
        public string Note { get; set; }

        public double Rating { get; set; }
        public DateTime RescheduleDateTime { get; set; }

    }
}
