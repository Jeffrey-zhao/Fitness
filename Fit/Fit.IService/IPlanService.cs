using Fit.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fit.IService
{
  public interface IPlanService : IServiceSupport
  {
    void ReAddPlan(long userID, int cycleDays);

    PlanDTO[] GetUserPlans(long userID);

    long GetPlanCount(long userId);

    SecheduleDTO[] GetSechedule(long userId,string startDateStr, string endDateStr);

    PieChartDTO[] CompareCombineAndPartial(long userID);

    PieChartDTO[] CompareMuscleGroups(long userID);
  }
}
