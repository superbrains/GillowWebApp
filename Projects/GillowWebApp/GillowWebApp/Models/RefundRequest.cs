using GillowWebApp.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GillowWebApp2.Models
{
    public class RefundRequest
    {

        public RefundRequest()
        {
            this.Owner = new Profile();
            this.Customer = new Profile();
            this.VideoSchedules = new VideoSchedules();
        }

        [Key]

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int? ID { get; set; }

        public virtual Profile Owner { get; set; }
        public virtual Profile Customer { get; set; }

        public virtual VideoSchedules VideoSchedules { get; set; }

        public DateTime DateRequested { get; set; }

        public string Reason { get; set; }

        public string Status { get; set; }

    }
}
