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

    public class LoginController : Controller
    {
        IProfileRepositories _profile;
        IPropertiesRepositories _properties;

        private readonly IHttpContextAccessor _httpContextAccessor;
        private ISession _session => _httpContextAccessor.HttpContext.Session;

        public LoginController(IProfileRepositories profiles, IPropertiesRepositories properties, IHttpContextAccessor httpContextAccessor)
        {
            _profile = profiles;
            _properties = properties;
            _httpContextAccessor = httpContextAccessor;
        }
        public IActionResult Index()
        {
            return View();
        }

        public JsonResult SignIn(LoginVM login)
        {
           var profileid = _profile.Login(login);
            if (profileid > 0)
            {
                Variables variables = new Variables();
                variables.ProfileID = profileid;
                HttpContext.Session.SetObjectAsJson("Variables", variables);
                return Json("Success");
            }
            else
            {
                return Json("Failed");
            }
           
        }
    }
}
