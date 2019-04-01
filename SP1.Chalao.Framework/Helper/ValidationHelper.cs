using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SP1.Chalao.Framework.Helper
{
    public class ValidationHelper
    {
        public static bool IsValidString(string value)
        {
            if (string.IsNullOrEmpty(value) || string.IsNullOrWhiteSpace(value))
                return false;

            return true;
        }

        public static bool IsValidInt(string value)
        {
            try
            {
                int i = Int32.Parse(value);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public static bool IsValidFloat(string value)
        {
            try
            {
                float i = float.Parse(value);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}
