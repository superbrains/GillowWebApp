using GillowWebApp.ModelViews;
using GillowWebApp.Repositories.AllProperties;
using GillowWebApp.Repositories.Profiles;
using GillowWebApp.Result;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GillowWebApp.Controllers
{
    public class PostPropertyController : Controller
    {
        IProfileRepositories _profile;
        IPropertiesRepositories _properties;

        private readonly IHttpContextAccessor _httpContextAccessor;
        private ISession _session => _httpContextAccessor.HttpContext.Session;
        public PostPropertyController(IProfileRepositories profiles, IPropertiesRepositories properties, IHttpContextAccessor httpContextAccessor)
        {
            _profile = profiles;
            _properties = properties;
            _httpContextAccessor = httpContextAccessor;
        }
        // GET: PostPropertyController
        public ActionResult Index()
        {
            var variables = _httpContextAccessor.HttpContext.Session.GetObjectFromJson<Variables>("Variables");
            int profid = variables.ProfileID;
            ViewBag.loginStatus = "true";
            FullProfile profile = new FullProfile();
            profile = _profile.GetProfile(profid);

            return View(profile);
        }

        // GET: PostPropertyController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: PostPropertyController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PostPropertyController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: PostPropertyController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: PostPropertyController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: PostPropertyController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: PostPropertyController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }


        [HttpPost]
        [Route("UploadProperty")]
        public JsonResult UploadProperty(IFormCollection formcollection)
        {
            
          
            PropertyView properties = new PropertyView();
            var variables = _httpContextAccessor.HttpContext.Session.GetObjectFromJson<Variables>("Variables");
            int profid = variables.ProfileID;
            properties.ProfileID = profid;
            properties.Title = formcollection["title"];
            properties.Amount = Double.Parse(formcollection["Amount"]);
            properties.Bathroom = int.Parse(formcollection["bathroom"]);
            properties.Bedroom = int.Parse(formcollection["bedroom"]);
            properties.Category = formcollection["category"];
            properties.Location = formcollection["location"];
            properties.Currency = formcollection["currency"];
            properties.Description = formcollection["Description"];
            properties.VirtualURL = formcollection["virtual"];
            properties.YoutubeLink = formcollection["youtube"];
            properties.Country = formcollection["country"];
            properties.State = formcollection["state"];
            properties.Toilet = int.Parse(formcollection["toilet"]);
            //properties.file3D = formcollection["file3D"].ToString();
            properties.Type = formcollection["types"];
            properties. Features = formcollection["features"].ToList();
            properties.files = formcollection.Files;
            //properties.VirtualURL = formcollection["VirtualURL"];
            //properties.YoutubeLink = formcollection["YoutubeLink"];




            var property = _properties.PostProperty(properties);
            
            //if (property.IsCompletedSuccessfully)
            //{
            //    PropertyResult prop = new PropertyResult();
            //    prop.message = "Success";
               return Json("Success");
            //}
            //else
            //{
            //    return Json("Failed");
            //}

        }





       
    }
}
