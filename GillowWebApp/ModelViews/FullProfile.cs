using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GillowWebApp.ModelViews
{
    public class FullProfile
    {

        public string FullName { get; set; }

        public string PhoneNumber { get; set; }

        public string CompanyName { get; set; }

        public string Location { get; set; }

        public string ProfilePic { get; set; }

        public double Lat { get; set; }
        public double Lon { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

      
        public DateTime DateRegistered { get; set; }

        public string WhatsappPhone { get; set; }

        public string BankName { get; set; }

        public string AccountName { get; set; }

        public string AccountNumber { get; set; }

        public string VerificationStatus { get; set; }

        public string Status { get; set; }




        public String SubscriptionPlan { get; set; }
        public int SubscriptionPlanID { get; set; }
        public int PropertyListing { get; set; }
        public int PropertyBoost { get; set; }
        public int PropertyBlast { get; set; }
        public int StarPremium { get; set; }
        public int RealtorSpecialist { get; set; }
        public int ZoneSpecialist { get; set; }

        public int VirtualDisplay { get; set; }
        public int NumberofMonths { get; set; }

        public DateTime ExpiryDate { get; set; }

    }
}
