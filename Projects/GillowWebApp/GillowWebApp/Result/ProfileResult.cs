using GillowWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GillowWebApp.Result
{
    public class ProfileResult
    {
        public List<Profile> profiles { get; set; }

        public string message { get; set; }
        public int? id { get; set; }
    }
}
