using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GillowWebApp2.Models
{
    public class ReferalPayment
    {
        [Key]

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int? ID { get; set; }
        public int AgentID { get; set; } //The persons subscribing
        public DateTime DatePaid { get; set; }
        public double AmountPaid { get; set; }
    }
}
