using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GillowWebApp.Models
{
    public class ProductImages
    {
        public ProductImages()
        {
            this.Products = new Products();
        }
        [Key]

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int? ImageID { get; set; }
        public virtual Products Products { get; set; }

        public string ImageURL { get; set; }
    }
}
