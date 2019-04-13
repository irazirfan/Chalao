using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATP2.SMS.Repo;
using SP1.Chalao.Data;

namespace RiderRepo
{
    public class BaseRepo
    {
        private ChalaoDBContext _context;

        public ChalaoDBContext Context
        {
            get
            {
                if (_context == null)
                    _context = new ChalaoDBContext();

                return _context;
            }
        }
    }
}
