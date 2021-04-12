using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GillowWebApp.ModelViews
{
    public class ReviewView
    {
        public int  ReviewerID { get; set; }
        public int SellerID { get; set; }
        public string Review { get; set; }

        public double Rating { get; set; }
    }
}
