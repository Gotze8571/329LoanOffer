using LoanOFFER.Web.Models;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace LoanOFFER.Web.Controllers
{

    public class HomeController : Controller
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        //[Authorize]
        
        public ActionResult Index()
        {
            var logger = NLog.LogManager.GetCurrentClassLogger();
            logger.Info("Home Page Loaded: " + DateTime.Now);
            return View();
        }
    }  

}
