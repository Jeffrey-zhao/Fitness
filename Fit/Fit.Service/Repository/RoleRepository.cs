using Fit.Common;
using Fit.Service.Entities.RBAC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fit.Service.Repository
{
  public class RoleRepository : IRepository<RoleEntity>
  {
   public FitDbContext Ctx { get; } = new FitDbContext();

    public long Add(RoleEntity entity)
    {
      Ctx.Roles.Add(entity);
      Ctx.SaveChanges();
      return entity.ID;
    }

    public void DeleteById(long id)
    {
      var entity = GetById(id);
      entity.IsDeleted = true;
      Ctx.SaveChanges();
    }

    public IQueryable<RoleEntity> GetAll()
    {
      return Ctx.Roles.Where(a => a.IsDeleted == false);
    }

    public RoleEntity GetById(long id)
    {
      return GetAll().Where(a => a.ID == id).FirstOrDefault();
    }

    public void Update(RoleEntity entity)
    {
      var updating = GetById(entity.ID);
      if (updating == null) throw new ArgumentException(ExceptionMsg.GetObjNullMsg("RoleEntity"));
      updating.Name = entity.Name;
      updating.Description = entity.Description;
      Ctx.SaveChanges();
    }
  }
}
