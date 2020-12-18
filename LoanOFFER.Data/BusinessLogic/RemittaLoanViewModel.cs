using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoanOFFER.Data.BusinessLogic
{
    public class RemittaLoanViewModel
    {
        [Display(Name = "Customer Name")]
        public string CustomerName { get; set; }
        [Display(Name = "ID")]
        public string Id { get; set; }
        [Display(Name = "Loan Offer Details")]
        public string Request { get; set; }
        [Display(Name = "Bvn")]
        public string Bvn { get; set; }
        [Display(Name = "phone number")]
        public string phoneNumber { get; set; }
        [Display(Name = "Channel")]
        public string channel { get; set; }
        [Display(Name = "Response Code")]
        public string reasonCode { get; set; }
        [Display(Name = "Account Number")]
        public string PhoneNo { get; set; }
        [Display(Name = "Loan Requested Time")]
        public string  RequestTime { get; set; }
        [Display(Name = "Customer Eligiblity")]
        public string Response { get; set; }
        [Display(Name = "Loan Date")]
        public  string LogDate { get; set; }
        [Display(Name = "Amount")]
        public string Amount { get; set; }
        [Display(Name = "Loan Type")]
        public string LoanType { get; set; }
        [Display(Name = "Transaction Status")]
        public string TransactionStatus { get; set; }
        [Display(Name = "Branch Sol ID")]
        public string BranchSolId { get; set; }
        [Display(Name = "Branch SBU")]
        public string BranchSbu { get; set; }
        [Display(Name = "Broker Code")]
        public string BrokerCode { get; set; }
        public RemittaLoan RemittaLoan { get; set; }
        public RemittaLoanResponse RemittaLoanResponse { get; set; }
    }
    
}


