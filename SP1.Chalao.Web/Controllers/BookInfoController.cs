using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SP1.Chalao.Entities;
using SP1.Chalao.Framework.Constants;
using SP1.Chalao.Repo;
using SP1.Chalao.Web.Framework.Attributes;
using SP1.Chalao.Web.Framework.Bases;

namespace SP1.Chalao.Web.Controllers
{
    public class BookInfoController : BaseController
    {
        // GET: BookInfo
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult List(string key = "")
        {
            var result = BookRepo.GetAll(key);
            if (TempData["Error"] != null)
            {
                ViewBag.Error = TempData["Error"];
            }
            return View(result);
        }

        public ActionResult Edit(int id)
        {
            var result = BookRepo.GetByID(id);
            ViewBag.Bikes = Context.BikeDetails.Where(b => b.Status == 0).ToList();
            return View(result.Data ?? new Book_Info());
        }

        [HttpPost]
        public ActionResult Edit(Book_Info bookInfo)
        {
            ViewBag.Bikes = Context.BikeDetails.Where(b => b.Status == 0).ToList();
           /* if (!ModelState.IsValid)
            {
                return View(bookInfo);
            }*/

            var result = BookRepo.Save(bookInfo);

            if (!result.HasError) return Content("Bike Booked Successfully");

            ViewBag.Error = result.Message;
            return View(bookInfo);

        }

        public ActionResult Delete(int id)
        {
            var result = BookRepo.Delete(id);

            if (result.HasError)
            {
                TempData["Error"] = result.Message;
            }
            return RedirectToAction("List");
        }
    }
}