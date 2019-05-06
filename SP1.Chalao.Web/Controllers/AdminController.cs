using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using SP1.Chalao.Entities;
using SP1.Chalao.Framework.Constants;
using SP1.Chalao.Web.Framework.Attributes;
using SP1.Chalao.Web.Framework.Bases;
using SP1.Chalao.Web.Framework.Utils;

namespace SP1.Chalao.Web.Controllers
{
    [ChalaoAuthorize(EnumCollection.UserTypeEnum.Admin)]
    public class AdminController : BaseController
    {
        // GET: Admin
        public ActionResult Index()
        {
            if (HttpUtil.Current == null)
            {
                return RedirectToAction("Login","Account");
            }

            return View();
        }

        public ActionResult List(string key="")
        {
            var result = AdminRepo.GetAll(key);
            if (TempData["Error"] != null)
            {
                ViewBag.Error = TempData["Error"];
            }
            return View(result);
        }

        public ActionResult Edit(int id)
        {
            var result = AdminRepo.GetByID(id);
            return View(result.Data ?? new Admins(){Users = new Users(),JoinDate = DateTime.Now });
        }

        [HttpPost]
        public ActionResult Edit(Admins admins)
        {
            if (!ModelState.IsValid)
            {
                return View(admins);
            }

            var result = AdminRepo.Save(admins);

            if (result.HasError)
            {
                ViewBag.Error = result.Message;
                return View(admins);
            }

            return RedirectToAction("List");
        }

        public ActionResult Delete(int id)
        {
            var result = AdminRepo.Delete(id);

            if (result.HasError)
            {
                TempData["Error"] = result.Message;
            }
            return RedirectToAction("List");
        }

        public ActionResult Profile(int id)
        {
            var result = AdminRepo.GetByID(id);
            return View(result.Data ?? new Admins() { Users = new Users(), JoinDate = DateTime.Now });
        }

        [HttpPost]
        public ActionResult Profile(Admins admins)
        {
            if (!ModelState.IsValid)
            {
                return View(admins);
            }

            var result = AdminRepo.Save(admins);

            if (result.HasError)
            {
                ViewBag.Error = result.Message;
                return View(admins);
            }

            return RedirectToAction("Index");
        }

        public ActionResult Dashboard()
        {
            return View();
        }

        public ActionResult GetData()
        {
            int male = Context.Riders.Where(x => x.Gender_ID == (int) EnumCollection.GenderTypeEnum.Male).Count();
            int female = Context.Riders.Where(x => x.Gender_ID == (int) EnumCollection.GenderTypeEnum.Female).Count();
            int others = Context.Riders.Where(x => x.Gender_ID == (int) EnumCollection.GenderTypeEnum.Others).Count();
            Ratio obj = new Ratio();
            obj.Male = male;
            obj.Female = female;
            obj.Others = others;

            return Json(obj, JsonRequestBehavior.AllowGet);
        }
        public class Ratio
        {
            public int Male { get; set; }
            public int Female { get; set; }
            public int Others { get; set; }
        }
    }
}