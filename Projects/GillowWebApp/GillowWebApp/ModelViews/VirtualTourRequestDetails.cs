using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GillowWebApp.ModelViews
{
    public class VirtualTourRequestDetails
    {
        public int ID { get; set; }
        public int ProfileID { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public int PropertyID  { get; set; }
        public string Title { get; set; }
        public string Location { get; set; }
        public DateTime RequestDate { get; set; }

        public string Status { get; set; }
    }
}
