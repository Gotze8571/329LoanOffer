using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoanOFFER.Data.BusinessLogic
{
    public class FastCashEligibleViewModel
    {
        [Display(Name = "Rec ID")]
        public int RecID { get; set; }
        [Display(Name = "Request ID")]
        public string RequestID { get; set; }
        [Display(Name = "Carrier")]
        public string Carrier { get; set; }
        [Display(Name = "Source Phone")]
        public string SourcePhone { get; set; }
        [Display(Name = "Channel")]
        public string Channel { get; set; }
        [Display(Name = "Request Type")]
        public string RequestType { get; set; }
        [Display(Name = "Request Date")]
        public string RequestDate { get; set; }
        [Display(Name = "Duration")]
        public string Duration { get; set; }
        [Display(Name = "Response Code")]
        public string ResponseCode { get; set; }
        [Display(Name = "Response Description")]
        public string ResponseDescr { get; set; }
        [Display(Name = "Log Date")]
        public string LogDate { get; set; }
        [Display(Name = "Remark")]
        public string Remark { get; set; }
    }
}
