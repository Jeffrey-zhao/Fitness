using Fit.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fit.DTO;
using Fit.Service.Repository;
using Fit.Service.Entities;

namespace Fit.Service.Services
{
  public class MotionService : IMotionService
  {
    IRepository<MotionEntity> motionRep;
    IRepository<MotionPicEntity> motionPicRep;
    public MotionService(IRepository<MotionEntity> motionRep, IRepository<MotionPicEntity> motionPicRep)
    {
      this.motionRep = motionRep;
      this.motionPicRep = motionPicRep;
    }

    public long Add(MotionDTO dto)
    {
      throw new NotImplementedException();
    }

    public void Delete(long id)
    {
      throw new NotImplementedException();
    }

    public void Edit(MotionDTO dto)
    {
      throw new NotImplementedException();
    }

    public MotionDTO[] GetAll()
    {
      throw new NotImplementedException();
    }

    public MotionDTO[] GetByMuscleID(long id)
    {
      throw new NotImplementedException();
    }

    public MotionDTO[] GetPagedData(int startIndex, int pageSize)
    {
      throw new NotImplementedException();
    }

    public long GetTotalCount()
    {
      throw new NotImplementedException();
    }
  }
}
