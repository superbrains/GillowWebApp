using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GillowWebApp.ModelViews
{
    public class propertiesdetails
    {
        public int PropertyID { get; set; }

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

        public double Amount { get; set; }

        public bool Negotiable { get; set; }

        public int Bedroom { get; set; }
        public int Toilet { get; set; }
        public int Bathroom { get; set; }
        public int Parking { get; set; }
        public int Views { get; set; }

        public string Features { get; set; }

        public List<string> ImageList { get; set; }

        public string ImageURL3D { get; set; }

        public String BoostPlan { get; set; }

        public int BoostPlanID { get; set; }

        public String VirtualURL { get; set; }


    }
}
