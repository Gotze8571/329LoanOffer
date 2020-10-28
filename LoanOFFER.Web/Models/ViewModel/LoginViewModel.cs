using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LoanOFFER.Web.Models.ViewModel
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "The User Name field is required")]
        [DisplayName("User Name")]
        public string UserId { get; set; }
        [Required]
        public string Password { get; set; }
    }
}