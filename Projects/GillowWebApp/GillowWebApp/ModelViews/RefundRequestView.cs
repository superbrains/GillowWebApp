using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GillowWebApp.ModelViews
{
    public class RefundRequestView
    {
        public int? ID { get; set; }

        public int OwnerID { get; set; }
        public int CustomerID { get; set; }

        public int VideoSchedulesID { get; set; }

        public DateTime DateRequested { get; set; }

        public string Reason { get; set; }

        public string Status { get; set; }
    }
}
