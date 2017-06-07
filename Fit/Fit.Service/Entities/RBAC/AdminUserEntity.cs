using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fit.Service.Entities.RBAC
{
  public class AdminUserEntity : BaseEntity
  {
    public string Name { get; set; }
    public string PhoneNum { get; set; }
    public string Email { get; set; }
    public string PasswordSalt { get; set; }
    public string PasswordHash { get; set; }
    public int LoginErrorTimes { get; set; } = 0;
    public DateTime? LastLoginErrorDateTime { get; set; }

    public virtual ICollection<RoleEntity> Roles { get; set; }
      = new List<RoleEntity>();

    protected bool AreEqual(AdminUserEntity other)
    {
      bool isEqual = true;
      isEqual &= ID == other.ID;
      isEqual &= Name.Equals(other.Name);
      isEqual &= Email.Equals(other.Email);
      return isEqual;
    }

    public override bool Equals(object obj)
    {
      if (ReferenceEquals(null, obj)) return false;
      if (ReferenceEquals(this, obj)) return true;
      if (obj.GetType() != this.GetType()) return false;
      return AreEqual((AdminUserEntity)obj);
    }

    public override int GetHashCode()
    {
      return base.GetHashCode();
    }
  }
}
