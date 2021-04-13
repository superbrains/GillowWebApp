using GillowWebApp.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GillowWebApp2.Models
{
    public class InspectionPayment
    {

        public InspectionPayment()
        {
            this.Owner = new Profile();
            this.Customer = new Profile();
            this.VideoSchedules = new VideoSchedules();
        }

        [Key]

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int? PaymentID { get; set; }

        public virtual Profile Owner { get; set; }
        public virtual Profile Customer { get; set; }

        public virtual VideoSchedules VideoSchedules { get; set; }

        public DateTime DatePaid { get; set; }

        public double Amount { get; set; }

        public string Status { get; set; }

    }
}
