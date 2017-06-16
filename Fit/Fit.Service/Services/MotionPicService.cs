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
  public class MotionPicService : IMotionPicService
  {
    IRepository<MotionPicEntity> motionPicRep;
    public MotionPicService(IRepository<MotionPicEntity> rep)
    {
      this.motionPicRep = rep;
    }

    public long Add(MotionPicDTO dto)
    {
      var entity = new MotionPicEntity
      {
        Description = dto.Description,
        MotionID = dto.MotionID,
        PicType = (int)dto.PicType,
        Url = dto.Url
      };

      return motionPicRep.Add(entity);
    }

    public void Delete(long id)
    {
      motionPicRep.DeleteById(id);
    }
  }
}
