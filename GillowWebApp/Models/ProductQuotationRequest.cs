using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GillowWebApp.Models
{
    public class ProductQuotationRequest
    {
        [Key]

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public int? RequestID { get; set; }
        public virtual Profile Buyer { get; set; }
        public virtual Profile Seller { get; set; }
        public virtual Products Products { get; set; }

        public DateTime DateRequested { get; set; }

        public int Quantity { get; set; }

        public String Note { get; set; }

        public String Status { get; set; }
    }
}
