using LoanOFFER.Data.BusinessObject;
using LoanOFFER.Web.Models;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using LoanOFFER.Data.DAL;
using LoanOFFER.Web.Models.AuditTrail;
using System.Web.UI.WebControls;
using System.IO;
using System.Web.UI;
using LoanOFFER.Data.BusinessLogic;
using LoanOFFER.Web.DAL;
using Rotativa;
using System.Dynamic;

namespace LoanOFFER.Web.Controllers
{
    public class SimbrellaReportController : Controller
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private readonly LoanReportDbContext context;
        public SimbrellaReportController()
        {
            context = new LoanReportDbContext();
        }
        // GET: Simbrella
       // [OutputCache(Duration = 60)]
        //[OutputCache(CacheProfile = "Cache10Min")]
        public ActionResult Index(int? page, string search)
        {
            var logger = NLog.LogManager.GetCurrentClassLogger();
            logger.Info(DateTime.Now);

            int pageNumber = (page ?? 1);
            const int pageSize = 20;
            string userId = Session["UserId"] as string;
            var startDate = Request.Params["RequestTime"];
            var endDate = Request.Params["LogDate"];
            var CustId = Request.Params["customerid"];
           // var accountNo = Request.Params["accountNo"];
            
            LoanOfferDb dataConnector = new LoanOfferDb();

            try
            {
                // if (string.IsNullOrWhiteSpace(startDate) || string.IsNullOrWhiteSpace(endDate) || string.IsNullOrWhiteSpace(CustId))
                if (string.IsNullOrWhiteSpace(startDate) || string.IsNullOrWhiteSpace(endDate))
                {
                    return View("Index", new SimbrellaLoanList().ToList().ToPagedList(pageNumber, pageSize));
                }
                if (!string.IsNullOrEmpty(CustId))
                {
                    var result = dataConnector.GetSimbrellaLoanWithCustId(startDate, endDate, CustId).ToPagedList(pageNumber, pageSize);
                    var afterResult = dataConnector.GetCustomerMoreInfoWithFinacle(CustId).ToPagedList(pageNumber, pageSize).FirstOrDefault();
                    //var combineResult = new Tuple<SimbrellaLoanList>(GetSimbrellaLoanWithCustId(startDate, endDate, CustId), GetCustomerMoreInfoWithFinacle(CustId));

                    foreach (var item in result) {
                        item.CustomerName = afterResult.CustomerName;
                        item.CustomerAddress1 = afterResult.CustomerAddress1;
                        item.BrokerCode = afterResult.BrokerCode;
                        item.BranchSolID = afterResult.BranchSolID;
                        item.BranchSBU = afterResult.BranchSBU;
                        //item.CustomerAddress2 = afterResult.CustomerAddress2;
                        //item.CustomerAddress3 = afterResult.CustomerAddress3;
                    }

                    ViewBag.startDate = this.Request.Params["RequestTime"];
                    ViewBag.endDate = this.Request.Params["LogDate"];
                    ViewBag.CustId = this.Request.Params["customerId"];
                    ViewBag.accountNo = this.Request.Params["accountNo"];

                    SimbrellaLoanTracker sim = new SimbrellaLoanTracker
                    {
                        StartDate = startDate,
                        EndDate = endDate,
                        FetchedData = true,
                        LoginUser = userId,
                        CustomerId = CustId,
                        Date = DateTime.Now
                    };
                    context.SimbrellaLoanTrackers.Add(sim);
                    context.SaveChanges();
                    logger.Info("start date:" + startDate);
                    logger.Info("end date:" + endDate);
                    logger.Info(" Simbrella Loan Offer Report Loaded Successfully");
                    return View(result);

                    
                }
                if ((startDate != null) || (endDate != null))
                   // if (string.IsNullOrWhiteSpace(startDate) || string.IsNullOrWhiteSpace(endDate))
                {
                    var result = dataConnector.GetSimbrellaLoanDb(startDate, endDate, CustId).ToPagedList(pageNumber, pageSize);
                    // var resultSecond = dataConnector.GetMoreInfo(accountNo).ToPagedList(pageNumber, pageSize);
                    ViewBag.startDate = this.Request.Params["RequestTime"];
                    ViewBag.endDate = this.Request.Params["LogDate"];
                    ViewBag.CustId = this.Request.Params["customerId"];
                    ViewBag.accountNo = this.Request.Params["accountNo"];

                    SimbrellaLoanTracker sim = new SimbrellaLoanTracker
                    {
                        StartDate = startDate,
                        EndDate = endDate,
                        FetchedData = true,
                        LoginUser = userId,
                        CustomerId = CustId,
                        Date = DateTime.Now
                    };
                    context.SimbrellaLoanTrackers.Add(sim);
                    context.SaveChanges();
                    logger.Info("start date:" + startDate);
                    logger.Info("end date:" + endDate);
                    logger.Info(" Simbrella Loan Offer Report Loaded Successfully");
                    return View(result);
                }
                else
                {
                    ErrorLoan error = new ErrorLoan
                    {
                        //Id = id,
                        StartDate = startDate,
                        EndDate = endDate,
                        FetchedData = false,
                        LoginUser = userId,
                        ErrorName = "Simbrella Report Error in spooling data!!",
                        ErrorDate = DateTime.Now
                    };
                    context.Errors.Add(error);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                //ErrorLoan error = new ErrorLoan
                //{
                //    //Id = id,
                //    StartDate = startDate,
                //    EndDate = endDate,
                //    FetchedData = false,
                //    LoginUser = userId,
                //    ErrorName = "Simbrella Report Error caught an Exception.",
                //    ErrorDate = DateTime.Now
                //};
                //context.Errors.Add(error);
                //context.SaveChanges();
            }

            return View();
        }

        public FileContentResult ExportToExcel(int? page, string startDate, string endDate)
        {
            var CustId = Request.Params["customerId"];
            int pageNumber = (page ?? 1);
            const int pageSize = 20;
           // string userId = Session["UserId"] as string;
           // var startDate = Request.Params["RequestTime"];
          //  var endDate = Request.Params["LogDate"];
           // var CustId = Request.Params["customerid"];

            LoanOfferDb report = new LoanOfferDb();
            //ViewBag.startDate = this.Request.Params["startDate"];
            //ViewBag.endDate = this.Request.Params["endDate"];

            if (!string.IsNullOrEmpty(CustId))
            {
                SimbrellaLoanList list = new SimbrellaLoanList();
                
                list = report.GetSimbrellaLoanWithCustId(startDate, endDate, CustId);
                var afterResult = report.GetCustomerMoreInfoWithFinacle(CustId);
                //var combineResult = new Tuple<SimbrellaLoanList>(GetSimbrellaLoanWithCustId(startDate, endDate, CustId), GetCustomerMoreInfoWithFinacle(CustId));

                //foreach (var item in list)
                //{
                //    item.CustomerName = afterResult.CustomerName;
                //    item.CustomerAddress1 = afterResult.CustomerAddress1;
                //    item.BrokerCode = afterResult.BrokerCode;
                //    item.BranchSolID = afterResult.BranchSolID;
                //    item.BranchSBU = afterResult.BranchSBU;

                //}

                string[] columns = { "Id", "customerid", "RequestTime", "Response", "ResponseTime", "LogDate", "ResponseCode" };
                byte[] filecontent = ExcelExportHelper.ExportExcel(list, "", true, columns);
                logger.Info("Simbrella Report exported successfully");
                string userId = Session["UserId"] as string;
                Export export = new Export
                {
                    ExportedDate = DateTime.Now,
                    ReportName = "SimbrellaLoanOffer",
                    LoginUser = userId
                };
                context.Exports.Add(export);
                context.SaveChanges();
                return File(filecontent, ExcelExportHelper.ExcelContentType, "SimbrellaLoanOffer.xlsx");

            }
            if ((startDate != null) || (endDate != null))
            {
                SimbrellaLoanList list = new SimbrellaLoanList();
                list = report.GetSimbrellaLoanDb(startDate, endDate, CustId);
                string[] columns = {"Id", "customerid", "RequestTime", "Response", "ResponseTime", "LogDate", "ResponseCode"};
                byte[] filecontent = ExcelExportHelper.ExportExcel(list, "", true, columns);
                logger.Info("Simbrella Report exported successfully");
                string userId = Session["UserId"] as string;
                Export export = new Export
                {
                    ExportedDate = DateTime.Now,
                    ReportName = "SimbrellaLoanOffer",
                    LoginUser = userId
                };
                context.Exports.Add(export);
                context.SaveChanges();
                return File(filecontent, ExcelExportHelper.ExcelContentType, "SimbrellaLoanOffer.xlsx");
            }
            else
            {
                SimbrellaLoanList list = null;
                list = report.GetSimbrellaLoanDb(startDate, endDate, CustId);
                string[] columns = { "Id", "customerid", "RequestTime", "Response", "ResponseTime", "LogDate", "ResponseCode" };
                byte[] filecontent = ExcelExportHelper.ExportExcel(list, "", true, columns);
                logger.Info(" Simbrella Report exported successfully");
                string userId = Session["UserId"] as string;
                Export export = new Export
                {
                    ExportedDate = DateTime.Now,
                    ReportName = "SimbrellaLoanOffer",
                    LoginUser = userId
                };
                context.Exports.Add(export);
                context.SaveChanges();
                return File(filecontent, ExcelExportHelper.ExcelContentType, "SimbrellaLoanOffer.xlsx");

                //logger.Info("Please, Contact Buisness Automation to Resolve the Issue.");
                //ViewBag.Message("Unable to download the data on excel!");
            }
           // return File(filecontent, ExcelExportHelper.ExcelContentType, "SimbrellaLoanOffer.xlsx");
        }
        public ActionResult PrintViewToPdf(string startDate, string endDate, string CustId)
        {
            LoanOfferDb report = new LoanOfferDb();
            if ((startDate != null) || (endDate != null))
            {
                SimbrellaLoanList list = new SimbrellaLoanList();
               
                list = report.GetSimbrellaLoanDb(startDate, endDate, CustId);
                string userId = Session["UserId"] as string;
                Export export = new Export
                {
                    ExportedDate = DateTime.Now,
                    ReportName = "SimbrellaLoanOffer",
                    LoginUser = userId
                };
                context.Exports.Add(export);
                context.SaveChanges();
                return new PartialViewAsPdf()
                {
                    FileName = "SimbrellaLoanOffer.pdf"
                };
            }
            else
            {
                SimbrellaLoanList list = null;
                list = report.GetSimbrellaLoanDb(startDate, endDate, CustId);
                string userId = Session["UserId"] as string;
                Export export = new Export
                {
                    ExportedDate = DateTime.Now,
                    ReportName = "SimbrellaLoanOffer",
                    LoginUser = userId
                };
                context.Exports.Add(export);
                context.SaveChanges();
                return new PartialViewAsPdf("_JobPdf", list)
                {
                    FileName = "SimbrellaLoanOffer.pdf"
                };
            }
        }
        public ActionResult SimbrellaPartialViewToPdf(string startDate, string endDate, string CustId)
        {
            LoanOfferDb db = new LoanOfferDb();

            SimbrellaLoanList list = new SimbrellaLoanList();
            list = db.GetSimbrellaLoanDb(startDate, endDate, CustId);

            //var report = new PartialViewAsPdf("~/Views/Shared/ExportToPdfSimbrella.cshtml", list);
            logger.Info("Simbrella Report exported successfully");
            //return report;
            return new PartialViewAsPdf("~/Views/Shared/ExportToPdfSimbrella.cshtml", list)
            {
                //FileName = Server.MapPath("~/Content/Relato.pdf"),
                PageOrientation = Rotativa.Options.Orientation.Landscape,
                PageSize = Rotativa.Options.Size.A2
            };
        }

        // Converting cshtml to PDf
        //public string RenderViewAsString(string viewName, object model)
        //{
        //    // create a string writer to receive the HTML code
        //    StringWriter stringWriter = new StringWriter();

        //    // get the view to render
        //    ViewEngineResult viewResult = ViewEngines.Engines.FindView(ControllerContext,
        //              viewName, null);
        //    // create a context to render a view based on a model
        //    ViewContext viewContext = new ViewContext(
        //        ControllerContext,
        //        viewResult.View,
        //        new ViewDataDictionary(model),
        //        new TempDataDictionary(),
        //        stringWriter
        //    );

        //    // render the view to a HTML code
        //    viewResult.View.Render(viewContext, stringWriter);

        //    // return the HTML code
        //    return stringWriter.ToString();
        //}

        //[HttpPost]
        //public ActionResult ConvertHtmlPageToPdf()
        //{
        //    // get the HTML code of this view
        //    string htmlToConvert = RenderViewAsString("Index", null);

        //    // the base URL to resolve relative images and css
        //    String thisPageUrl = this.ControllerContext.HttpContext.Request.Url.AbsoluteUri;
        //    String baseUrl = thisPageUrl.Substring(0, thisPageUrl.Length -
        //        "Home/ConvertThisPageToPdf".Length);

        //    // instantiate the HiQPdf HTML to PDF converter
        //    HtmlToPdf htmlToPdfConverter = new HtmlToPdf();

        //    // hide the button in the created PDF
        //    htmlToPdfConverter.HiddenHtmlElements = new string[]
        //               { "#convertThisPageButtonDiv" };

        //    // render the HTML code as PDF in memory
        //    byte[] pdfBuffer = htmlToPdfConverter.ConvertHtmlToMemory(htmlToConvert, baseUrl);

        //    // send the PDF file to browser
        //    FileResult fileResult = new FileContentResult(pdfBuffer, "application/pdf");
        //    fileResult.FileDownloadName = "ThisMvcViewToPdf.pdf";

        //    return fileResult;
        //}
    }
}