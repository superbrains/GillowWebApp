using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GillowWebApp.Models
{
    public class ProductQuotation
    {
        [Key]

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public int? TransactionID { get; set; }
        public virtual Profile Buyer { get; set; }
        public virtual Profile Seller { get; set; }
        public virtual Products Products { get; set; }
        public virtual Invoice Invoice { get; set; }

        public int Quantity { get; set; }
        public double Rate { get; set; }
        public double Amount { get; set; }
        public DateTime TransactionDate { get; set; }
       
        public string Status { get; set; } //Completed, On-Hold, Rejected
    }
}
