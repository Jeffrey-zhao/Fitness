using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Fit.AdminWeb.Models
{
  public class LoginModel
  {
    [Required]
    [EmailAddress]
    [StringLength(30, MinimumLength = 6)]
    public string Email { get; set; }
    [Required]
    [StringLength(30, MinimumLength = 6)]
    public string Password { get; set; }
    [Required]
    [StringLength(4)]
    public string VerifyCode { get; set; }
  }
}