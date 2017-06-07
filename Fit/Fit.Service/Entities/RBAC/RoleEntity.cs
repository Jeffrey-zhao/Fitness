using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fit.Service.Entities.RBAC
{
  public class RoleEntity : BaseEntity
  {
    public string Name { get; set; }
    public string Description { get; set; }
    public virtual ICollection<AdminUserEntity> AdminUsers { get; set; }
      = new List<AdminUserEntity>();

    public virtual ICollection<PermissionEntity> Permissions { get; set; }
      = new List<PermissionEntity>();


    public override bool Equals(object obj)
    {
      if (ReferenceEquals(null, obj)) return false;
      if (ReferenceEquals(this, obj)) return true;
      if (obj.GetType() != this.GetType()) return false;
      return AreEqual((RoleEntity)obj);
    }

    public override int GetHashCode()
    {
      return base.GetHashCode();
    }

    private bool AreEqual(RoleEntity other)
    {
      bool isEqual = true;
      isEqual &= ID == other.ID;
      isEqual &= Name == other.Name;
      isEqual &= Description == other.Description;
      return isEqual;
    }
  }
}
