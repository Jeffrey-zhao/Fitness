using Fit.Service.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fit.Service.Repository
{
  public class PlanRepository : IRepository<PlanEntity>
  {
    public FitDbContext Ctx { get; }
    public PlanRepository()
    {
      Ctx = new FitDbContext();
    }

    public long Add(PlanEntity entity)
    {
      Ctx.Plans.Add(entity);
      Ctx.SaveChanges();
      return entity.ID;
    }

    public void DeleteById(long id)
    {
      throw new NotImplementedException();
    }

    public IQueryable<PlanEntity> GetAll()
    {
      return Ctx.Plans.Where(a => a.IsDeleted == false).OrderBy(a => a.CreatedDateTime);
    }

    public PlanEntity GetById(long id)
    {
      throw new NotImplementedException();
    }

    public void Update(PlanEntity entity)
    {
      throw new NotImplementedException();
    }
  }
}
