using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SP1.Chalao.Framework.Constants;
using SP1.Chalao.Web.Framework.Utils;

namespace SP1.Chalao.Web.Framework.Attributes
{
    [AttributeUsage(AttributeTargets.Class|AttributeTargets.Method,AllowMultiple = true, Inherited = true)]
    public class ChalaoAuthorize : FilterAttribute, IAuthorizationFilter
    {
        private EnumCollection.UserTypeEnum CurrentType;
        public ChalaoAuthorize(EnumCollection.UserTypeEnum userType)
        {
            CurrentType = userType;
        }
        public void OnAuthorization(AuthorizationContext filterContext)
        {
            if (!HttpContext.Current.User.Identity.IsAuthenticated || HttpContext.Current == null)
            {
                filterContext.Result = new HttpUnauthorizedResult("");
                return;
            }

            if (HttpUtil.Current.User_TypeID != (int) CurrentType )
            {
                filterContext.Result = new HttpUnauthorizedResult();
                return;
            }
           
        }
    }
}