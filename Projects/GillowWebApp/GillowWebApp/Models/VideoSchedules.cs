using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GillowWebApp.Models
{
    public class VideoSchedules
    {
        public VideoSchedules()
        {
            this.Properties = new Properties();
            this.Owner = new Profile();
            this.Customer = new Profile();
        }
        [Key]

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int? ID { get; set; }
        public virtual Properties Properties { get; set; }

        public virtual Profile Owner { get; set; }

        public virtual Profile Customer { get; set; }

        public DateTime DateTime { get; set; }

        public string Note { get; set; }

        public string Status { get; set; }

        public string GillowAgentComment { get; set; }

    }
}
