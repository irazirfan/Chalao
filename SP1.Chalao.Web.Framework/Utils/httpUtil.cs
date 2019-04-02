using System;
using System.Web;
using Newtonsoft.Json;
using SP1.Chalao.Framework.Objects;

namespace SP1.Chalao.Web.Framework.Utils
{
    public class HttpUtil
    {
        public static UserProfile Current
        {
            get
            {
                try
                {
                    var userProfile = JsonConvert.DeserializeObject<UserProfile>(HttpContext.Current.User.Identity.Name);
                    return userProfile;
                }
                catch (Exception e)
                {
                    return null;
                }
            }
        }
    }
}