using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GillowWebApp.Models
{
    public class PropertyImages
    {
        public PropertyImages()
        {
            this.Properties = new Properties();
        }
        [Key]

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int? ImageID { get; set; }
        public virtual Properties Properties { get; set; }

        public string ImageURL { get; set; }
    }
}
