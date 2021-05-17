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
    public class DashBoardController : Controller
    {

        IProfileRepositories _profile;
        IPropertiesRepositories _properties;

        private readonly IHttpContextAccessor _httpContextAccessor;
        private ISession _session => _httpContextAccessor.HttpContext.Session;

        public DashBoardController(IProfileRepositories profiles, IPropertiesRepositories properties, IHttpContextAccessor httpContextAccessor)
        {
            _profile = profiles;
            _properties = properties;
            _httpContextAccessor = httpContextAccessor;
        }

        // GET: DashBoardAgentController2
        public ActionResult Index()
        {

            var variables = _httpContextAccessor.HttpContext.Session.GetObjectFromJson<Variables>("Variables");
            int profid = variables.ProfileID;

            FullProfile profile = new FullProfile();
            profile = _profile.GetProfile(profid);
            ViewBag.loginStatus = "true";
            return View(profile);
        }

        // GET: DashBoardAgentController2/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: DashBoardAgentController2/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DashBoardAgentController2/Create
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

        // GET: DashBoardAgentController2/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: DashBoardAgentController2/Edit/5
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

        // GET: DashBoardAgentController2/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: DashBoardAgentController2/Delete/5
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
