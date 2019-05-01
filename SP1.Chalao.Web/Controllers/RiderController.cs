using System;
using System.Web.Mvc;
using ATP2.SMS.Web.Framework.Bases;
using SP1.Chalao.Entities;
using SP1.Chalao.Framework.Constants;
using SP1.Chalao.Web.Framework.Attributes;
using SP1.Chalao.Web.Framework.Utils;

namespace SP1.Chalao.Web.Controllers
{
    
    public class RiderController : BaseController
    {
        // GET: Rider
        [ChalaoAuthorize(EnumCollection.UserTypeEnum.Rider)]
        public ActionResult Index(int error=-1)

        {
            if (error != -1)
            {
                ViewBag.Message = "You were trying to bypass the security.";
            }

            return View();
        }
        [ChalaoAuthorize(EnumCollection.UserTypeEnum.Employee)]
        public ActionResult List(string key = "")
        {
            var result = RiderRepo.GetAll(key);
            if (TempData["Error"] != null)
            {
                ViewBag.Error = TempData["Error"];
            }
            return View(result);
        }

        public ActionResult Edit(int id)
        {
            var result = RiderRepo.GetByID(id);
            return View(result.Data ?? new Riders() { Users = new Users(), DOB = DateTime.Now });
        }

        [HttpPost]
        public ActionResult Edit(Riders riders)
        {
            if (!ModelState.IsValid)
            {
                return View(riders);
            }

            var result = RiderRepo.Save(riders);

            if (result.HasError)
            {
                ViewBag.Error = result.Message;
                return View(riders);
            }

            return RedirectToAction("List");
        }

        public ActionResult Delete(int id)
        {
            var result = RiderRepo.Delete(id);

            if (result.HasError)
            {
                TempData["Error"] = result.Message;
            }
            return RedirectToAction("List");
        }

        public ActionResult Profile(int id)
        {
            var result = RiderRepo.GetByID(id);
            return View(result.Data ?? new Riders() { Users = new Users(), DOB = DateTime.Now });
        }

        [HttpPost]
        public ActionResult Profile(Riders riders)
        {
            if (!ModelState.IsValid)
            {
                return View(riders);
            }

            var result = RiderRepo.Save(riders);

            if (result.HasError)
            {
                ViewBag.Error = result.Message;
                return View(riders);
            }

            return RedirectToAction("Index");
        }
    }

}