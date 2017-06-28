using Fit.Service.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fit.Service.ModelConfigs
{
  public class KeyValueConfig:EntityTypeConfiguration<KeyValueEntity>
  {
    public KeyValueConfig()
    {
      this.ToTable("TFit_KeyValues");
      this.Property(a => a.Key).IsRequired().HasMaxLength(100).IsUnicode(true);
      this.Property(a => a.Value).IsRequired().HasMaxLength(1024).IsUnicode(true);
      this.Property(a => a.Type).IsOptional().HasMaxLength(50);
    }
  }
}
