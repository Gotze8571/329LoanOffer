using LoanOFFER.Data.BusinessLogic;
using LoanOFFER.Web.DAL;
using LoanOFFER.Web.Models;
using LoanOFFER.Web.Models.AuditTrail;
using Microsoft.Owin.Security;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace LoanOFFER.Web.Controllers
{
    public class LoginController : Controller
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private readonly LoanReportDbContext context;
        // GET: Login
        [AllowAnonymous]
        public ActionResult Index(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
             return View();
        }

        //[HttpGet]
        //public ActionResult Login()
        //{
        //   // return RedirectToAction("Login", "Login");
        //     return View();
        //}

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Index(User user, string returnUrl)
        {
            logger.Info("NEW LOGIN");

            if (ModelState.IsValid)
            {
                try
                {
                    LoginUserDB login = new LoginUserDB();
                    //string hostName;
                    //hostName = Dns.GetHostEntry(Request.ServerVariables["REMOTE_HOST"]).HostName;

                    string machineName = System.Environment.MachineName;

                    //ViewBag.Message = hostName;
                    ViewBag.Message = machineName;
                    if (login.ValidLogin(user.UserId, user.Password, machineName))
                    //if (login.ValidLogin(user.UserId, user.Password, hostName))
                    {

                        FormsAuthentication.SetAuthCookie(user.UserId, true);

                        logger.Info("Signed in User: " + user.UserId);

                        string UserId = Session["user.UserId"] as string;
                        //Login log = new Login()
                        //{
                        //    Name = user.UserId,
                        //    Group = "null",
                        //    Date = DateTime.Now,
                        //    // SpooledData = true,
                        //    IPAddress = UserIPAddress.GetIPAddress(),
                        //    HostName = machineName
                        //};
                        //context.Logins.Add(log);
                        //context.SaveChanges();

                        logger.Info("IP Address: " + UserIPAddress.GetIPAddress());
                        //logger.Info("IP Address: " + hostName);
                        logger.Info("IP Address: " + machineName);
                        ViewBag.Message = "hostName";

                       // return RedirectToLocal(returnUrl);
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        ModelState.AddModelError("message", "incorrect login details!");
                        ViewBag.Message = "Incorrect login details";
                        logger.Info("Incorrect login details");
                       // return RedirectToAction("Index", "Login");
                        return View();
                    }
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("message", "Unable to connect to server");
                    logger.Error(ex);
                }
            }
            else 
            {
                ModelState.AddModelError("message", "Server not connected!");
                ViewBag.Message = "Server not connected!";
                ErrorLoan err = new ErrorLoan
                {
                    //StartDate = ,
                    //EndDate = DateTime.Now,
                    LoginUser = user.UserId,
                    FetchedData = false,
                    ErrorName = "ADService not reachable!!",
                    ErrorDate = DateTime.Now
                };
               
            }
            return View(user);

        }
        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }

            return RedirectToAction("Index", "Login");
        }
        public ActionResult Logout(User user, string returnUrl)
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Index");
        }
        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

    }
}