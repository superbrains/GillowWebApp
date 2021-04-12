using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GillowWebApp.Models
{
    public class ServiceImages
    {
        public ServiceImages()
        {
            this.Services = new Services();
        }

        [Key]

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int? ImageID { get; set; }
        public virtual Services Services { get; set; }

        public string ImageURL { get; set; }
    }
}
