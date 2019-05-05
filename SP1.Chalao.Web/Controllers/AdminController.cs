using System;
using System.Collections.Generic;
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

    }
}