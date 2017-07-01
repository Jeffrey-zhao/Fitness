using Fit.Service.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fit.Service.ModelConfigs
{
  public class PlanConfig:EntityTypeConfiguration<PlanEntity>
  {
    public PlanConfig()
    {
      this.ToTable("TFit_Plans");
      this.HasRequired(a => a.User).WithMany(b => b.Plans).HasForeignKey(a => a.UserID).WillCascadeOnDelete(false);
    }
  }
}
