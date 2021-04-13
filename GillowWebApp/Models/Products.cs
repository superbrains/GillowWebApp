using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GillowWebApp.Models
{
    public class Products
    {
        public Products()
        {
            this.Profile = new Profile();

        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int? ProductID { get; set; }
        public virtual Profile Profile { get; set; }
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

        public DateTime ExpiryDate { get; set; }
        public String BoostPlan { get; set; }
        public int BoostPlanID { get; set; }





    }
}
