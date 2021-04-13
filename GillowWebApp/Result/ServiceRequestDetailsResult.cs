using GillowWebApp.ModelViews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GillowWebApp.Result
{
    public class ServiceRequestDetailsResult
    {
        public List<ServiceRequestDetails> serviceRequestDetails { get; set; }

        public string message { get; set; }
        public int? id { get; set; }
    }
}
