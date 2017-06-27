using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fit.Service.Entities
{
  public class UserEntity : BaseEntity
  {
    public string Name { get; set; }
    public string PhoneNum { get; set; }
    public string Email { get; set; }
    public string PasswordSalt { get; set; }
    public string PasswordHash { get; set; }
    public int LoginErrorTimes { get; set; } = 0;
    public DateTime? LastLoginErrorDateTime { get; set; }
    public string OperateCode { get; set; }
    public bool IsActivated { get; set; } = false;
  }
}
