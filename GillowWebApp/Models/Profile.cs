using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GillowWebApp.Models
{
    public class Profile
    {
        [Key]

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int? ProfileID { get; set; }

        public int ReferalID { get; set; }

        public string FullName { get; set; }

        public string PhoneNumber { get; set; }

        public string CompanyName { get; set; }

        public string Location { get; set; }

        public double Lat { get; set; }
        public double Lon { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string SubscriptionPlan { get; set; }

        public int SubscriptionPlanID{ get; set; }

        public DateTime ExpiryDate { get; set; }

        public DateTime DateRegistered { get; set; }

        public string WhatsappPhone { get; set; }

        public string BankName { get; set; }

        public string BankCode { get; set; }

        public string AccountName { get; set; }

        public string AccountNumber { get; set; }

        public string VerificationStatus { get; set; }

        public string Status { get; set; }

        public string ImageUrl { get; set; }
    }
}
