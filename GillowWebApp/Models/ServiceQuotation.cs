using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GillowWebApp.Models
{
    public class ServiceQuotation
    {
        [Key]

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int? TransactionID { get; set; }
        public virtual Profile Buyer { get; set; }
        public virtual Profile Seller { get; set; }
        public virtual Services Services { get; set; }

        public virtual Invoice Invoice { get; set; }
        public double MaterialCost{ get; set; }
        public double LabourCost { get; set; }
        public double Total { get; set; }
        public DateTime TransactionDate { get; set; }
        public string Notes { get; set; }

        public string Status { get; set; } 



    }
}
