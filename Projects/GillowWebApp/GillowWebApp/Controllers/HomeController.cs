using GillowWebApp.Models;
using GillowWebApp.Repositories.AllProperties;
using GillowWebApp.Repositories.Profiles;
using GillowWebApp.Result;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace GillowWebApp.Controllers
{
    public class HomeController : Controller
    {
        IProfileRepositories _profile;
        IPropertiesRepositories _properties;

        private readonly IHttpContextAccessor _httpContextAccessor;
        private ISession _session => _httpContextAccessor.HttpContext.Session;
       
        public HomeController(IProfileRepositories profiles, IPropertiesRepositories properties, IHttpContextAccessor httpContextAccessor)
        {
            _profile = profiles;
            _properties = properties;
            _httpContextAccessor = httpContextAccessor;
        }

        public IActionResult Index()
        {
            SearchResult searchResult = _properties.Top10Properties();

            ViewBag.Properties = searchResult.searches;
            

            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public async Task<JsonResult> Location(string term)
        {
           Locations locations = new Locations();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://maps.googleapis.com/maps/api/place/autocomplete/json?input="+ term +"&types=geocode&key=AIzaSyAuciemUDUfoAKAQvki2mFn4COjFPlNRLI"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    locations = JsonConvert.DeserializeObject<Locations>(apiResponse);
                }
            }

            string[] loc;

            List<string> list = new List<string>();
            foreach (var item in locations.Predictions)
            {
                list.Add(item.Description);
            }

            loc = list.ToArray();
            return Json(loc);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
public static class SessionExtensions
{
    public static void SetObjectAsJson(this ISession session, string key, object value)
    {
        session.SetString(key, JsonConvert.SerializeObject(value));
    }

    public static T GetObjectFromJson<T>(this ISession session, string key)
    {
        var value = session.GetString(key);

        return value == null ? default(T) : JsonConvert.DeserializeObject<T>(value);
    }
}