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
    public class FastCashController : Controller
    {
        // GET: FastCash
        private readonly LoanReportDbContext context;
        private static Logger logger = LogManager.GetCurrentClassLogger();
        public FastCashController()
        {
            context = new LoanReportDbContext();
        }
      
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
                    return View("Index", new FastCashList().ToList().ToPagedList(pageNumber, pageSize));
                }
                if ((startDate != null) || (endDate != null))
                {
                    var result = dataConnector.GetFastCashLoan(startDate, endDate).ToPagedList(pageNumber, pageSize);
                    ViewBag.startDate = this.Request.Params["RequestDate"];
                    ViewBag.endDate = this.Request.Params["LogDate"];
                    ViewBag.phoneNo = this.Request.Params["SourcePhone"];
                    ViewBag.channel = this.Request.Params["Channel"];
                    //ViewBag.recID = this.Request.Params["RecID"];

                    FashcashLoan loan = new FashcashLoan()
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
                    context.FastcashLoans.Add(loan);
                    context.SaveChanges();
                    logger.Info("start date:" + startDate);
                    logger.Info("end date:" + endDate);
                    logger.Info("USSD FAST CASH Loan Report Loaded Successful");
                    return View(result);
                }
                else
                {
                    logger.Info("Please, Contact Buisness Automation to Resolve the Issue.");
                    ViewBag.Message("USSD FAST CASH Loan information is incomplete to spool the data!");
                    ErrorLoan error = new ErrorLoan
                    {
                        StartDate = startDate,
                        EndDate = endDate,
                        FetchedData = false,
                        LoginUser = userId,
                        ErrorName = "USSD FAST CASH Loan Report Error spooling data!!",
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
                    FetchedData = true,
                    LoginUser = userId,
                    ErrorName = "USSD FAST CASH Loan Report caught an Exception.",
                    ErrorDate = DateTime.Now
                };
                context.Errors.Add(error);
                context.SaveChanges();

            }

            //ViewBag.Message = "Salary Top Up Report would be up shortly";
            return View();
        }

        public FileContentResult ExportToExcel(int? page, string startDate, string endDate)
        {

            var PhoneNo = Request.Params["phoneNo"];
            int pageNumber = (page ?? 1);
            const int pageSize = 20;
            //StageDb stagereport = new StageDb();
            LoanOfferDb report = new LoanOfferDb();
            //ViewBag.startDate = this.Request.Params["startDate"];
            //ViewBag.endDate = this.Request.Params["endDate"];
            if ((startDate != null) || (endDate != null))
            {
                FastCashList list = new FastCashList();
                list = report.GetFastCashLoan(startDate, endDate);
                string[] columns = { "LoanID", "RequestID", "SourcePhone", "LoanType", "Amount", "CustType", "Channel", "BrokerCode", "RespCode",
                "RespDescr", "TranDate", "Address_1", "State", "FirstName", "LastName", "Account", "network", "CustomerID"};
                byte[] filecontent = ExcelExportHelper.ExportExcel(list, "", true, columns);
                logger.Info("USSD Fast Cash Loan Report exported successfully");
                string userId = Session["UserId"] as string;
                Export export = new Export
                {
                    ExportedDate = DateTime.Now,
                    ReportName = "FastCashLoanOffer",
                    LoginUser = userId
                };
                context.Exports.Add(export);
                context.SaveChanges();
                return File(filecontent, ExcelExportHelper.ExcelContentType, "USSSDFastCashLoanOffer.xlsx");
            }
            else
            {
                FastCashList list = null;
                list = report.GetFastCashLoan(startDate, endDate);
                string[] columns = { "LoanID", "RequestID", "SourcePhone", "LoanType", "Amount", "CustType", "Channel", "BrokerCode", "RespCode",
                "RespDescr", "TranDate", "Address_1", "State", "FirstName", "LastName", "Account", "network", "CustomerID"};
                byte[] filecontent = ExcelExportHelper.ExportExcel(list, "", true, columns);
                logger.Info("Fast Cash Loan Report exported successfully");
                string userId = Session["UserId"] as string;
                Export export = new Export
                {
                    ExportedDate = DateTime.Now,
                    ReportName = "FastCashLoanOffer",
                    LoginUser = userId
                };
                context.Exports.Add(export);
                context.SaveChanges();
                return File(filecontent, ExcelExportHelper.ExcelContentType, "USSSDFastCashLoanOffer.xlsx");

                //logger.Info("Please, Contact Buisness Automation to Resolve the Issue.");
                //ViewBag.Message("Unable to download the data on excel!");
            }
        }
        public ActionResult PrintViewToPdf()
        {
            var report = new ActionAsPdf("");
            return report;
        }
    }
}