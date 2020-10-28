using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LoanOFFER.Web.Models.RoleAuth
{
    public class Role
    {
        [Key]
        public int RoleId { get; set; }
        public string RoleName { get; set; }
    }

    public class UserIdentity
    {
        [Key]
        public int UserId { get; set; }
        public string StaffId { get; set; }
        public string UserName { get; set; }
    }

    public class UserRole
    {
        [Key]
        public int Id { get; set; }
        public int RoleId { get; set; }
        public int UserId { get; set; }

        public virtual Role Role { get; set; }
        public virtual UserIdentity User { get; set; }
    }
}