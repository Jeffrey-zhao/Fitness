using Fit.Service.Entities.RBAC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fit.Service.Entities
{
  public class AdminLogEntity : BaseEntity
  {
    public string Name { get; set; }
    public long AdminUserID { get; set; }
    public virtual AdminUserEntity AdminUser { get; set; }
  }
}
