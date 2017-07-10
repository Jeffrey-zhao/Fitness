using Fit.Service.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fit.Service.Repository
{
  public class BodyCircumferenceRepository : IRepository<BodyCircumferenceEntity>
  {
    public FitDbContext Ctx { get; }
    public BodyCircumferenceRepository()
    {
      Ctx = new FitDbContext();
    }

    public long Add(BodyCircumferenceEntity entity)
    {
      Ctx.BodyCircumferences.Add(entity);
      Ctx.SaveChanges();
      return entity.ID;
    }

    public void DeleteById(long id)
    {
      throw new NotImplementedException();
    }

    public IQueryable<BodyCircumferenceEntity> GetAll()
    {
      return Ctx.BodyCircumferences.Where(a => a.IsDeleted == false);
    }

    public BodyCircumferenceEntity GetById(long id)
    {
      return GetAll().FirstOrDefault(a => a.ID == id);
    }

    public void Update(BodyCircumferenceEntity entity)
    {
      throw new NotImplementedException();
    }
  }
}
