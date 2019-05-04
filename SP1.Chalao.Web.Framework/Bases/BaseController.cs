using System.Web.Mvc;
using SP1.Chalao.Data;
using SP1.Chalao.Repo;

namespace SP1.Chalao.Web.Framework.Bases
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

        private UserRepo _userRepo;

        public UserRepo UserRepo
        {
            get
            {
                if (_userRepo == null)
                    _userRepo = new UserRepo();

                return _userRepo;
            }
        }

        private AdminRepo _adminRepo;

        public AdminRepo AdminRepo
        {
            get
            {
                if (_adminRepo == null)
                    _adminRepo = new AdminRepo();

                return _adminRepo;
            }
        }

        private EmployeeRepo _employeeRepo;

        public EmployeeRepo EmployeeRepo
        {
            get
            {
                if (_employeeRepo == null)
                    _employeeRepo = new EmployeeRepo();

                return _employeeRepo;
            }
        }

        private RiderRepo _riderRepo;

        public RiderRepo RiderRepo
        {
            get
            {
                if (_riderRepo == null)
                    _riderRepo = new RiderRepo();

                return _riderRepo;
            }
        }

        private BikeRepo _bikeRepo;

        public BikeRepo BikeRepo
        {
            get
            {
                if (_bikeRepo == null)
                    _bikeRepo = new BikeRepo();

                return _bikeRepo;
            }
        }

        private BookRepo _bookRepo;
        public BookRepo BookRepo
        {
            get
            {
                if (_bookRepo == null)
                    _bookRepo = new BookRepo();

                return _bookRepo;
            }
        }

    }


}
