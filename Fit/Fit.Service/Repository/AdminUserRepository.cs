using Fit.Service.Entities.RBAC;
using Fit.Service.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fit.Service.Repository
{
  public class AdminUserRepository : IRepository<AdminUserEntity>
  {
    private FitDbContext ctx;

    public AdminUserRepository()
    {
      ctx = new FitDbContext();
    }

    public long Add(AdminUserEntity entity)
    {
      ctx.AdminUsers.Add(entity);
      ctx.SaveChanges();
      return entity.ID;
    }

    public void DeleteById(long id)
    {
      var cs = new CommonService<AdminUserEntity>(ctx);
      cs.MarkDeleted(id);
    }

    public AdminUserEntity GetById(long id)
    {
      var cs = new CommonService<AdminUserEntity>(ctx);
      return cs.GetById(id);
    }

    public IQueryable<AdminUserEntity> GetAll()
    {
      var cs = new CommonService<AdminUserEntity>(ctx);
      return cs.GetAll();
    }

    public void Update(AdminUserEntity entity)
    {
      var updating = GetById(entity.ID);

      updating.Name = entity.Name;
      updating.Email = entity.Name;
      updating.PasswordSalt = entity.PasswordSalt;
      updating.PasswordHash = entity.PasswordHash;
      updating.LoginErrorTimes = entity.LoginErrorTimes;
      updating.LastLoginErrorDateTime = entity.LastLoginErrorDateTime;

      ctx.SaveChanges();
    }
  }
}
