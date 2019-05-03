using System.Web.Mvc;

namespace SP1.Chalao.Web.Controllers
{
    public class ParkingZoneController : Controller
    {
        // GET: ParkingZone
        [AllowAnonymous]
        public ActionResult Index()
        {
            return View();
        }
    }
}