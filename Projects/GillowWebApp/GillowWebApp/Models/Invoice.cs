using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GillowWebApp.Models
{
    public class Invoice
    {
        [Key]

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int? InvoiceNumber { get; set; }
        public virtual Profile Buyer { get; set; }
        public virtual Profile Seller { get; set; }
        public DateTime TransactionDate { get; set; }

        public DateTime DateDue { get; set; }

        public string Type { get; set; } //Service or Product

        public int TypeID { get; set; } //RequestID

        public double TotalAmount { get; set; }

        public string Status { get; set; }


    }
}
