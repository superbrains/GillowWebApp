using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GillowWebApp.ModelViews
{
    public class TourDetails
    {
        public int VideoScheduleID { get; set; }

        public int PropertID { get; set; }

        public string PropertyTitle { get; set; }

        public string PropertyImageURL { get; set; }

        public string Location { get; set; }

        public string Note { get; set; }

        public DateTime TourTime { get; set; }


        public int CustomerID { get; set; }

        public string CustomerName { get; set; }

        public string CustomerPhone { get; set; }


        public int SellerID { get; set; }

        public string SellerName { get; set; }

        public string SellerPhone { get; set; }

        public string Status { get; set; }


    }
}
