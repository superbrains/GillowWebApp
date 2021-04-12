using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GillowWebApp.ModelViews
{
    public class ReferalReport
    {
        public double AvailableAmount { get; set; }
        public List<MyReferalsVM> Referals { get; set; }
    }
}
