using GillowWebApp.Models;
using GillowWebApp.ModelViews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GillowWebApp.Result
{
    public class RefundRequestResult
    {
        public List<RefundRequestDetails> refundRequestDetails { get; set; }
        public string message { get; set; }
        public int? id { get; set; }
    }
}
