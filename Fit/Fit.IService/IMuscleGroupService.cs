using Fit.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fit.IService
{
  public interface IMuscleGroupService
  {
    MuscleGroupDTO[] GetPagedData(int startIndex, int pageSize);

    long GetTotalCount();

    MuscleGroupDTO[] GetAll();
  }
}
