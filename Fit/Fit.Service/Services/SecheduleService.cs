using Fit.Common;
using Fit.IService;
using Fit.Service.Entities;
using Fit.Service.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fit.Service.Services
{
  public class SecheduleService:ISecheduleService
  {
    IRepository<PlanEntity> planRep;
    IRepository<SecheduleEntity> secheduleRep;
    IRepository<SecheduleDetailEntity> secheduleDetailRep;

    public SecheduleService(IRepository<PlanEntity> planRep, IRepository<SecheduleEntity> secheuleRep, IRepository<SecheduleDetailEntity> secheuleDetailRep)
    {
      this.planRep = planRep;
      this.secheduleRep = secheuleRep;
      this.secheduleDetailRep = secheuleDetailRep;
    }

    /// <summary>
    /// create sechedules within the given days: if the sechedule is empty, create all, 
    /// else if less than the given days, insert sechedules so that the count of new sechedules equal the given days
    /// </summary>
    public void CreateSechedule(long userID, int days, DateTime startDate)
    {
      startDate = new DateTime(startDate.Year, startDate.Month, startDate.Day);
      var plans = planRep.GetAll().Where(a => a.UserID == userID).OrderBy(a => a.ID).ToList();
      if (plans == null || plans.Count() <= 0) return;
      var planIDs = plans.Select(a => a.ID).ToArray();
      var sechedules = secheduleRep.GetAll().Where(a => planIDs.Contains(a.PlanID)).OrderByDescending(a => a.ActDate).ToList();
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
        AddSechedulesAndDetails(index, days-count, startDate.AddDays(count), plans);
      }
    }

    private void AddSechedulesAndDetails(int index, int days, DateTime startDate, List<PlanEntity> plans)
    {
      for (int i = 1; i <= days; i++)
      {
        if (index > plans.Count() - 1) index = 0;  //if index reaches upper of the collection, return 0
        var plan = plans[index];  //get one with the index
        index++;  //index plus one, for next step using
        if (plan.MotionsInPlans == null || plan.MotionsInPlans.Count <= 0) continue;  //if it's a rest day, continue
        var sechedule = new SecheduleEntity
        {
          ActDate = startDate.AddDays(i - 1),
          PlanID = plan.ID
        };
        var addID = secheduleRep.Add(sechedule);
        //add SecheduleDetail with the addID
        foreach (var item in plan.MotionsInPlans)
        {
          var secheduleDetail = new SecheduleDetailEntity
          {
            MotionsInPlanID = item.ID,
            SecheduleID = addID
          };
          secheduleDetailRep.Add(secheduleDetail);
        }
      }
    }
  }
}
