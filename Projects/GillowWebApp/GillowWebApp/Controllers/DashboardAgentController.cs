using GillowWebApp.ModelViews;
using GillowWebApp.Repositories.AllProperties;
using GillowWebApp.Repositories.Profiles;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GillowWebApp.Controllers
{

    public class DashboardAgentController : Controller
    {
        IProfileRepositories _profile;
        IPropertiesRepositories _properties;

        private readonly IHttpContextAccessor _httpContextAccessor;
        private ISession _session => _httpContextAccessor.HttpContext.Session;

        public DashboardAgentController(IProfileRepositories profiles, IPropertiesRepositories properties, IHttpContextAccessor httpContextAccessor)
        {
            _profile = profiles;
            _properties = properties;
            _httpContextAccessor = httpContextAccessor;
        }

        public IActionResult Index()
        {
           var variables= _httpContextAccessor.HttpContext.Session.GetObjectFromJson<Variables>("Variables");
            int profid = variables.ProfileID;

            FullProfile profile = new FullProfile();
            profile = _profile.GetProfile(profid);

            return View(profile);
        }
    }
}
