using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Fit.FrontWeb.Models
{
  public class ChangePwdModel
  {
    [Required]
    [StringLength(30, MinimumLength = 6)]
    public string Password { get; set; }
    [Required]
    [Compare("Password", ErrorMessage = "两次输入的密码不一致")]
    public string PasswordConfirm { get; set; }
  }
}