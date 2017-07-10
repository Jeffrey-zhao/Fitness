using Fit.Common;
using Fit.IService;
using Fit.Service.Entities;
using Fit.Service.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fit.DTO;

namespace Fit.Service.Services
{
  public class SecheduleService : ISecheduleService
  {
    IRepository<PlanEntity> planRep;
    IRepository<SecheduleEntity> secheduleRep;
    IRepository<SecheduleDetailEntity> secheduleDetailRep;
    IRepository<MotionsInPlanEntity> mipRep;

    public SecheduleService(IRepository<PlanEntity> planRep, IRepository<SecheduleEntity> secheuleRep
      , IRepository<SecheduleDetailEntity> secheuleDetailRep, IRepository<MotionsInPlanEntity> mipRep)
    {
      this.planRep = planRep;
      this.secheduleRep = secheuleRep;
      this.secheduleDetailRep = secheuleDetailRep;
      this.mipRep = mipRep;
    }

    public void CompleteItems(string itemIDs)
    {
      var arr = itemIDs.Split(new char[] { Consts.SPLITER }, StringSplitOptions.RemoveEmptyEntries);
      long temp = 0;
      foreach (var item in arr)
      {
        if (!Int64.TryParse(item, out temp)) continue;
        var detail = secheduleDetailRep.GetById(temp);
        detail.IsFinished = true;
        secheduleDetailRep.Ctx.SaveChanges();
      }
    }

    /// <summary>
    /// create sechedules within the given days: if the sechedule is empty, create all, 
    /// else if less than the given days, insert sechedules so that the count of new sechedules equal the given days
    /// </summary>
    public void CreateSechedule(long userID, int days, DateTime startDate)
    {
      startDate = new DateTime(startDate.Year, startDate.Month, startDate.Day);
      var plans = GetPlansByUserID(userID);
      if (plans == null || plans.Count() <= 0) return;
      var planIDs = plans.Select(a => a.ID).ToArray();
      var sechedules = secheduleRep.GetAll().Where(a => planIDs.Contains(a.PlanID) && a.ActDate >= startDate)
        .OrderByDescending(a => a.ActDate).ToList();
      var count = 0;
      if (sechedules != null && sechedules.Count() > 0)
      {
        count = sechedules.Count();
      }
      if (count == 0)
      {
        AddSechedulesAndDetails(0, days, startDate, plans);
      }
      else
      {
        //get the nextID of plans and it's corresponding index
        var currentPlanID = sechedules[0].PlanID;
        var minPlanID = plans[0].ID;
        var maxPlanID = plans[plans.Count - 1].ID;
        var nextID = minPlanID;
        if (currentPlanID < maxPlanID)
        {
          nextID = plans.Where(a => a.ID > currentPlanID).OrderBy(a => a.ID).First().ID;
        }
        //get the corresponding index of the nextID
        int index = -1;
        foreach (var item in plans)
        {
          index++;
          if (item.ID == nextID) break;
        }
        AddSechedulesAndDetails(index, days - count, startDate.AddDays(count), plans);
      }
    }

    public bool FinishSechedule(long userID)
    {
      var sechedule = GetCurrentDaySecheduleByUserID(userID);
      if (sechedule == null) return false;
      var secheduleDetails = GetSecheduleDetailsBySecheduleID(sechedule.ID);
      if (secheduleDetails == null || secheduleDetails.Count() <= 0) return false;

      sechedule.IsFinished = true;
      secheduleDetails.ForEach(a => a.IsFinished = true);
      secheduleRep.Ctx.SaveChanges();
      secheduleDetailRep.Ctx.SaveChanges();
      return true;
    }

    public CurrentItemDTO[] GetCurrentItems(long userID)
    {
      var sechedule = GetCurrentDaySecheduleByUserID(userID);
      if (sechedule == null) return null;
      var secheduleDetails = GetSecheduleDetailsBySecheduleID(sechedule.ID);
      return secheduleDetails.Select(a => ToCurrentItemDTO(a)).ToArray();
    }

