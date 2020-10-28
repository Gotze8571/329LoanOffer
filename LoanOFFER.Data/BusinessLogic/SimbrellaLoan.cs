using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoanOFFER.Data.BusinessLogic
{
    public class SimbrellaLoan
    {
        public int Id { get; set; }
        public string Request { get; set; }
        public string customerId { get; set; }
        public DateTime RequestTime { get; set; }
        public DateTime LogDate { get; set; }
    }
}
