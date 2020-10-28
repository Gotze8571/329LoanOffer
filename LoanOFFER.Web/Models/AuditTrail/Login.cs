using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LoanOFFER.Web.Models.AuditTrail
{
    public class Login
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Group { get; set; }
        public DateTime Date { get; set; }
        public string IPAddress { get; set; }
        public string HostName { get; set; }
    }
}