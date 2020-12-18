using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using LoanOFFER.Data.BusinessLogic;
using LoanOFFER.Web.Models.AuditTrail;
using LoanOFFER.Web.Models.RoleAuth;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace LoanOFFER.Web.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
           : base("SqlConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }

    public class LoanReportDbContext : IdentityDbContext<ApplicationUser>
    {
        public LoanReportDbContext()
          : base("SqlConnection", throwIfV1Schema: false)
        {
        }

        public DbSet<Login> Logins { get; set; }
       
        //public DbSet<SimbrellaLoan> SimbrellaLoans { get; set; }

        public DbSet<SimbrellaLoanTracker> SimbrellaLoanTrackers { get; set; }
        
        public DbSet<RemittaLoan> RemittaLoans { get; set; }
        public DbSet<SalaryTopUpLoan> SalaryTopUpLoans { get; set; }
        public DbSet<ErrorLoan> Errors { get; set; }
        public DbSet<Export> Exports { get; set; }
        public static LoanReportDbContext Create()
        {
            return new LoanReportDbContext();
        }

        //public System.Data.Entity.DbSet<LoanOFFER.Data.BusinessLogic.SimbrellaViewModel> SimbrellaViewModels { get; set; }
    }
    
}