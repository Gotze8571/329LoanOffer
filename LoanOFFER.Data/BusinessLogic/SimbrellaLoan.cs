using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace LoanOFFER.Data.BusinessLogic
{
    
    public class SimbrellaLoan
    {
        public int Id { get; set; }
        public string Request { get; set; }
       
        public DateTime RequestTime { get; set; }
        public DateTime LogDate { get; set; }
        public string type { get; set; }
        public string transactionId { get; set; }
        public string customerId { get; set; }
        [Display(Name = "Account Number")]
        public string accountId { get; set; }
        [Display(Name = "Phone Number")]
        public string msisdn { get; set; }
        public string AppId { get; set; }
        public string AppKey { get; set; }
       
    }
   
}
