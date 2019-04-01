using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using SP1.Chalao.Data;
using SP1.Chalao.Entities;
using ATP2.SMS.Repo;

namespace ATP2.SMS.Web.Framework.Bases
{
    public class BaseController : Controller
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

        private UserRepo _UserRepo;

        public UserRepo UserRepo
        {
            get
            {
                if (_UserRepo == null)
                    _UserRepo = new UserRepo();

                return _UserRepo;
            }
        }
    }


}
