using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LoanOFFER.Web.Models.AuditTrail
{
    public class Export
    {
        [Key]
        public int Id { get; set; }
        public string ReportName { get; set; }
        public DateTime ExportedDate { get; set; }
        public string LoginUser { get; set; }
    }
}