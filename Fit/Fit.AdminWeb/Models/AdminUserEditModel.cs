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
    [Required]
    [StringLength(50)]
    public string Name { get; set; }
    [Required]
    [StringLength(20)]
    public string PhoneNum { get; set; }
    [Required]
    [EmailAddress]
    [StringLength(30,MinimumLength =6)]
    public string Email { get; set; }
    [StringLength(30, MinimumLength = 6)]
    public string Password { get; set; }
    [Compare("Password",ErrorMessage = "两次输入的密码不一致")]
    public string PasswordComfirm { get; set; }
  }
}