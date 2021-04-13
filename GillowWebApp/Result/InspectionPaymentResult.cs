
using GillowWebApp2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GillowWebApp.Result
{
    public class InspectionPaymentResult
    {
        public List<InspectionPayment> inspectionPayments { get; set; }

        public string message { get; set; }
        public int? id { get; set; }
    }
}
