using GillowWebApp.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GillowWebApp2.Models
{
    public class PropertySubscription
    {
        public PropertySubscription()
        {
            this.Profile = new Profile();

        }

        [Key]

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int? SubscriptionID { get; set; }
        public virtual Profile Profile { get; set; }
                
        public String SubscriptionPlan { get; set; }
        public int SubscriptionPlanID { get; set; }
        public int PropertyListing { get; set; }
        public int PropertyBoost { get; set; }
        public int PropertyBlast { get; set; }
        public int StarPremium { get; set; }

        public int VirtualDisplay { get; set; }

        public int RealtorSpecialist { get; set; }
        public int ZoneSpecialist { get; set; }
        public int NumberofMonths { get; set; }

        public DateTime ExpiryDate { get; set; }
        public string Status { get; set; }

    }
}
