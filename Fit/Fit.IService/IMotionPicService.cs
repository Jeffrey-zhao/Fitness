using Fit.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fit.IService
{
  public interface IMotionPicService : IServiceSupport
  {
    long Add(MotionPicDTO dto);

    void Delete(long id);
  }
}
