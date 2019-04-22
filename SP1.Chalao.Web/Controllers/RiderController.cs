using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using ATP2.SMS.Web.Framework.Bases;
using SP1.Chalao.Entities;
using SP1.Chalao.Framework.Constants;
using SP1.Chalao.Web.Framework.Attributes;
using SP1.Chalao.Web.Framework.Utils;

namespace SP1.Chalao.Web.Controllers
{
    [ChalaoAuthorize(EnumCollection.UserTypeEnum.Admin)]
    public class RiderController : BaseController
    {
        // GET: Rider
        public ActionResult Index(int error=-1)

        {
            if (error != -1)
            {
                ViewBag.Message = "You were trying to bypass the security.";
            }
            if (HttpUtil.Current == null)
            {
                return RedirectToAction("Login", "Account");
            }

            return View();
        }

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
    }

}