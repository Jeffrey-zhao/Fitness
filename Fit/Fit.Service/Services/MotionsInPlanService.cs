using Fit.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using Fit.DTO;
using Fit.Service.Repository;
using Fit.Service.Entities;
using Fit.Common;

namespace Fit.Service.Services
{
  public class MotionsInPlanService : IMotionsInPlanService
  {
    IRepository<MotionsInPlanEntity> mipRep;

    public MotionsInPlanService(IRepository<MotionsInPlanEntity> mipRep)
    {
      this.mipRep = mipRep;
    }

    public MotionsInPlanDTO[] GetByPlanID(long planID)
    {
      var entities = mipRep.GetAll().Where(a => a.PlanID == planID).Include(a => a.Motion).AsNoTracking().ToList();
      return entities.Select(a => ToDTO(a)).ToArray();
    }

    private MotionsInPlanDTO ToDTO(MotionsInPlanEntity entity)
    {
      if (entity == null) throw new ArgumentException(ExceptionMsg.GetObjNullMsg("MotionsInPlanEntity"));
      var dto = new MotionsInPlanDTO
      {
        ID = entity.ID,
        MotionName = entity.Motion.Name
      };
      if (entity.Groups <= 0)  //综合训练
      {
        dto.ExcerciseDetail = string.Format(Consts.PLAN_TEMPLATE_DETAIL2, entity.Number);
      }
      else  //局部训练
      {
        if (entity.Number <= 0)  //无负重
        {
          dto.ExcerciseDetail = string.Format(Consts.PLAN_TEMPLATE_DETAIL1
            , entity.Groups, entity.Times, string.Empty);
        }
        else  //负重
        {
          dto.ExcerciseDetail = string.Format(Consts.PLAN_TEMPLATE_DETAIL1
            , entity.Groups, entity.Times
            , string.Format(Consts.PLAN_TEMPLATE_DETAIL1_1, entity.Number));
        }
      }
      return dto;
    }
  }
}
