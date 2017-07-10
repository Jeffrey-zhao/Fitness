using Fit.Service.Entities;
using Fit.Service.Entities.RBAC;
using log4net;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Fit.Service
{
  public class FitDbContext : DbContext
  {
    private ILog log = LogManager.GetLogger(typeof(FitDbContext));
    public FitDbContext() : base("name=connStr")
    {
      //Database.SetInitializer<FitDbContext>(null);

      this.Database.Log = (sql) => { log.DebugFormat("EF-SQL: {0}", sql); };
    }

    //This function is import which maps the entity with it's modelconfig
    protected override void OnModelCreating(DbModelBuilder modelBuilder)
    {
      base.OnModelCreating(modelBuilder);
      modelBuilder.Configurations.AddFromAssembly(Assembly.GetExecutingAssembly());
    }

    public DbSet<PermissionEntity> Permissions { get; set; }
    public DbSet<RoleEntity> Roles { get; set; }
    public DbSet<AdminUserEntity> AdminUsers { get; set; }

    public DbSet<MuscleGroupEntity> MuscleGroups { get; set; }
    public DbSet<MuscleEntity> Muscles { get; set; }
    public DbSet<MotionEntity> Motions { get; set; }
    public DbSet<MotionPicEntity> MotionPics { get; set; }
    public DbSet<AdminLogEntity> AdminLogs { get; set; }
    public DbSet<UserEntity> Users { get; set; }
    public DbSet<KeyValueEntity> KeyValues { get; set; }
    public DbSet<PlanEntity> Plans { get; set; }
    public DbSet<MotionsInPlanEntity> MotionsInPlans { get; set; }
    public DbSet<SecheduleEntity> Sechedules { get; set; }
    public DbSet<SecheduleDetailEntity> SecheduleDetails { get; set; }
    public DbSet<BodyCircumferenceEntity> BodyCircumferences { get; set; }
  }
}
