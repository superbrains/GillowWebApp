using GillowWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GillowWebApp.Result
{
    public class ServiceImageResult
    {
        public List<ServiceImages> serviceImages { get; set; }
        public string message { get; set; }
        public int? id { get; set; }
    }
}
