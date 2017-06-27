using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fit.DTO
{
  public class UserInfoDTO
  {
    public long ID { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public DateTime CreatedDateTime { get; set; }
    public bool IsActivated { get; set; }
  }
}
