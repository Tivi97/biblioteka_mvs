//using System.Web;
//using System.Web.Mvc;
//using ClubClient.Models.Account;

//namespace ClubClient.Controllers
//{
//    public class AccountController : ParentController
//    {
//        public ActionResult Index()
//        {
//            return Redirect("/Account/Login");
//        }

//        [HttpGet]
//        public ActionResult Login()
//        {
//            return View();
//        }

//        [HttpPost]
//        public ActionResult Login(AuthModel user)
//        {
//            if (ModelState.IsValid)
//            {
//                TempData["message"] = new AccountModel().Auth(user.Username, user.Password);
//                if (Session["login"] == null)
//                {
//                    return View();
//                }
//                else
//                {
//                    return Redirect("/Account/UserProfile");
//                }
//            }
//            else
//            {
//                SaveMessages();
//                return View();
//            }

//        }

//        public ActionResult Logout()
//        {
//            Session["admin"] = null;
//            Session["coach"] = null;
//            Session["login"] = null;
//            Session["id"] = null;
//            return Redirect("/Account/Login");
//        }

//        [HttpGet]
//        public ActionResult Register()
//        {
//            return View();
//        }

//        [HttpPost]
//        public ActionResult Register(RegisterModel user)
//        {
//            if (ModelState.IsValid)
//            {
//                TempData["message"] = new AccountModel().Register(user.Fio, user.Phone, user.Sex, user.Birthday, user.Login, user.Password);
//                if (Session["login"] == null)
//                {
//                    return View();
//                }
//                else
//                {
//                    return Redirect("/Account/UserProfile");
//                }
//            }
//            else
//            {
//                SaveMessages();
//                return View();
//            }
//        }

//        [HttpGet]
//        public ActionResult UserProfile()
//        {
//            if (Session["login"] != null)
//            {
//                ViewBag.HikesList = new AccountModel().GetHikesList();
//                ViewBag.ExpList = new AccountModel().GetExpList(int.Parse(Session["id"].ToString()));
//                return View(new AccountModel().GetProfile(Session["id"]));
//            }
//            else
//            {
//                TempData["message"] = "Вы не авторизованы!";
//                return Redirect("/Account/Login");
//            }
//        }

//        [HttpPost]
//        public ActionResult UserProfile(ProfileModel profile)
//        {
//            if (Session["login"] != null)
//            {
//                if (ModelState.IsValid)
//                {
//                    TempData["message"] = new AccountModel().UpdateProfile(profile, Session["id"]);
//                }
//                else
//                {
//                    SaveMessages();
//                }
//                return Redirect("/Account/UserProfile");
//            }
//            else
//            {
//                TempData["message"] = "Вы не авторизованы!";
//                return Redirect("/Account/Login");
//            }
//        }

//        [HttpPost]
//        public ActionResult AddExpereince(ProfileModel profile)
//        {
//            if (Session["login"] != null)
//            {
//                profile.Exp.IdUser = int.Parse(Session["id"].ToString());
//                TempData["message"] = new AccountModel().AddExperience(profile.Exp);
//                return Redirect("/Account/UserProfile");
//            }
//            else
//            {
//                TempData["message"] = "Вы не авторизованы!";
//                return Redirect("/Account/Login");
//            }

//        }

//        public ActionResult GetPdf()
//        {
//            if (Session["login"] != null)
//            {
//                Response.Clear();
//                Response.ContentType = "application/pdf";
//                Response.AddHeader("content-disposition", "attachment;filename=" + "PDFfile.pdf");
//                Response.Cache.SetCacheability(HttpCacheability.NoCache);
//                Response.BinaryWrite(new AccountModel().GetPdf());
//                Response.End();
//                return Redirect("/Account/UserAccount");
//            }
//            else
//            {
//                TempData["message"] = "Вы не авторизованы!";
//                return Redirect("/Account/Login");
//            }

//        }

//    }
//}