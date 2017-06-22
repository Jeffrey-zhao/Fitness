using Fit.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fit.DTO;
using Fit.Service.Repository;
using Fit.Service.Entities;
using Fit.Common;
using static Fit.Common.Enums;

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
        PicType = (int)dto.PicType,
        Url = dto.Url
      };
      if (dto.MotionID > 0)
      {
        entity.MotionID = dto.MotionID;
      }

      return motionPicRep.Add(entity);
    }

    public void Delete(long id)
    {
      motionPicRep.DeleteById(id);
    }

    public void DeleteNoReference()
    {
      var entities = motionPicRep.GetAll().Where(a => a.Motion == null).ToList();
      if (entities == null) return;
      foreach (var item in entities)
      {
        Delete(item.ID);
      }
    }

    public MotionPicDTO[] GetByMotionAndType(int picType, long motionID)
    {
      var all = motionPicRep.GetAll().OrderByDescending(a => a.CreatedDateTime);
      MotionPicDTO[] dtos;
      if (motionID <= 0)
      {
        dtos = all.Where(a => a.Motion == null && a.PicType == picType).ToList().Select(a => ToDTO(a)).ToArray();
      }
      else
      {
        dtos = all.Where(a => a.MotionID == motionID && a.PicType == picType).ToList().Select(a => ToDTO(a)).ToArray();
      }
      return dtos;
    }

    public void LinkPicToMotion(long motionID)
    {
      var addedPics = motionPicRep.GetAll().Where(a => a.Motion == null).ToList();
      addedPics.ForEach(a => a.MotionID = motionID);
    }

    private MotionPicDTO ToDTO(MotionPicEntity entity)
    {
      if (entity == null)
      {
        throw new ArgumentException(ExceptionMsg.GetObjNullMsg("MotionPicEntity"));
      }
      var dto = new MotionPicDTO
      {
        ID = entity.ID,
        Url = Consts.CLOUD_DOMAIN + entity.Url,
        PicType = entity.PicType
      };
      if (entity.Motion != null)
      {
        dto.MotionID = entity.MotionID.Value;
      }
      return dto;
    }
  }
}
