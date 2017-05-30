using Fit.Service.Entities.RBAC;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fit.Service.ModelConfigs.RBAC
{
  public class PermissionConfig:EntityTypeConfiguration<PermissionEntity>
  {
    public PermissionConfig()
    {
      this.ToTable("TFit_Permissions");
      this.Property(p => p.Name).IsRequired().HasMaxLength(50);
      this.Property(p => p.Description).IsOptional().HasMaxLength(512);
    }
  }
}
