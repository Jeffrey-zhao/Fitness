using Fit.Service.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fit.Service.Repository
{
  public class MotionPicRepository : IRepository<MotionPicEntity>
  {
    public FitDbContext Ctx { get; }

    public MotionPicRepository()
    {
      Ctx = new FitDbContext();
    }

    public long Add(MotionPicEntity entity)
    {
      Ctx.MotionPics.Add(entity);
      Ctx.SaveChanges();
      return entity.ID;
    }

    public void DeleteById(long id)
    {
      var entity = GetById(id);
      entity.IsDeleted = false;
      Ctx.SaveChanges();
    }

    public IQueryable<MotionPicEntity> GetAll()
    {
      return Ctx.MotionPics.Where(a => a.IsDeleted == false);
    }

    public MotionPicEntity GetById(long id)
    {
      return GetAll().Where(a => a.ID == id).FirstOrDefault();
    }

    public void Update(MotionPicEntity entity)
    {
      throw new NotImplementedException();
    }
  }
}
