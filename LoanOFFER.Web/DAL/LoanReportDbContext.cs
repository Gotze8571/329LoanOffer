using LoanOFFER.Data.BusinessLogic;
using LoanOFFER.Web.Models.AuditTrail;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace LoanOFFER.Web.DAL
{
    public class LoanReportDbContext : DbContext
    {
        public LoanReportDbContext()
          : base("LoanReport")
        {
        }

        public DbSet<Login> Logins { get; set; }

        //public DbSet<SimbrellaLoan> SimbrellaLoans { get; set; }

        public DbSet<SimbrellaLoanTracker> SimbrellaLoanTrackers { get; set; }

        public DbSet<RemittaLoan> RemittaLoans { get; set; }
        public DbSet<SalaryTopUpLoan> SalaryTopUpLoans { get; set; }
        public DbSet<FashcashLoan> FastcashLoans { get; set; }
        public DbSet<ErrorLoan> Errors { get; set; }
        public DbSet<Export> Exports { get; set; }
        public static LoanReportDbContext Create()
        {
            return new LoanReportDbContext();
        }

        //public System.Data.Entity.DbSet<LoanOFFER.Data.BusinessLogic.SimbrellaViewModel> SimbrellaViewModels { get; set; }
    }
}