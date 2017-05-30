using Fit.Service.Entities.RBAC;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fit.Service.ModelConfigs.RBAC
{
  public class AdminUserConfig : EntityTypeConfiguration<AdminUserEntity>
  {
    public AdminUserConfig()
    {
      this.ToTable("TFit_AdminUsers");
      this.Property(p => p.Name).IsRequired().HasMaxLength(50);
      this.Property(p => p.PhoneNum).IsRequired().HasMaxLength(20).IsUnicode(false);
      this.Property(p => p.PasswordSalt).IsRequired().HasMaxLength(20).IsUnicode(false);
      this.Property(p => p.PasswordHash).IsRequired().HasMaxLength(100).IsUnicode(false);
      this.Property(p => p.Email).IsRequired().HasMaxLength(30).IsUnicode(false);
      this.Property(p => p.LoginErrorTimes).IsRequired();
      this.Property(p => p.LastLoginErrorDateTime).IsOptional();

      this.HasMany(p => p.Roles).WithMany(p => p.AdminUsers)
        .Map(m => m.ToTable("TFit_AdminUserRole")
          .MapLeftKey("AdminUserId")
          .MapRightKey("RoleId"));
    }
  }
}
