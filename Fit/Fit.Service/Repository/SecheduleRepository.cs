using Fit.Service.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fit.Service.Repository
{
  public class SecheduleRepository : IRepository<SecheduleEntity>
  {
    public FitDbContext Ctx { get; }
    public SecheduleRepository()
    {
      Ctx = new FitDbContext();
    }

    public long Add(SecheduleEntity entity)
    {
      Ctx.Sechedules.Add(entity);
      Ctx.SaveChanges();
      return entity.ID;
    }

    public void DeleteById(long id)
    {
      var entity = GetAll().FirstOrDefault(a => a.ID == id);
      entity.IsDeleted = true;
      Ctx.SaveChanges();
    }

    public IQueryable<SecheduleEntity> GetAll()
    {
      return Ctx.Sechedules.Where(a => a.IsDeleted == false);
    }

    public SecheduleEntity GetById(long id)
    {
      return GetAll().FirstOrDefault(a => a.ID == id);
    }

    public void Update(SecheduleEntity entity)
    {
      throw new NotImplementedException();
    }
  }
}
