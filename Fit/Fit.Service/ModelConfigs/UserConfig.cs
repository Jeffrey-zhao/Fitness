using Fit.Service.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fit.Service.ModelConfigs
{
  public class UserConfig : EntityTypeConfiguration<UserEntity>
  {
    public UserConfig()
    {
      this.ToTable("TFit_Users");
      this.Property(p => p.Name).IsRequired().HasMaxLength(50);
      this.Property(p => p.PhoneNum).IsOptional().HasMaxLength(20).IsUnicode(false);
      this.Property(p => p.PasswordSalt).IsRequired().HasMaxLength(20).IsUnicode(false);
      this.Property(p => p.PasswordHash).IsRequired().HasMaxLength(100).IsUnicode(false);
      this.Property(p => p.Email).IsRequired().HasMaxLength(30).IsUnicode(false);
      this.Property(p => p.LoginErrorTimes).IsOptional();
      this.Property(p => p.LastLoginErrorDateTime).IsOptional();
    }
  }
}
