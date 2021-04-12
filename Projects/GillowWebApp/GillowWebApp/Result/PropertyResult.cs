using GillowWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GillowWebApp.Result
{
    public class PropertyResult
    {
        public List<Properties> properties { get; set; }
        public List<PropertyFeatures> propertyFeatures { get; set; }
        public string message { get; set; }
        public int? id { get; set; }
    }
}