    public long GetPersistDays(long userID)
    {
      var planIDs = planRep.GetAll().Where(a => a.UserID == userID).ToList().Select(a => a.ID).ToArray();
      return secheduleRep.GetAll()
        .Where(a => a.IsFinished == true && planIDs.Contains(a.PlanID)).Count();
    }

    public bool IsSecheduleFinished(long userID)
    {
      return GetCurrentDaySecheduleByUserID(userID).IsFinished;
    }

    private void AddSechedulesAndDetails(int index, int days, DateTime startDate, List<PlanEntity> plans)
    {
      for (int i = 1; i <= days; i++)
      {
        if (index > plans.Count() - 1) index = 0;  //if index reaches upper of the collection, return 0
        var plan = plans[index];  //get one with the index
        index++;  //index plus one, for next step using
        if (plan.MotionsInPlans == null || plan.MotionsInPlans.Count <= 0
          ||plan.MotionsInPlans.Where(a=>a.IsDeleted==false)==null
          ||!plan.MotionsInPlans.Where(a=>a.IsDeleted==false).Any()) continue;  //if it's a rest day, continue
        var sechedule = new SecheduleEntity
        {
          ActDate = startDate.AddDays(i - 1),
          PlanID = plan.ID
        };
        var addID = secheduleRep.Add(sechedule);
        //add SecheduleDetail with the addID
        foreach (var item in plan.MotionsInPlans)
        {
          if (item.IsDeleted == true) continue;
          var secheduleDetail = new SecheduleDetailEntity
          {
            MotionsInPlanID = item.ID,
            SecheduleID = addID
          };
          secheduleDetailRep.Add(secheduleDetail);
        }
      }
    }

    private List<PlanEntity> GetPlansByUserID(long userID)
    {
      return planRep.GetAll().Where(a => a.UserID == userID).OrderBy(a => a.ID).ToList();
    }

    private SecheduleEntity GetCurrentDaySecheduleByUserID(long userID)
    {
      var planIDs = GetPlansByUserID(userID).Select(a => a.ID).ToArray();
      var date = DateTimeHelper.GetToday();
      return secheduleRep.GetAll()
          .Where(a => a.ActDate == date && planIDs.Contains(a.PlanID)).FirstOrDefault();
    }

    private List<SecheduleDetailEntity> GetSecheduleDetailsBySecheduleID(long secheduleID)
    {
      return secheduleDetailRep.GetAll()
            .Where(a => a.SecheduleID == secheduleID).ToList();
    }

    private CurrentItemDTO ToCurrentItemDTO(SecheduleDetailEntity entity)
    {
      if (entity == null) throw new ArgumentException(ExceptionMsg.GetObjNullMsg("SecheduleDetailEntity"));
      var mip = mipRep.GetById(entity.MotionsInPlanID);
      var dto = new CurrentItemDTO
      {
        SecheduleDetailID = entity.ID,
        IsFinished = entity.IsFinished,
        ItemName = mip.Motion.Name
      };
      if (mip.Groups <= 0)  //综合训练
      {
        dto.ItemBurden = string.Format(Consts.PLAN_TEMPLATE_DETAIL2, mip.Number, mip.Motion.Measurement);
      }
      else  //局部训练
      {
        if (mip.Number <= 0)  //无负重
        {
          dto.ItemBurden = string.Format(Consts.PLAN_TEMPLATE_DETAIL1
            , mip.Groups, mip.Times, string.Empty);
        }
        else  //负重
        {
          dto.ItemBurden = string.Format(Consts.PLAN_TEMPLATE_DETAIL1
            , mip.Groups, mip.Times
            , string.Format(Consts.PLAN_TEMPLATE_DETAIL1_1, mip.Number));
        }
      }
      return dto;
    }
  }
}
