using Fit.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fit.IService
{
  public interface IMotionsInPlanService : IServiceSupport
  {
    MotionsInPlanOutputDTO[] GetByPlanID(long planID);

    void Add(MotionsInPlanInputDTO dto);

    void Delete(long id);
  }
}
