using LoanOFFER.Data.BusinessLogic;
using LoanOFFER.Data.BusinessObject;
using LoanOFFER.Data.DAL;
using LoanOFFER.Web.DAL;
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

namespace LoanOFFER.Web.Controllers
{
    public class SalaryTopUpReportController : Controller
    {
        private readonly LoanReportDbContext context;
        private static Logger logger = LogManager.GetCurrentClassLogger();
        public SalaryTopUpReportController()
        {
            context = new LoanReportDbContext();
        }
        // GET: SalaryTopUpReport
        //[OutputCache(Duration = 60)]
        //[OutputCache(CacheProfile = "Cache10Min")]
        public ActionResult Index(int? page, string search)
        {
            var logger = NLog.LogManager.GetCurrentClassLogger();
            logger.Info(DateTime.Now);

            int pageNumber = (page ?? 1);
            const int pageSize = 20;
            string userId = Session["UserId"] as string;
            var startDate = Request.Params["RequestDate"];
            var endDate = Request.Params["LogDate"];
            var phoneNo = Request.Params["SourcePhone"];
            var channel = Request.Params["Channel"];
            //var recID = Request.Params["RecID"];
            LoanOfferDb dataConnector = new LoanOfferDb();

            try
            {
                if (string.IsNullOrWhiteSpace(startDate) || string.IsNullOrWhiteSpace(endDate))
                {
                    return View("Index", new SalaryTopUpPlusList().ToList().ToPagedList(pageNumber, pageSize));
                }
                if ((startDate != null) || (endDate != null))
                {
                    var result = dataConnector.GetSalaryTopPlus(startDate, endDate).ToPagedList(pageNumber, pageSize);
                    ViewBag.startDate = this.Request.Params["RequestDate"];
                    ViewBag.endDate = this.Request.Params["LogDate"];
                    ViewBag.phoneNo = this.Request.Params["SourcePhone"];
                    ViewBag.channel = this.Request.Params["Channel"];
                    //ViewBag.recID = this.Request.Params["RecID"];

                    SalaryTopUpLoan salary = new SalaryTopUpLoan()
                    {
                        // RecID = int.Parse(recID),
                        SourcePhone = phoneNo,
                        Channel = channel,
                        StartDate = startDate,
                        EndDate = endDate,
                        SpooledData = true,
                        LoginUser = userId,
                        Date = DateTime.Now
                    };
                    context.SalaryTopUpLoans.Add(salary);
                    context.SaveChanges();
                    logger.Info("start date:" + startDate);
                    logger.Info("end date:" + endDate);
                    logger.Info("Salary Top-Up Plus Loan Report Loaded Successful");
                    return View(result);
                }
                else
                {
                    logger.Info("Please, Contact Buisness Automation to Resolve the Issue.");
                    ViewBag.Message("Salary Top-Up Plus Loan information is incomplete to spool the data!");
                    ErrorLoan error = new ErrorLoan
                    {
                        StartDate = startDate,
                        EndDate = endDate,
                        FetchedData = false,
                        LoginUser = userId,
                        ErrorName = "Salary Top-Up Plus Report Error spooling data!!",
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
                //ErrorLoan error = new ErrorLoan
                //{
                //    StartDate = startDate,
                //    EndDate = endDate,
                //    FetchedData = true,
                //    LoginUser = userId,
                //    ErrorName = "Salary Top-Up Plus Report caught an Exception.",
                //    ErrorDate = DateTime.Now
                //};
                //context.Errors.Add(error);
                //context.SaveChanges();

            }

            //ViewBag.Message = "Salary Top Up Report would be up shortly";
            return View();
        }

        public FileContentResult ExportToExcel(int? page, string startDate, string endDate)
        {
            var PhoneNo = Request.Params["phoneNo"];
            int pageNumber = (page ?? 1);
            const int pageSize = 20;
            LoanOfferDb report = new LoanOfferDb();
            //ViewBag.startDate = this.Request.Params["startDate"];
            //ViewBag.endDate = this.Request.Params["endDate"];
            if ((startDate != null) || (endDate != null))
            {
                SalaryTopUpPlusList list = new SalaryTopUpPlusList();
                list = report.GetSalaryTopPlus(startDate, endDate);
                string[] columns = { "LoanID", "RequestID", "SourcePhone", "LoanType", "Amount", "CustType", "Channel", "BrokerCode", "RespCode",
                "RespDescr", "TranDate", "Address_1", "State", "FirstName", "LastName", "Account", "network", "CustomerID"};
                byte[] filecontent = ExcelExportHelper.ExportExcel(list, "", true, columns);
                logger.Info("Salary Top-Up Report exported successfully");
                string userId = Session["UserId"] as string;
                Export export = new Export
                {
                    ExportedDate = DateTime.Now,
                    ReportName = "SalaryTopUpLoanOffer",
                    LoginUser = userId
                };
                context.Exports.Add(export);
                context.SaveChanges();
                return File(filecontent, ExcelExportHelper.ExcelContentType, "SalaryTopUpLoanOffer.xlsx");
            }
            else
            {
                SalaryTopUpPlusList list = null;
                list = report.GetSalaryTopPlus(startDate, endDate);
                string[] columns = { "LoanID", "RequestID", "SourcePhone", "LoanType", "Amount", "CustType", "Channel", "BrokerCode", "RespCode",
                "RespDescr", "TranDate", "Address_1", "State", "FirstName", "LastName", "Account", "network", "CustomerID"};
                byte[] filecontent = ExcelExportHelper.ExportExcel(list, "", true, columns);
                logger.Info("Salary Top-Up Report exported successfully");
                string userId = Session["UserId"] as string;
                Export export = new Export
                {
                    ExportedDate = DateTime.Now,
                    ReportName = "SalaryTopUpLoanOffer",
                    LoginUser = userId
                };
                context.Exports.Add(export);
                context.SaveChanges();
                return File(filecontent, ExcelExportHelper.ExcelContentType, "SalaryTopUpLoanOffer.xlsx");

                //logger.Info("Please, Contact Buisness Automation to Resolve the Issue.");
                //ViewBag.Message("Unable to download the data on excel!");
            }
        }
        public ActionResult PrintViewToPdf()
        {
             
             return  new ActionAsPdf("Index")
             {
                FileName = Server.MapPath("~/Content/SPL.pdf")
             };
            
        }
        public ActionResult SPLViewToPdf(string startDate, string endDate)
        {
            LoanOfferDb db = new LoanOfferDb();

            SalaryTopUpPlusList list = new SalaryTopUpPlusList();
            list = db.GetSalaryTopPlus(startDate, endDate);

            //var report = new PartialViewAsPdf("~/Views/Shared/ExportToPdfSPL.cshtml", list);

            logger.Info("SPL Report exported successfully");
            //return report;

            return new PartialViewAsPdf("~/Views/Shared/ExportToPdfSPL.cshtml", list)
            {
                //FileName = Server.MapPath("~/Content/Relato.pdf"),
                PageOrientation = Rotativa.Options.Orientation.Landscape,
                PageSize = Rotativa.Options.Size.A4
            };
        }
    }
}