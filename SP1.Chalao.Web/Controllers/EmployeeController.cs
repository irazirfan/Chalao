using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ATP2.SMS.Web.Framework.Bases;
using RiderRepo;
using SP1.Chalao.Entities;
using SP1.Chalao.Framework.Constants;
using SP1.Chalao.Web.Framework.Attributes;
using SP1.Chalao.Web.Framework.Utils;

namespace SP1.Chalao.Web.Controllers
{
    [ChalaoAuthorize(EnumCollection.UserTypeEnum.Admin)]
    public class EmployeeController : BaseController
    {
        // GET: Admin
        public ActionResult Index()
        {
            if (HttpUtil.Current == null)
            {
                return RedirectToAction("Login", "Account");
            }

            return View();
        }

        public ActionResult List(string key = "")
        {
            var result = EmployeeRepo.GetAll(key);
            if (TempData["Error"] != null)
            {
                ViewBag.Error = TempData["Error"];
            }
            return View(result);
        }

        public ActionResult Edit(int id)
        {
            var result = EmployeeRepo.GetByID(id);
            return View(result.Data ?? new Employees() { Users = new Users(), JoinDate = DateTime.Now });
        }

        [HttpPost]
        public ActionResult Edit(Employees employees)
        {
            if (!ModelState.IsValid)
            {
                return View(employees);
            }

            var result = EmployeeRepo.Save(employees);

            if (result.HasError)
            {
                ViewBag.Error = result.Message;
                return View(employees);
            }

            return RedirectToAction("List");
        }

        public ActionResult Delete(int id)
        {
            var result = EmployeeRepo.Delete(id);

            if (result.HasError)
            {
                TempData["Error"] = result.Message;
            }
            return RedirectToAction("List");
        }
    }
}