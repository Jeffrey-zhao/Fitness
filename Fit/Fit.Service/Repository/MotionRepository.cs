using Fit.Service.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fit.Service.Repository
{
  public class MotionRepository : IRepository<MotionEntity>
  {
    public FitDbContext Ctx { get; }// => new FitDbContext();
    public MotionRepository()
    {
      Ctx = new FitDbContext();
    }

    public long Add(MotionEntity entity)
    {
      Ctx.Motions.Add(entity);
      Ctx.SaveChanges();
      return entity.ID;
    }

    public void DeleteById(long id)
    {
      var entity = GetById(id);
      entity.IsDeleted = true;
      Ctx.SaveChanges();
    }

    public IQueryable<MotionEntity> GetAll()
    {
      return Ctx.Motions.Where(a => a.IsDeleted == false);
    }

    public MotionEntity GetById(long id)
    {
      return GetAll().Where(a => a.ID == id).FirstOrDefault();
    }

    public void Update(MotionEntity entity)
    {
      Ctx.SaveChanges();
    }
  }
}
