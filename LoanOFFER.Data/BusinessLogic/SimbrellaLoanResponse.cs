using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoanOFFER.Data.BusinessLogic
{
    public class SimbrellaLoanResponse
    {
        public int customerId { get; set; }
        public string msisdn { get; set; }
        public string eligiblieOffers { get; set; }
    }
}
