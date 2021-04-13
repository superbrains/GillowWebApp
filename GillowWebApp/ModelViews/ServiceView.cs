using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GillowWebApp.ModelViews
{
    public class ServiceView
    {
        public int ProfileID { get; set; }
        public String Title { get; set; }
        public String Description { get; set; }
        public String Category { get; set; }
        public String SubCategory { get; set; }
        public String Location { get; set; }
        public double ActualPrice { get; set; }
        public double DicountedPrice { get; set; }
        public String Currency { get; set; }
        public String Country { get; set; }

        public String State { get; set; }
        public IFormFileCollection files { get; set; }
    }
}
