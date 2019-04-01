using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SP1.Chalao.Framework.Constants
{
    public class EnumCollection
    {
        public enum UserTypeEnum
        {
            Admin = 1,
            Employee =2,
            Rider = 3
        }

        public static List<EnumDetail> getUserType()
        {
            var list = new List<EnumDetail>();
            list.Add(new EnumDetail
            {
                ID = 1, Name = "Admin"
            });

            list.Add(new EnumDetail
            {
                ID = 2, Name = "Employee"
            });

            list.Add(new EnumDetail
            {
                ID = 3,
                Name = "Rider"
            });
            
            return list;
        }
    }
}
