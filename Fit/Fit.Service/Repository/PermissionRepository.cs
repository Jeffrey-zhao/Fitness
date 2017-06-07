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
    private FitDbContext ctx;

    public PermissionRepository()
    {
      ctx = new FitDbContext();
    }

    public long Add(PermissionEntity entity)
    {
      ctx.Permissions.Add(entity);
      ctx.SaveChanges();
      return entity.ID;
    }

    public void DeleteById(long id)
    {
      var entity = GetById(id);
      entity.IsDeleted = true;
      ctx.SaveChanges();
    }

    public IQueryable<PermissionEntity> GetAll()
    {
      var c = ctx.Permissions.Where(a => a.IsDeleted == false);
      return c;
    }

    public PermissionEntity GetById(long id)
    {
      return GetAll().Where(a => a.ID == id).FirstOrDefault();
    }

    public void Update(PermissionEntity entity)
    {
      var updating = GetById(entity.ID);
      if (updating == null) throw new ArgumentException(ExceptionMsg.GetObjectNullMsg("PermissionEntity"));
      updating.Name = entity.Name;
      updating.Description = entity.Description;
      ctx.SaveChanges();
    }
  }
}
