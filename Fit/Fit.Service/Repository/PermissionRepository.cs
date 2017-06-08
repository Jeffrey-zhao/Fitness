using Fit.Common;
using Fit.Service.Entities.RBAC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fit.Service.Repository
{
  public class PermissionRepository : IRepository<PermissionEntity>
  {
    public FitDbContext Ctx { get; }

    public PermissionRepository()
    {
      Ctx = new FitDbContext();
    }

    public long Add(PermissionEntity entity)
    {
      Ctx.Permissions.Add(entity);
      Ctx.SaveChanges();
      return entity.ID;
    }

    public void DeleteById(long id)
    {
      var entity = GetById(id);
      entity.IsDeleted = true;
      Ctx.SaveChanges();
    }

    public IQueryable<PermissionEntity> GetAll()
    {
      var c = Ctx.Permissions.Where(a => a.IsDeleted == false);
      return c;
    }

    public PermissionEntity GetById(long id)
    {
      return GetAll().Where(a => a.ID == id).FirstOrDefault();
    }

    public void Update(PermissionEntity entity)
    {
      var updating = GetById(entity.ID);
      if (updating == null) throw new ArgumentException(ExceptionMsg.GetObjNullMsg("PermissionEntity"));
      updating.Name = entity.Name;
      updating.Description = entity.Description;
      Ctx.SaveChanges();
    }
  }
}
