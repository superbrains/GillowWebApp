using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GillowWebApp.Models
{
    public class Payment
    {
        [Key]

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int? PaymentID { get; set; }
        public virtual Profile Profile { get; set; }
        public virtual Invoice Invoice { get; set; }

        public double TotalAmount { get; set; }
        public double Paid { get; set; }

        public double OnHold { get; set; }

        public DateTime Transaction { get; set; }
        public DateTime PaymentDate { get; set; }

        public string Status { get; set; }
    }
}
