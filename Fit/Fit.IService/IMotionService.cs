using Fit.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fit.IService
{
  public interface IMotionService : IServiceSupport
  {
    MotionDTO[] GetPagedData(int startIndex, int pageSize);

    long GetTotalCount();

    MotionDTO[] GetByMuscleID(long id);

    long Add(MotionDTO dto);

    void Update(MotionDTO dto);

    void Delete(long id);

    MotionDTO GetByID(long id);
  }
}
