using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LoanOFFER.Web.Models.ViewModel
{
    public class SimbrellaViewModel
    {
        // public int ID { get; set; }
        [Display(Name = "Customer ID")]
        public string customerId { get; set; }
        //[Display(Name = "Customer Name")]

        //public string CustomerName { get; set; }

        //public int CustomerId { get; set; }
        [Display(Name = "Phone Number")]
        public string PhoneNo { get; set; }
        [Display(Name = "Account Number")]
        public string msisdn { get; set; }
        //public string CustomerName { get; set; }
        [Display(Name = "Transaction Type")]
        public string TransactionType { get; set; }
        public string transactionId { get; set; }
        //[Display(Name = "Screen Details")]
        //public string ScreenDetails { get; set; }
        [Display(Name = "Loan Type")]
        public string type { get; set; }
        [Display(Name = "Amount")]
        public decimal eligibleAmounts { get; set; }
        [Display(Name = "Broker Code")]
        public string BrokerCode { get; set; }
        [Display(Name = "Response Code")]
        public string resultCode { get; set; }
        [Display(Name = "Traqnsaction Date")]
        public DateTime LogDate { get; set; }
        [Display(Name = "Branch Sol ID")]
        public string BranchSolID { get; set; }
        [Display(Name = "Branch SBU")]
        public string BranchSBU { get; set; }
    }
}