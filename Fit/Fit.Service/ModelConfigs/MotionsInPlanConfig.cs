using Fit.Service.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fit.Service.ModelConfigs
{
  public class MotionsInPlanConfig:EntityTypeConfiguration<MotionsInPlanEntity>
  {
    public MotionsInPlanConfig()
    {
      this.ToTable("TFit_MotionsInPlans");
      this.HasRequired(a => a.Plan).WithMany(b => b.MotionsInPlans).HasForeignKey(a => a.PlanID).WillCascadeOnDelete(false);
      this.HasRequired(a => a.Motion).WithMany(b=>b.MotionsInPlan).WillCascadeOnDelete(false);
    }
  }
}
