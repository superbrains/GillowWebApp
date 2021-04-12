using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GillowWebApp.ModelViews
{
    public class UploadProfileView
    {
        public int ProfileID { get; set; }
        public IFormFileCollection files { get; set; }
    }
}
