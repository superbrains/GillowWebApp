using GillowWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GillowWebApp.Result
{
    public class ProductResult
    {
        public List<Products> products { get; set; }

        public string message { get; set; }
        public int? id { get; set; }
    }
}
