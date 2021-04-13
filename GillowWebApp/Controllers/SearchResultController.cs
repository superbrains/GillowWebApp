using GillowWebApp.Models;
using GillowWebApp.Repositories.AllProperties;
using GillowWebApp.Repositories.Profiles;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GillowWebApp.Controllers
{
    public class SearchResultController : Controller
    {
        IProfileRepositories _profile;
        IPropertiesRepositories _properties;
        public SearchResultController(IProfileRepositories profiles, IPropertiesRepositories properties)
        {
            _profile = profiles;
            _properties = properties;
        }
        public IActionResult Index(SearchModel query)
        {
            var properties = _properties.Filter(query);

            ViewBag.Query = query;

            ViewBag.PageCount = (query.Skip / 10) + 1;
           
            ViewBag.Properties = properties.searches;
            return View();
        }
    }
}
