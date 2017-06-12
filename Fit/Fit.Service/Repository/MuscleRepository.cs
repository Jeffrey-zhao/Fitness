using Fit.Service.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fit.Service.Repository
{
  public class MuscleRepository : IRepository<MuscleEntity>
  {
    public FitDbContext Ctx => new FitDbContext();

    public long Add(MuscleEntity entity)
    {
      throw new NotImplementedException();
    }

    public void DeleteById(long id)
    {
      throw new NotImplementedException();
    }

    public IQueryable<MuscleEntity> GetAll()
    {
      return Ctx.Muscles.Where(a => a.IsDeleted == false);
    }

    public MuscleEntity GetById(long id)
    {
      throw new NotImplementedException();
    }

    public void Update(MuscleEntity entity)
    {
      throw new NotImplementedException();
    }
  }
}
