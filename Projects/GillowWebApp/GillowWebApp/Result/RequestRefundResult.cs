
using GillowWebApp2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GillowWebApp.Result
{
    public class RequestRefundResult
    {
        public List<RefundRequest> refundRequests { get; set; }
        public string message { get; set; }
        public int? id { get; set; }
    }

}
