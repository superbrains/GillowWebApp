using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GillowWebApp.Models
{
    public class SearchModel
    {
        public string Location { get; set; }
        public string Category { get; set; }

        public string Type { get; set; }
        public long MinPrice { get; set; }
        public int Toilet { get; set; }
        public int Bedroom { get; set; }
        public long MaxPrice { get; set; }

        public string Keywords { get; set; }
        public int Skip { get; set; }
        
    }
}
