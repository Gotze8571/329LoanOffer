using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoanOFFER.Data.BusinessLogic
{
    public class RemittaLoan
    {
        public int Id { get; set; }
        public string Request { get; set; }
        public string PhoneNo { get; set; }
        public string StartDate  { get; set; }
        public string EndDate { get; set; }
        public bool SpooledData { get; set; }
        public string LoginUser { get; set; }
        public DateTime Date { get; set; }
        // Request
        public string bvn { get; set; }
        public string mobileNumber { get; set; }
       
    }
}
