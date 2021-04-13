using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GillowWebApp.ModelViews
{
    public class CallLogView
    {
        public int BuyerID { get; set; }
        public int SellerID { get; set; }

        public DateTime DateContacted { get; set; }

        public String Channel { get; set; } //Whatsapp or Phone

        public String Type { get; set; } //Product or Service or Property

        public int TypeID { get; set; } //Whatsapp or Phone
    }
}
