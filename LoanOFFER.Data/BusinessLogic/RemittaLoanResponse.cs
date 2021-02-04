using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoanOFFER.Data.BusinessLogic
{
    public class RemittaLoanResponse
    {
        // Response
        public string eligible2MonthsDBRAmount { get; set; }
        public string eligible2MonthsDBRAmountTenor { get; set; }
        public string eligibleAmount { get; set; }
        public string isDeliquent { get; set; }
        public string responseDescription { get; set; }
        public string responseCode { get; set; }
        public string interestRate { get; set; }
        public string eligible1MonthDBRAmount { get; set; }
        public string eligible1MonthDBRAmountTenor { get; set; }
        public string eligible3MonthsDBRAmount { get; set; }
        public string eligible1MonthlyRepayment { get; set; }
        public string eligible2MonthlyRepayment { get; set; }
        public string eligible3MonthlyRepayment { get; set; }
        public string eligible3MonthsDBRAmountTenor { get; set; }
        public string DueDate { get; set; }
        public string ManagementFee { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string dob { get; set; }
        public string Title { get; set; }
        public string AcoountNo { get; set; }
        public string BankCode { get; set; }
        public string companyName { get; set; }
    }
}
