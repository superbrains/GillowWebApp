using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GillowWebApp.ModelViews
{
    public class ServiceRequestDetails
    {
        public int RequestID { get; set; }
        public int BuyerID { get; set; }
        public String BuyerName { get; set; }
        public int SellerID { get; set; }
        public String SellerName { get; set; }

        public int ServicesID { get; set; }

        public DateTime DateRequested { get; set; }

        public string ServiceTitle { get; set; }

        public string SellerPhone { get; set; }

        public string BuyerPhone { get; set; }

        public double MaterialCost { get; set; }

        public double LabourCost { get; set; }

        public String Note { get; set; }

        public double Lat { get; set; }

        public double Lon { get; set; }

        public String Status { get; set; }

        public List<String> ImageList { get; set; }
    }
}
