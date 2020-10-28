using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LoanOFFER.Web.Models.AuditTrail
{
    public class SimbrellaLoanTracker
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public bool FetchedData { get; set; }
        public string LoginUser { get; set; }
        public string CustomerId { get; set; }
    }
}