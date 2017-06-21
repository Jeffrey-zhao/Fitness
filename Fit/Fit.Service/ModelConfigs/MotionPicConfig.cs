using Fit.Service.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fit.Service.ModelConfigs
{
  public class MotionPicConfig : EntityTypeConfiguration<MotionPicEntity>
  {
    public MotionPicConfig()
    {
      this.ToTable("TFit_MotionPics");
      this.Property(a => a.Description).IsOptional().HasMaxLength(512);
      this.Property(a => a.Url).IsOptional().HasMaxLength(512);
      this.HasOptional(a => a.Motion).WithMany(a => a.MotionPics)
        .HasForeignKey(a => a.MotionID).WillCascadeOnDelete(false);
    }
  }
}
