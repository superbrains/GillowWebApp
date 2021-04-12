using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GillowWebApp.Models
{
    public class Reviews
    {
        public Reviews()
        {
            this.Reviewer = new Profile();
            this.Seller = new Profile();
        }
        [Key]

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int? ID { get; set; }
        public virtual Profile Reviewer { get; set; }
        public virtual Profile Seller { get; set; }
        public string Review { get; set; }

        public double Rating { get; set; }

        public string Type { get; set; } //Product, Service,  Property
        public int TypeID { get; set; }

        public DateTime DateReviewed { get; set; }


    }
}
