using Fit.Common;
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
    public FitDbContext Ctx { get; }

    public AdminUserRepository()
    {
      Ctx = new FitDbContext();
    }

    public long Add(AdminUserEntity entity)
    {
      Ctx.AdminUsers.Add(entity);
      Ctx.SaveChanges();
      return entity.ID;
    }

    public void DeleteById(long id)
    {
      var entity = GetById(id);
      entity.IsDeleted = true;
      Ctx.SaveChanges();
    }

    public AdminUserEntity GetById(long id)
    {
      return GetAll().Where(a => a.ID == id).FirstOrDefault();
    }

    public IQueryable<AdminUserEntity> GetAll()
    {
      return Ctx.AdminUsers.Where(a => a.IsDeleted == false);
    }

    public void Update(AdminUserEntity entity)
    {
      var updating = GetById(entity.ID);
      if (updating == null) throw new ArgumentException(ExceptionMsg.GetObjNullMsg("AdminUserEntity"));
      updating.Name = entity.Name;
      updating.Email = entity.Email;
      updating.PhoneNum = entity.PhoneNum;
      if (!string.IsNullOrEmpty(entity.PasswordHash))
      {
        updating.PasswordSalt = entity.PasswordSalt;
        updating.PasswordHash = entity.PasswordHash;
      }

      Ctx.SaveChanges();
    }
  }
}
