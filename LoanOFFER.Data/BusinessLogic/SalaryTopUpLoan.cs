using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoanOFFER.Data.BusinessLogic
{
    public class SalaryTopUpLoan
    {
        [Key]
        public int RecID  { get; set; }
        public string RequestID { get; set; }
        public string SourcePhone { get; set; }
        public string Channel { get; set; }
        public string RequestType { get; set; }
        public string RequestDate { get; set; }
        public string ResponseDate { get; set; }
        public string Duration { get; set; }
        public string ResponseCode { get; set; }
        public string ResponseDescr { get; set; }
        public string Remark { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public bool SpooledData { get; set; }
        public string LoginUser { get; set; }
        public DateTime Date { get; set; }
    }
}
