using Fit.Service.Entities.RBAC;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fit.Service.ModelConfigs.RBAC
{
  public class RoleConfig : EntityTypeConfiguration<RoleEntity>
  {
    public RoleConfig()
    {
      this.ToTable("TFit_Roles");
      this.Property(p => p.Name).IsRequired().HasMaxLength(50);
      this.HasMany(p => p.Permissions).WithMany(p => p.Roles)
        .Map(m => m.ToTable("TFit_RolePermissions")
          .MapLeftKey("RoleId")
          .MapRightKey("PermissionId"));
    }
  }
}
