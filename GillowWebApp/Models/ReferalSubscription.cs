using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GillowWebApp2.Models
{
    public class ReferalSubscription
    {
        [Key]

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int? ID { get; set; }
        public int AgentID { get; set; } //The persons subscribing
        public int ReferalID { get; set; }
        public DateTime DateSubscribed { get; set; }
        public double AmountSubscribed { get; set; }
        public double Percentge { get; set; }
        public string Status { get; set; }
    }
}
