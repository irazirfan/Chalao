using System;
using System.Linq;
using System.Web.Mvc;
using SP1.Chalao.Entities;
using SP1.Chalao.Framework.Constants;
using SP1.Chalao.Web.Framework.Attributes;
using SP1.Chalao.Web.Framework.Bases;

namespace SP1.Chalao.Web.Controllers
{
    
    public class EmployeeController : BaseController
    {
        // GET: Employee
        [ChalaoAuthorize(EnumCollection.UserTypeEnum.Employee)]
        public ActionResult Index(int error = -1)

        {
            if (error != -1)
            {
                ViewBag.Message = "You were trying to bypass the security.";
            }
            return View();
        }

        [ChalaoAuthorize(EnumCollection.UserTypeEnum.Admin)]
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

        [ChalaoAuthorize(EnumCollection.UserTypeEnum.Admin)]
        public ActionResult Delete(int id)
        {
            var result = EmployeeRepo.Delete(id);

            if (result.HasError)
            {
                TempData["Error"] = result.Message;
            }
            return RedirectToAction("List");
        }

        public ActionResult Profile(int id)
        {
            var result = EmployeeRepo.GetByID(id);
            return View(result.Data ?? new Employees() { Users = new Users(), JoinDate = DateTime.Now });
        }

        [HttpPost]
        public ActionResult Profile(Employees employees)
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

            return RedirectToAction("Index");
        }

        public ActionResult GetData()
        {
            int male = Context.Riders.Where(x => x.Gender_ID == (int)EnumCollection.GenderTypeEnum.Male).Count();
            int female = Context.Riders.Where(x => x.Gender_ID == (int)EnumCollection.GenderTypeEnum.Female).Count();
            int others = Context.Riders.Where(x => x.Gender_ID == (int)EnumCollection.GenderTypeEnum.Others).Count();
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