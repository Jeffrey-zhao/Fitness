using Fit.Service.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fit.Service.ModelConfigs
{
  public class AdminLogConfig : EntityTypeConfiguration<AdminLogEntity>
  {
    public AdminLogConfig()
    {
      this.ToTable("TFit_AdminLogs");
      this.Property(a => a.Name).IsRequired().HasMaxLength(64);
      this.HasRequired(a => a.AdminUser).WithMany(b => b.AdminLogs)
        .HasForeignKey(a => a.AdminUserID).WillCascadeOnDelete(false);
    }
  }
}
