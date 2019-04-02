using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using ATP2.SMS.Web.Framework.Bases;
using SP1.Chalao.Framework.Constants;
using SP1.Chalao.Web.Framework.Attributes;
using SP1.Chalao.Web.Framework.Utils;

namespace SP1.Chalao.Web.Controllers
{
    [ChalaoAuthorize(EnumCollection.UserTypeEnum.Rider)]
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


    }
}