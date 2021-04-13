using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GillowWebApp.Models
{
    public class BusinessArea
    {

        public BusinessArea()
        {
            this.Profile = new Profile();
        }
        [Key]

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int? ID { get; set; }
        public virtual Profile Profile { get; set; }

        public string BizArea { get; set; }

    }
}
