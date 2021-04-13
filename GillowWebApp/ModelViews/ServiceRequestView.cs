using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GillowWebApp.ModelViews
{
    public class ServiceRequestView
    {
        public int BuyerID { get; set; }
        public int SellerID { get; set; }
        public int ServicesID { get; set; }

        public DateTime DateRequested { get; set; }

        public String Note { get; set; }

        public double Lat { get; set; }

        public double Lon { get; set; }

        public String Status { get; set; }


    }
}
