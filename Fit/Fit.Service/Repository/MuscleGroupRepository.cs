using Fit.Service.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fit.Service.Repository
{
  public class MuscleGroupRepository : IRepository<MuscleGroupEntity>
  {
    public FitDbContext Ctx => new FitDbContext();

    public long Add(MuscleGroupEntity entity)
    {
      throw new NotImplementedException();
    }

    public void DeleteById(long id)
    {
      throw new NotImplementedException();
    }

    public IQueryable<MuscleGroupEntity> GetAll()
    {
      return Ctx.MuscleGroups.Where(a => a.IsDeleted == false);
    }

    public MuscleGroupEntity GetById(long id)
    {
      throw new NotImplementedException();
    }

    public void Update(MuscleGroupEntity entity)
    {
      throw new NotImplementedException();
    }
  }
}
