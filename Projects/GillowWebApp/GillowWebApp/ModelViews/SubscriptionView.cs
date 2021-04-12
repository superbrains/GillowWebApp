using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GillowWebApp.ModelViews
{
    public class SubscriptionView
    {
        public int ProfileID { get; set; }
        public string SubscriptionPlan { get; set; }

        public string Type { get; set; }

        public int TypeID { get; set; }

        public double Amount { get; set; }

        public int NumberofMonths { get; set; }
        public int PropertyListing { get; set; }
        public int PropertyBoost { get; set; }
        public int PropertyBlast { get; set; }
        public int StarPremium { get; set; }
        public int RealtorSpecialist { get; set; }
        public int ZoneSpecialist { get; set; }
    }
}
