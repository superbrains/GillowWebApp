using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GillowWebApp.Models
{
    public class Properties
    {
        public Properties()
        {
            this.Profile = new Profile();

        }

        [Key]

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int? PropertyID { get; set; }
        public virtual Profile Profile { get; set; }

        public DateTime DatePosted { get; set; }

        public String Title { get; set; }

        public String Description { get; set; }

        public String Category { get; set; }
        public String Type { get; set; }
        public String Location { get; set; }

        public String Country { get; set; }
        public String State { get; set; }

        public String Currency { get; set; }

        public String YoutubeLink { get; set; }

        public bool Negotiable { get; set; }
        public double Amount { get; set; }
        public int Bedroom { get; set; }
        public int Toilet { get; set; }
        public int Bathroom { get; set; }
        public int Parking { get; set; }
        public int Views { get; set; }
        public String Status { get; set; }

        public String BoostPlan { get; set; }

        public int BoostPlanID { get; set; }
        public string VirtualURL { get; set; }


    }
}
