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
    }
  }
}
