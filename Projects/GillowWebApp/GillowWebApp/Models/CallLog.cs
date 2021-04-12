using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GillowWebApp.Models
{
    public class CallLog
    {
        public CallLog()
        {
            this.Buyer = new Profile();
        }
        [Key]

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int? ID { get; set; }
        public virtual Profile Buyer { get; set; }
        public virtual Profile Seller { get; set; }

        public DateTime DateContacted { get; set; }

        public String Channel { get; set; } //Whatsapp or Phone

        public String Type { get; set; } //Product or Service or Property

        public int TypeID { get; set; } //Whatsapp or Phone

    }
}
