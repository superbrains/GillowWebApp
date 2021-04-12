using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GillowWebApp.ModelViews
{
    public class RefundRequestDetails
    {
        public int? ID { get; set; }

        public int OwnerID { get; set; }
        public string OwnerName { get; set; }
        public string OwnerPhone { get; set; }

        public string OwnerBank { get; set; }
        public string OwnerAccountName { get; set; }
        public string OwnerAccountNumber{ get; set; }

        public int CustomerID { get; set; }

        public string CustomerName { get; set; }
        public string CustomerPhone { get; set; }

        public string CustomerBank { get; set; }
        public string CustomerAccountName { get; set; }
        public string CustomerAccountNumber { get; set; }

        public int VideoSchedulesID { get; set; }

        public string AgentComment { get; set; }

        public DateTime DateRequested { get; set; }

        public string Reason { get; set; }

        public string Status { get; set; }
    }
}
