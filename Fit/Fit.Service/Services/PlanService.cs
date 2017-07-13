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
    IRepository<SecheduleEntity> secheduleRep;
    IRepository<SecheduleDetailEntity> secheduleDetailRep;
    IRepository<UserEntity> userRep;

    public PlanService(IRepository<PlanEntity> planRep, IRepository<SecheduleEntity> secheduleRep
                                    , IRepository<SecheduleDetailEntity> secheduleDetailRep
                                    , IRepository<UserEntity> userRep)
    {
      this.planRep = planRep;
      this.secheduleRep = secheduleRep;
      this.secheduleDetailRep = secheduleDetailRep;
      this.userRep = userRep;
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

    public SecheduleDTO[] GetSechedule(long userId, string startDateStr, string endDateStr)
    {
      var startDate = new DateTime();
      var endDate = new DateTime();
      var temp1 = DateTime.TryParse(startDateStr, out startDate);
      var temp2 = DateTime.TryParse(endDateStr, out endDate);
      if (!temp1 && temp2)
      {
        throw new ArgumentException();
      }

      var planIDs = planRep.GetAll().Where(a => a.UserID == userId).ToList().Select(a => a.ID).ToArray();
      var sechedules = secheduleRep.GetAll()
        .Where(a => a.ActDate >= startDate && a.ActDate <= endDate && planIDs.Contains(a.PlanID)).ToList();

      if (sechedules == null) return null;
      return sechedules.Select(a => ToSecheduleDTO(a)).ToArray();
    }

    public SecheduleDTO ToSecheduleDTO(SecheduleEntity entity)
    {
      if (entity == null) throw new ArgumentException(ExceptionMsg.GetObjNullMsg("SecheduleEntity"));

      var dto = new SecheduleDTO
      {
        Title = string.Empty,
        Start = entity.ActDate.ToShortDateString(),
        Color = entity.IsFinished ? Consts.COLOR_FINISHED : Consts.COLOR_UNFINISHED
      };
      if (entity.ActDate.Equals(DateTimeHelper.GetToday()))
      {
        dto.Color = Consts.COLOR_TODAY;
      }
      return dto;
    }

    public PieChartDTO[] CompareCombineAndPartial(long userID)
    {
      var entities = planRep.GetAll().Where(a => a.UserID == userID).Include(a => a.MotionsInPlans).ToList();
      int combineCount = 0, partialCount = 0;
      foreach (var plan in entities)
      {
        if (plan.MotionsInPlans == null || !plan.MotionsInPlans.Any()) continue;
        foreach (var mip in plan.MotionsInPlans)
        {
          if (mip.IsDeleted == true) continue;
          if (mip.Motion.MuscleID.HasValue)
          {
            partialCount++;
          }
          else
          {
            combineCount++;
          }
        }
      }

      var dtos = new PieChartDTO[2];
      var dto = new PieChartDTO
      {
        Label = Consts.TEXT_COMBINE,
        Value = combineCount
      };
      dtos[0] = dto;
      dto = new PieChartDTO
      {
        Label = Consts.TEXT_PARTIAL,
        Value = partialCount
      };
      dtos[1] = dto;
      return dtos;
    }

    public PieChartDTO[] CompareMuscleGroups(long userID)
    {
      throw new NotImplementedException();
    }
  }
}