using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoanOFFER.Data.BusinessLogic
{
    public class ErrorLoan
    {
        [Key]
        public int Id { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public bool FetchedData { get; set; }
        public string LoginUser { get; set; }
        public string ErrorName { get; set; }
        public DateTime ErrorDate { get; set; }
    }
}
