using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GillowWebApp.ModelViews
{
    public class InspectionPaymentView
    {
        public int? PaymentID { get; set; }

        public int OwnerID { get; set; }
        public int CustomerID { get; set; }

        public int VideoScheduleID { get; set; }

        public DateTime DatePaid { get; set; }

        public double Amount { get; set; }

        public string Status { get; set; }
    }
}
