using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GillowWebApp.ModelViews
{
    public class Searches
    {
        public string Type { get; set; } 

        public string Category { get; set; } //Rent, Buy, Lease

        public int? TypeID { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string SellerProfilePic { get; set; }

        public double Price { get; set; }
        public bool Negotiable { get; set; }
        public string Location { get; set; }
        public double Lat { get; set; }
        public double Lon { get; set; }
        public int Beds { get; set; }
        public int Toilets { get; set; }
        public int Baths { get; set; }
        public int Parking { get; set; }
        public string Phone { get; set; }
        public int? SellerID { get; set; }
        public string ImageURL { get; set; }
        public List<string> ImageList { get; set; }
        public string SellerName { get; set; }
        public string ImageURL3D { get; set; }
        public string Subscription { get; set; }
        public int SubscriptionID { get; set; }
        public string VerificationStatus { get; set; }
        public double Rating { get; set; }
        public string YoutubeLink { get; set; }

        public string Features { get; set; }

        public string VirtualURL { get; set; }
    }
}
