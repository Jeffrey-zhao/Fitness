using Fit.Service.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fit.Service.Repository
{
  public class AdminLogRepository : IRepository<AdminLogEntity>
  {
    public FitDbContext Ctx { get; }
    public AdminLogRepository()
    {
      Ctx = new FitDbContext();
    }

    public long Add(AdminLogEntity entity)
    {
      Ctx.AdminLogs.Add(entity);
      Ctx.SaveChanges();
      return entity.ID;
    }

    public void DeleteById(long id)
    {
      throw new NotImplementedException();
    }

    public IQueryable<AdminLogEntity> GetAll()
    {
      return Ctx.AdminLogs.Where(a => a.IsDeleted == false);
    }

    public AdminLogEntity GetById(long id)
    {
      throw new NotImplementedException();
    }

    public void Update(AdminLogEntity entity)
    {
      throw new NotImplementedException();
    }
  }
}
