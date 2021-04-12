using GillowWebApp.Models;
using GillowWebApp2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GillowWebApp.Result
{
    public class BankResult
    {
        public List<Banks> banks { get; set; }

        public string message { get; set; }
        public int? id { get; set; }
    }
}
