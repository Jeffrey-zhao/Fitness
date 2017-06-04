using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Fit.AdminWeb.Models
{
  public class AdminUserEditModel
  {
    public long ID { get; set; }

    public string Name { get; set; }
    public string PhoneNum { get; set; }
    [Required]
    [EmailAddress]
    [StringLength(30,MinimumLength =6)]
    public string Email { get; set; }
    [Required]
    [StringLength(30, MinimumLength = 6)]
    public string Password { get; set; }
    [Required]
    [Compare("Password",ErrorMessage = "两次输入的密码不一致")]
    public string PasswordComfirm { get; set; }
  }
}