using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GillowWebApp.Models
{
    public class ServiceRequests
    {

        public ServiceRequests()
        {
            this.Buyer = new Profile();
            this.Seller = new Profile();
            this.Services = new Services();
        }

        [Key]

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int? RequestID { get; set; }
        public virtual Profile Buyer { get; set; }
        public virtual Profile Seller { get; set; }
        public virtual Services Services { get; set; }

        public DateTime DateRequested { get; set; }

        public String Note { get; set; }

        public double Lat { get; set; }

        public double Lon { get; set; }

        public String Status { get; set; }

        public double MaterialCost { get; set; }

        public double LabourCost { get; set; }

        public double Balance { get; set; }



    }
}
