﻿using SP1.Chalao.Model;
using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using SP1.Chalao.Entities;
using Newtonsoft.Json;
using SP1.Chalao.Framework.Constants;
using SP1.Chalao.Framework.Objects;
using SP1.Chalao.Web.Framework.Bases;
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

            var riders = new Riders()
            {   
                DOB = model.DOB,
                
                Users = new Users()
                {
                    Name = model.Name,
                    Email = model.Email,
                    Password = model.Password,
                    Mobile = model.Mobile,
                    User_TypeID = (int)EnumCollection.UserTypeEnum.Rider,
                }
            };


            var result = RiderRepo.Save(riders);


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
                if (HttpUtil.Current.User_TypeID == (int)EnumCollection.UserTypeEnum.Employee)
                    return RedirectToAction("Index", "Employee");
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
                switch (HttpUtil.Current.User_TypeID)
                {
                    case (int)EnumCollection.UserTypeEnum.Admin:
                        return RedirectToAction("Index", "Admin");
                    case (int)EnumCollection.UserTypeEnum.Rider:
                        return RedirectToAction("Index", "Rider");
                    case (int)EnumCollection.UserTypeEnum.Employee:
                        return RedirectToAction("Index", "Employee");
                    default:
                        return RedirectToAction("Login", "Account");
                }
            }
            catch (Exception e)
            {
                return RedirectToAction("Login","Account");
            }
            
        }
        
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login","Account");
        }

    }
}