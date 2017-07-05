using Fit.IService;
using Fit.Service.Entities;
using Fit.Service.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Text;
using System.Threading.Tasks;
using Fit.DTO;
using Fit.Common;

namespace Fit.Service.Services
{
  public class PlanService : IPlanService
  {
    IRepository<PlanEntity> planRep;
    public PlanService(IRepository<PlanEntity> planRep)
    {
      this.planRep = planRep;
    }

    public void ReAddPlan(long userID, int cycleDays)
    {
      if (cycleDays <= 0) return;

      var oldPlans = planRep.GetAll().Where(a => a.UserID == userID).ToList();
      if (oldPlans != null && oldPlans.Count > 0)
      {
        oldPlans.ForEach(a => a.IsDeleted = true);
      }
      for (int i = 0; i < cycleDays; i++)
      {
        planRep.Ctx.Plans.Add(new PlanEntity { UserID = userID });
      }

      planRep.Ctx.SaveChanges();
    }

    public PlanDTO[] GetUserPlans(long userID)
    {
      var entities = planRep.GetAll().Where(a => a.UserID == userID).Include(a => a.MotionsInPlans).ToList();
      var list = new List<PlanDTO>();
      if (entities.Count <= 0) return list.ToArray();
      for (int i = 0; i < entities.Count; i++)
      {
        var dto = new PlanDTO()
        {
          Day = string.Format(Consts.PLAN_TEMPLATE_DAY, i + 1),
        };
        if (entities[i].MotionsInPlans != null && entities[i].MotionsInPlans.Count > 0)
        {
          dto.Detail = GetMotionsNames(entities[i].MotionsInPlans);
        }
        else
        {
          dto.Detail = Consts.PLAN_REST;
        }
        dto.ID = entities[i].ID;
        dto.Remark = SubString(dto.Detail);
        list.Add(dto);
      }

      return list.ToArray();
    }
    
    public long GetPlanCount(long userId)
    {
      return planRep.GetAll().Where(a => a.UserID == userId).Count();
    }

    private string GetMotionsNames(ICollection<MotionsInPlanEntity> entities)
    {
      StringBuilder strB = new StringBuilder();
      foreach (var item in entities)
      {
        if (item.IsDeleted == true) continue;
        strB.Append(item.Motion.Name).Append(Consts.SPLITER);
      }

      return strB.ToString().Remove(strB.Length - 1);
    }

    private string SubString(string str)
    {
      var planRemarkLength =
        Convert.ToInt32(planRep.Ctx.KeyValues.FirstOrDefault(a => a.Key == DBKeys.PLAN_REMARK_LENGTH).Value);

      if (string.IsNullOrEmpty(str) || str.Length <= planRemarkLength) return str;
      return str.Substring(0, planRemarkLength) + "...";
    }

    private PlanDTO ToDTO(PlanEntity entity)
    {
      if (entity == null)
      {
        throw new Exception(ExceptionMsg.GetObjNullMsg("PlanEntity"));
      }
      var dto = new PlanDTO
      {
        ID = entity.ID
      };
      return dto;
    }
  }
}
