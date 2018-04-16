using System.Web.Mvc;


namespace Library.Controllers
{
    public class ParentController : Controller
    {
        protected void SaveMessages()
        {
            foreach (var value in ModelState.Values)
            {
                foreach (var error in value.Errors)
                {
                    TempData["message"] += error.ErrorMessage + "<br/>";
                }
            }
        }
    }
}