using Fit.Common;
using Fit.Service.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fit.Service.Repository
{
  public class MotionsInPlanRepository : IRepository<MotionsInPlanEntity>
  {
    public FitDbContext Ctx { get; }
    public MotionsInPlanRepository()
    {
      Ctx = new FitDbContext();
    }

    public long Add(MotionsInPlanEntity entity)
    {
      Ctx.MotionsInPlans.Add(entity);
      Ctx.SaveChanges();
      return entity.ID;
    }

    public void DeleteById(long id)
    {
      var entity = GetById(id);
      if (entity == null) throw new Exception(ExceptionMsg.GetObjNullMsg("MotionsInPlanEntity"));
      entity.IsDeleted = true;
      Ctx.SaveChanges();
    }

    public IQueryable<MotionsInPlanEntity> GetAll()
    {
      return Ctx.MotionsInPlans.Where(a => a.IsDeleted == false);
    }

    public MotionsInPlanEntity GetById(long id)
    {
      return GetAll().FirstOrDefault(a => a.ID == id);
    }

    public void Update(MotionsInPlanEntity entity)
    {
      throw new NotImplementedException();
    }
  }
}
