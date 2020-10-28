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
        [Display(Name = "RecID")]   
        public string RecID { get; set; }
        [Display(Name = "Request ID")]
        public string RequestID { get; set; }
        [Display(Name = "Phone Number")]
        public string SourcePhone { get; set; }
        [Display(Name = "Channel")]
        public string Channel { get; set; }
        [Display(Name = "Request Type")]
        public string RequestType { get; set; }
        [Display(Name = "Request Date")]
        public string RequestDate { get; set; }
        [Display(Name = "Response Date")]
        public string ResponseDate { get; set; }
        [Display(Name = "Duration")]
        public string Duration { get; set; }
        [Display(Name = "Response Code")]
        public string ResponseCode { get; set; }
        [Display(Name = "Response Description")]
        public string ResponseDescr { get; set; }
        [Display(Name = "Account Number")]
        public string Remark { get; set; }
    }
}
