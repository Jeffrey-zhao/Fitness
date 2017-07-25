using Fit.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fit.IService
{
  public interface IBodyCircumferenceService : IServiceSupport
  {
    long AddOrUpdate(BodyCircumferenceDTO dto);

    BodyCircumferenceDTO[] GetByUserID(long userID);

    BodyCircumferenceDTO GetByID(long id);

    BodyCircumferenceDTO GetCurrentByUserID(long userID);

    LineChartDTO[] GetWeightHistory(long userID);

    LineChartDTO[] GetUpperArmHistory(long userID);

    LineChartDTO[] GetLowerArmHistory(long userID);

    LineChartDTO[] GetChestHistory(long userID);

    LineChartDTO[] GetWaistHistory(long userID);

    LineChartDTO[] GetHipHistory(long userID);

    LineChartDTO[] GetUpperLegHistory(long userID);

    LineChartDTO[] GetLowerLegHistory(long userID);
  }
}
