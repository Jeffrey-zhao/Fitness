using Fit.Service.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fit.Service.ModelConfigs
{
  public class MotionConfig : EntityTypeConfiguration<MotionEntity>
  {
    public MotionConfig()
    {
      this.ToTable("TFit_Motions");
      this.Property(p => p.Name).IsRequired().HasMaxLength(50);
      this.Property(p => p.Description).IsOptional().HasMaxLength(512);
      this.Property(p => p.Detail).IsOptional().HasMaxLength(1024);
      this.Property(p => p.Attention).IsOptional().HasMaxLength(1024);
      this.Property(p => p.MainPoint).IsOptional().HasMaxLength(1024);
      this.HasOptional(p => p.Muscle).WithMany(p => p.Motions)
        .HasForeignKey(p => p.MuscleID).WillCascadeOnDelete(false);
    }
  }
}
