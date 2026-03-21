using System.Web.Mvc;

namespace IncidentReportingSystem.Controllers
{
    public class IncidentController : Controller
    {
        public ActionResult Index() => View();
        public ActionResult LoginPage() => View();
        public ActionResult RegistrationPage() => View();
        public ActionResult ResidentDashboard() => View();
        public ActionResult OfficialDashboard() => View();
        public ActionResult AdminDashboard() => View();
    }
}