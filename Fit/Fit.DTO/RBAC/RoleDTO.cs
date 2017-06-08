using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fit.DTO.RBAC
{
  public class RoleDTO
  {
    public long Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public long[] PermissionIDs { get; set; }
  }
}
