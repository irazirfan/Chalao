using SP1.Chalao.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.Services.Description;
using ATP2.SMS.Repo;
using ATP2.SMS.Web.Framework.Bases;
using Microsoft.Ajax.Utilities;
using SP1.Chalao.Entities;
using Newtonsoft.Json;
using SP1.Chalao.Framework.Constants;
using SP1.Chalao.Framework.Helper;
using SP1.Chalao.Framework.Objects;
using SP1.Chalao.Web.Framework.Attributes;
using SP1.Chalao.Web.Framework.Utils;
using SP1.Chalao.Web.ViewModels;

namespace SP1.Chalao.Web.Controllers
{
    public class AccountController : BaseController
    {
        // GET: Account
        [HttpGet]
        public ActionResult Registration()
        {
            var reg = new RegistrationModel();
            return View(reg);
        }
        [HttpPost]
        public ActionResult Registration(RegistrationModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var users = new Users()
            {
                Name = model.Name,
                Email = model.Email,
                Password = model.Password,
                Mobile = model.Mobile,
                User_TypeID = model.User_TypeID
            };

            var result = UserRepo.Save(users);

            if (result.HasError)
            {
                ViewBag.Error = result.Message;
                return View(model);
            }
            else
                return RedirectToAction("Login","Account");
        }
        public ActionResult Login()
        {
            if (User.Identity.IsAuthenticated && HttpUtil.Current != null)
            {
                if (HttpUtil.Current.User_TypeID == (int) EnumCollection.UserTypeEnum.Admin)
                    return RedirectToAction("Index", "Admin");
                if (HttpUtil.Current.User_TypeID == (int)EnumCollection.UserTypeEnum.Rider)
                    return RedirectToAction("Index", "Rider");
            }
          
            var loginModel = new LoginVM();

            if (this.ControllerContext.HttpContext.Request.Cookies.AllKeys.Contains("UserCredential"))
            {
                var cookie = this.ControllerContext.HttpContext.Request.Cookies["UserCredential"];
                if (cookie != null)
                {
                    string uc = cookie.Value;
                    string[] ep = uc.Split(',');
                    loginModel.Email = ep[0];
                    loginModel.Password = ep[1];
                    loginModel.RememberMe = true;
                }
            }
               
            return View(loginModel);
        }

        [HttpPost]
        public ActionResult Login(LoginVM model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if (model.RememberMe)
            {
                HttpCookie cookieCredential = new HttpCookie("UserCredential");
                cookieCredential.Value = model.Email + "," + model.Password;
                cookieCredential.Expires = DateTime.Now.AddDays(3);
                this.ControllerContext.HttpContext.Response.Cookies.Add(cookieCredential);
            }

            else
            {
                if (this.ControllerContext.HttpContext.Request.Cookies.AllKeys.Contains("UserCredential"))
                {
                    var cookie = this.ControllerContext.HttpContext.Request.Cookies["UserCredential"];
                    if (cookie != null)
                    {
                        cookie.Expires = DateTime.Now.AddDays(-1);
                        this.ControllerContext.HttpContext.Response.Cookies.Add(cookie);
                    }
                }
            }
            var result = UserRepo.Login(model.Email, model.Password);
            if (result.HasError)
            {
                ViewBag.Error = result.Message;
                return View(model);
            }

            var userProfile = new UserProfile()
            {
                ID = result.Data.ID,
                Email = result.Data.Email,
                Name = result.Data.Name,
                User_TypeID = result.Data.User_TypeID,
                Mobile = result.Data.Mobile
            };
            var upJson = JsonConvert.SerializeObject(userProfile);

            FormsAuthentication.SetAuthCookie(upJson,false);

            try
            {
                if (HttpUtil.Current.User_TypeID == (int)EnumCollection.UserTypeEnum.Admin)
                    return RedirectToAction("Index", "Admin");
                else if (HttpUtil.Current.User_TypeID == (int)EnumCollection.UserTypeEnum.Rider)
                    return RedirectToAction("Index", "Rider");
                else
                    return RedirectToAction("Login", "Account");
            }
            catch (Exception e)
            {
                return RedirectToAction("Login");
            }
            
        }
        
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login","Account");
        }

    }
}