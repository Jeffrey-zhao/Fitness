using Fit.Service.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fit.Service.ModelConfigs
{
  public class SecheduleDetailConfig : EntityTypeConfiguration<SecheduleDetailEntity>
  {
    public SecheduleDetailConfig()
    {
      this.ToTable("TFit_SecheduleDetail");
    }
  }
}
