using Fit.Service.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fit.Service.Repository
{
  public class SecheduleDetailRepository : IRepository<SecheduleDetailEntity>
  {
    public FitDbContext Ctx { get; }

    public SecheduleDetailRepository()
    {
      Ctx = new FitDbContext();
    }

    public long Add(SecheduleDetailEntity entity)
    {
      Ctx.SecheduleDetails.Add(entity);
      Ctx.SaveChanges();
      return entity.ID;
    }

    public void DeleteById(long id)
    {
      var entity = GetAll().First(a => a.ID == id);
      entity.IsDeleted = true;
      Ctx.SaveChanges();
    }

    public IQueryable<SecheduleDetailEntity> GetAll()
    {
      return Ctx.SecheduleDetails.Where(a => a.IsDeleted == false);
    }

    public SecheduleDetailEntity GetById(long id)
    {
      return GetAll().FirstOrDefault(a => a.ID == id);
    }

    public void Update(SecheduleDetailEntity entity)
    {
      throw new NotImplementedException();
    }
  }
}
