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
  }
}
