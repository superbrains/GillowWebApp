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
    public class MyPropertyController : Controller
    {
        IProfileRepositories _profile;
        IPropertiesRepositories _properties;

        private readonly IHttpContextAccessor _httpContextAccessor;
        private ISession _session => _httpContextAccessor.HttpContext.Session;
        public MyPropertyController(IProfileRepositories profiles, IPropertiesRepositories properties, IHttpContextAccessor httpContextAccessor)
        {
            _profile = profiles;
            _properties = properties;
            _httpContextAccessor = httpContextAccessor;
        }
        // GET: MyPropertyController
        public ActionResult Index()
        {
            var variables = _httpContextAccessor.HttpContext.Session.GetObjectFromJson<Variables>("Variables");
            int profid = variables.ProfileID;
            FullProfile profile = new FullProfile();
            profile = _profile.GetProfile(profid);
            ViewBag.loginStatus = "true";
            var property =  _properties.GetPropertyByOwner(profid,0);
            ViewBag.myProperty = property.searches;
            return View(profile);
        }

        // GET: MyPropertyController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: MyPropertyController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MyPropertyController/Create
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

        // GET: MyPropertyController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: MyPropertyController/Edit/5
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

        // GET: MyPropertyController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: MyPropertyController/Delete/5
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
        public JsonResult UploadProperty(string package, int propId )
        {

        
            BoostView boostView = new BoostView();
            var variables = _httpContextAccessor.HttpContext.Session.GetObjectFromJson<Variables>("Variables");
            boostView.ProfileID = variables.ProfileID;
            boostView.BoostOption = "Boost";
            boostView.PropertyID = propId;

            var property = _properties.BoostProperty(boostView);

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






        
        public JsonResult BoostProperty(int TypeID, string package)
        {

            BoostView properties = new BoostView();
            var variables = _httpContextAccessor.HttpContext.Session.GetObjectFromJson<Variables>("Variables");
            int profid = variables.ProfileID;
            properties.ProfileID = profid;
            properties.BoostOption = package;
            properties.PropertyID = TypeID;

         
            var property = _properties.BoostProperty(properties);
            

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
