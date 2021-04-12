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
    public class PropertiesDetailsController : Controller
    {

        IProfileRepositories _profile;
        IPropertiesRepositories _properties;

        private readonly IHttpContextAccessor _httpContextAccessor;
        private ISession _session => _httpContextAccessor.HttpContext.Session;


        public PropertiesDetailsController(IProfileRepositories profiles, IPropertiesRepositories properties , IHttpContextAccessor httpContextAccessor)
        {
            _profile = profiles;
            _properties = properties;
            _httpContextAccessor = httpContextAccessor;
        }
        // GET: propertiesdetailsController
        public ActionResult Index(int ID)
        {
            SearchResult property = _properties.FilterByProperty(ID);

            ViewBag.Property = property.searches;

            Variables variables = new Variables();
            variables.Address = property.searches[0].Location;

            HttpContext.Session.SetObjectAsJson("Variables", variables);


            ViewBag.ImageList = property.searches[0].ImageList;
            ViewBag.Virtual = property.searches[0].ImageURL3D;

            ViewBag.Features = property.searches[0].Features.Substring(1, property.searches[0].Features.Length-2).Split(",");

            SearchResult feautured = _properties.Top10Properties();

            ViewBag.FeaturedProperties = feautured.searches;
            


            return View();
        }

        // GET: propertiesdetailsController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: propertiesdetailsController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: propertiesdetailsController/Create
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

        // GET: propertiesdetailsController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: propertiesdetailsController/Edit/5
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

        // GET: propertiesdetailsController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: propertiesdetailsController/Delete/5
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
    }
}
