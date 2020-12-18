using LoanOFFER.Data.BusinessLogic;
using LoanOFFER.Data.BusinessObject;
using LoanOFFER.Data.DAL;
using LoanOFFER.Web.Models;
using LoanOFFER.Web.Models.AuditTrail;
using NLog;
using PagedList;
using Rotativa;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
//using Rotativa;

namespace LoanOFFER.Web.Controllers
{
    public class RemittaController : Controller
    {
        private readonly LoanReportDbContext context;
        private static Logger logger = LogManager.GetCurrentClassLogger();
        public RemittaController()
        {
            context = new LoanReportDbContext();
        }
        // GET: Remitta
        //[OutputCache(Duration = 60)]
        [OutputCache(CacheProfile = "Cache10Min")]
        public ActionResult Index(int? page, string search)
        {
            var logger = NLog.LogManager.GetCurrentClassLogger();
            logger.Info(DateTime.Now);

            int pageNumber = (page ?? 1);
            const int pageSize = 20;
            string userId = Session["UserId"] as string;
            var startDate = Request.Params["RequestTime"];
            var endDate = Request.Params["LogDate"];
            var phoneNo = Request.Params["PhoneNo"];
            LoanOfferDb dataConnector = new LoanOfferDb();

            try
            {
                if (string.IsNullOrWhiteSpace(startDate) || string.IsNullOrWhiteSpace(endDate))
                {
                    return View("Index", new RemittaLoanList().ToList().ToPagedList(pageNumber, pageSize));
                }
                if ((startDate != null) || (endDate != null))
                {
                    var result = dataConnector.GetRemittaLoanDb(startDate, endDate).ToPagedList(pageNumber, pageSize);
                    ViewBag.startDate = this.Request.Params["RequestTime"];
                    ViewBag.endDate = this.Request.Params["LogDate"];
                    ViewBag.phoneNo = this.Request.Params["PhoneNo"];

                    RemittaLoan remitta = new RemittaLoan()
                    {
                        PhoneNo = phoneNo,
                        StartDate = startDate,
                        EndDate = endDate,
                        SpooledData = true,
                        LoginUser = userId,
                        Date = DateTime.Now
                    };
                    context.RemittaLoans.Add(remitta);
                    context.SaveChanges();
                    logger.Info("start date:" + startDate);
                    logger.Info("end date:" + endDate);
                    logger.Info(" Remitta Report Loaded Successful");
                    return View(result);
                }
                else
                {
                    logger.Info("Please, Contact Buisness Automation to Resolve the Issue.");
                    ViewBag.Message("Remitta information is incomplete to spool the data!");
                    ErrorLoan error = new ErrorLoan
                    {
                        StartDate = startDate,
                        EndDate = endDate,
                        FetchedData = false,
                        LoginUser = userId,
                        ErrorName = "Remitta information is incomplete to spool the data!",
                        ErrorDate = DateTime.Now
                    };
                    context.Errors.Add(error);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("message", "An error occured");
                logger.Error(ex);
                ErrorLoan error = new ErrorLoan
                {
                    StartDate = startDate,
                    EndDate = endDate,
                    FetchedData = false,
                    LoginUser = userId,
                    ErrorName = "Remitta Report caught an Exception.",
                    ErrorDate = DateTime.Now
                };
                context.Errors.Add(error);
                context.SaveChanges();
            }
            //ViewBag.Message = "Remitta Report would be up shortly";
            return View("Error");
        }
        public FileContentResult ExportToExcel(int? page, string RequestTime, string LogTime)
        {

            var PhoneNo = Request.Params["phoneNo"];
            int pageNumber = (page ?? 1);
            const int pageSize = 20;
            //StageDb stagereport = new StageDb();
            LoanOfferDb report = new LoanOfferDb();
            //ViewBag.startDate = this.Request.Params["startDate"];
            //ViewBag.endDate = this.Request.Params["endDate"];
            if (RequestTime != null)
            {
                RemittaLoanList list = new RemittaLoanList();
                list = report.GetRemittaLoanDb(RequestTime, LogTime);
                string[] columns = { "Request", "PhoneNo", "RequestTime", "LogDate" };
                byte[] filecontent = ExcelExportHelper.ExportExcel(list, "", true, columns);
                logger.Info("Remitta Report exported successfully");
                string userId = Session["UserId"] as string;
                Export export = new Export
                {
                    ExportedDate = DateTime.Now,
                    ReportName = "RemittaLoanOffer",
                    LoginUser = userId
                };
                context.Exports.Add(export);
                context.SaveChanges();
                return File(filecontent, ExcelExportHelper.ExcelContentType, "SimbrellaLoanOffer.xlsx");
            }
            else
            {
                RemittaLoanList list = null;
                list = report.GetRemittaLoanDb(RequestTime, LogTime);
                string[] columns = { "Request", "PhoneNo", "RequestTime", "LogDate" };
                byte[] filecontent = ExcelExportHelper.ExportExcel(list, "", true, columns);
                logger.Info("Remitta Report exported successfully");
                string userId = Session["UserId"] as string;
                Export export = new Export
                {
                    ExportedDate = DateTime.Now,
                    ReportName = "RemittaLoanOffer",
                    LoginUser = userId
                };
                context.Exports.Add(export);
                context.SaveChanges();
                return File(filecontent, ExcelExportHelper.ExcelContentType, "RemittaLoanOffer.xlsx");

                //logger.Info("Please, Contact Buisness Automation to Resolve the Issue.");
                //ViewBag.Message("Unable to download the data on excel!");
            }
            //return File(filecontent, ExcelExportHelper.ExcelContentType, "SimbrellaLoanOffer.xlsx");
        }
        public ActionResult ExportToPdf(int? page, string RequestTime, string LogTime)
        {
            var reportList = new ActionAsPdf("");
            var startDate = Request.Params["RequestTime"];
            var endDate = Request.Params["LogDate"];
            string userId = Session["UserId"] as string;
            try
            {
                if ((RequestTime != null) || (LogTime != null))
                {
                    var PhoneNo = Request.Params["phoneNo"];
                    int pageNumber = (page ?? 1);
                    const int pageSize = 20;

                    
                    LoanOfferDb report = new LoanOfferDb();

                    RemittaLoanList list = new RemittaLoanList();
                    list = report.GetRemittaLoanDb(RequestTime, LogTime);

                    // reportList = report.GetRemittaLoanDb(RequestTime, LogTime, PhoneNo);

                    logger.Info("Remitta pdf Report exported successfully");
                    //string userId = Session["UserId"] as string;
                    Export export = new Export
                    {
                        ExportedDate = DateTime.Now,
                        ReportName = "RemittaLoanOffer",
                        LoginUser = userId
                    };
                    context.Exports.Add(export);
                    context.SaveChanges();
                    return View(reportList);
                }
                else
                {
                    ViewBag.Message("Unable to download the data on pdf!");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("message", "An error occured");
                logger.Error(ex);
                ErrorLoan error = new ErrorLoan
                {
                    StartDate = startDate,
                    EndDate = endDate,
                    FetchedData = true,
                    LoginUser = userId,
                    ErrorName = "Remitta Report caught an Exception.",
                    ErrorDate = DateTime.Now
                };
                context.Errors.Add(error);
                context.SaveChanges();
            }
            
            return null;
        }

    }
}