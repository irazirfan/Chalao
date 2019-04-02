using System.Web.Mvc;
using ATP2.SMS.Repo;
using SP1.Chalao.Data;

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
