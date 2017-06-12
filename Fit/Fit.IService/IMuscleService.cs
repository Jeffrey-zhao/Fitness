using Fit.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fit.IService
{
  public interface IMuscleService
  {
    MuscleDTO[] GetPagedData(int startIndex, int pageSize);

    long GetTotalCount();

    MuscleDTO[] GetAll();

    MuscleDTO[] GetByMuscleGroupID(long id);
  }
}
