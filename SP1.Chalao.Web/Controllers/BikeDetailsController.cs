using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SP1.Chalao.Entities;
using SP1.Chalao.Repo;
using SP1.Chalao.Web.Framework.Bases;

namespace SP1.Chalao.Web.Controllers
{
    public class BikeDetailsController : BaseController
    {
        // GET: BikeDetails
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult List(string key = "")
        {
            var result = BikeRepo.GetAll(key);
            if (TempData["Error"] != null)
            {
                ViewBag.Error = TempData["Error"];
            }
            return View(result);
        }

        public ActionResult Edit(int id)
        {
            var result = BikeRepo.GetByID(id);
            return View(result.Data ?? new Bike_Details());
        }

        [HttpPost]
        public ActionResult Edit(Bike_Details bikesDetails)
        {
            if (!ModelState.IsValid)
            {
                return View(bikesDetails);
            }

            var result = BikeRepo.Save(bikesDetails);

            if (result.HasError)
            {
                ViewBag.Error = result.Message;
                return View(bikesDetails);
            }

            return RedirectToAction("List");
        }

        public ActionResult Delete(int id)
        {
            var result = BikeRepo.Delete(id);

            if (result.HasError)
            {
                TempData["Error"] = result.Message;
            }
            return RedirectToAction("List");
        }
    }
}