using System.Web;
using System.Web.Mvc;

//контролер "всевидяще око"
namespace Library.Controllers
{
    public class AccountController : ParentController
    {
        public ActionResult Index()
        {
            return Redirect("/Account/Login");
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Registre()
        {
            return View();
        }



    } 
}