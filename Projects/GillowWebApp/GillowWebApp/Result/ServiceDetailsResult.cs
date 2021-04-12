using GillowWebApp.ModelViews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GillowWebApp.Result
{
    public class ServiceDetailsResult
    {
        public List<ServiceDetails> serviceDetails { get; set; }

        public string message { get; set; }
        public int? id { get; set; }
    }
}
