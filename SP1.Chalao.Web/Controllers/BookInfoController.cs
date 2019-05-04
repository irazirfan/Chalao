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
    }
}