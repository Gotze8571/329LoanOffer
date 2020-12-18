using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoanOFFER.Data.BusinessLogic
{
    public class SalaryTopUpViewModel
    {
        [Display(Name = "Loan ID")]   
        public int LoanID { get; set; }
        [Display(Name = "Request ID")]
        public string RequestID { get; set; }
        [Display(Name = "Phone Number")]
        public string SourcePhone { get; set; }
        [Display(Name = "Loan Type")]
        public string LoanType { get; set; }
        [Display(Name = "Amount")]
        public decimal Amount { get; set; }
        [Display(Name = "Customer Account Type")]
        public string CustType { get; set; }
        [Display(Name = "Channel")]
        public string Channel { get; set; }
        [Display(Name = "Broker Code")]
        public string BrokerCode { get; set; }
        [Display(Name = "Response Code")]
        public string RespCode { get; set; }
        [Display(Name = "Response Description")]
        public string RespDescr { get; set; }
        [Display(Name = "Transaction Date")]
        public string TranDate { get; set; }
        [Display(Name = "Customer Address")]
        public string Address_1 { get; set; }
        [Display(Name = "State")]
        public string State { get; set; }
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        [Display(Name = "Account Number")]
        public string Account { get; set; }
        [Display(Name = "Network")]
        public string Network { get; set; }
        [Display(Name = "Customer Id")]
        public string CustomerID { get; set; }
        

    }
}
