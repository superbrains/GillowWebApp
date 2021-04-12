using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GillowWebApp.ModelViews
{
    public class InvoiceView
    {
        public String Buyer { get; set; }
        public String Seller { get; set; }
        public DateTime TransactionDate { get; set; }

        public DateTime DateDue { get; set; }

        public string Type { get; set; } //Service or Product

        public double TotalAmount { get; set; }

        public string Status { get; set; }
    }
}
