using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ATP2.SMS.Web.Framework.Bases;
using SP1.Chalao.Entities;
using SP1.Chalao.Framework.Constants;
using SP1.Chalao.Web.Framework.Attributes;
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
                return RedirectToAction("Login", "Account");
            }
            return View();
        }
    }
}