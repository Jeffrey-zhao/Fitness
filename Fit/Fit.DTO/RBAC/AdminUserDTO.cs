using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fit.DTO.RBAC
{
  public class AdminUserDTO
  {
    public string Name { get; set; }
    public string PhoneNum { get; set; }
    public string Email { get; set; }
    public int LoginErrorTimes { get; set; }
    public DateTime? LastLoginErrorDateTime { get; set; }
  }
}
