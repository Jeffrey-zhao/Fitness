using Fit.Service.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fit.Service.ModelConfigs
{
  public class MuscleConfig : EntityTypeConfiguration<MuscleEntity>
  {
    public MuscleConfig()
    {
      this.ToTable("TFit_Muscles");
      this.Property(p => p.Name).IsRequired().HasMaxLength(50);
      this.Property(p => p.Description).IsOptional().HasMaxLength(512);
      this.HasRequired(p => p.MuscleGroup).WithMany(p => p.Muscles)
        .HasForeignKey(p => p.MuscleGroupID).WillCascadeOnDelete(false);
    }
  }
}
