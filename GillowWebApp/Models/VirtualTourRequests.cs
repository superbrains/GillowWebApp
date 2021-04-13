using GillowWebApp.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GillowWebApp2.Models
{
    public class VirtualTourRequests
    {
        public VirtualTourRequests()
        {
            this.Profile = new Profile();
            this.Properties = new Properties();

        }

        [Key]

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int? ID { get; set; }
        public virtual Profile Profile { get; set; }
        public virtual Properties Properties { get; set; }

        public DateTime RequestDate { get; set; }

        public string Status { get; set; }

    }
}
