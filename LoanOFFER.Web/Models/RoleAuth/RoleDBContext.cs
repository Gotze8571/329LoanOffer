using LoanOFFER.Data.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace LoanOFFER.Web.Models.RoleAuth
{
    public class RoleDBContext : DbContext
    {
        public RoleDBContext() : base("name=LoanRoleDB") { }

        public DbSet<Role> Roles { get; set; }
        public DbSet<UserIdentity> UserIdentities { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<ErrorLoan> ErrorLoans { get; set; }
    }
}