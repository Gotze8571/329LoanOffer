using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace LoanOFFER.Data.BusinessLogic
{
    public class SimbrellaViewModel
    {
        [Display(Name = "ID")]
        public string Id { get; set; }
        [Display(Name = "Customer Request")]
        public string Request { get; set; }
        [Display(Name = "Customer ID")]
        public string customerId { get; set; }
        [Display(Name = "Request Date")]
        public string RequestTime { get; set; }
        [Display(Name = "Customer Response")]
        public string Response { get; set; }
        [Display(Name = "Response Date")]
        public string ResponseTime { get; set; }
        [Display(Name = "Date Loan Offered ")]
        public string LogDate { get; set; }
        [Display(Name = "Response Code")]
        public string ResponseCode { get; set; }
        [Display(Name = "Loan Type")]
        public string LoanType { get; set; }
        [Display(Name = "Customer Name")]
        public string CustomerName { get; set; }
        [Display(Name = "Customer Address 1")]
        public string CustomerAddress1 { get; set; }
        //[Display(Name = "Customer Address 2")]
        //public string CustomerAddress2 { get; set; }
        //[Display(Name = "Customer Address 3")]
        //public string CustomerAddress3 { get; set; }
        [Display(Name = "Branch Sol ID")]
        public string BranchSolID { get; set; }
        [Display(Name = "Branch SBU")]
        public string BranchSBU { get; set; }
        [Display(Name = "Broker Code")]
        public string BrokerCode { get; set; }
        public SimbrellaLoan SimbrellaLoan { get; set; }
     
    }
}
