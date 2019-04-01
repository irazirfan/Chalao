using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SP1.Chalao.Framework.Objects
{
    public class Result<T>
    {
        public T Data { get; set; }
        public bool HasError { get; set; }
        public bool HasWarning { get; set; }
        public string Message { get; set; }
    }
}
