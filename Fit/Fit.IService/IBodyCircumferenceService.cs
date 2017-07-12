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

    ChartDataDTO[] GetWeightHistory(long userID);

    ChartDataDTO[] GetUpperArmHistory(long userID);

    ChartDataDTO[] GetLowerArmHistory(long userID);

    ChartDataDTO[] GetChestHistory(long userID);

    ChartDataDTO[] GetWaistHistory(long userID);

    ChartDataDTO[] GetHipHistory(long userID);

    ChartDataDTO[] GetUpperLegHistory(long userID);

    ChartDataDTO[] GetLowerLegHistory(long userID);
  }
}
