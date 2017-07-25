using Fit.Common;
using Fit.FrontWeb.App_Start;
using Fit.FrontWeb.Models;
using Fit.IService;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Fit.FrontWeb.Controllers
{
  public class PlanController : Controller
  {
    IPlanService planService;
    IKeyValueService kvService;
    ISecheduleService secheduleService;

    public PlanController(IPlanService planService, ISecheduleService secheduleService, IKeyValueService kvService)
    {
      this.planService = planService;
      this.secheduleService = secheduleService;
      this.kvService = kvService;
    }

    [Login]
    [HttpGet]
    public ActionResult SetCycleDays()
    {
      var maxCycleDays = kvService.GetIntValue(DBKeys.COM_MAX_CYCLEDAYS);
      return View(maxCycleDays);
    }
    [Login]
    [HttpPost]
    public ActionResult SetCycleDays(int cycleDays)
    {
      var maxCycleDays = kvService.GetIntValue(DBKeys.COM_MAX_CYCLEDAYS);
      if (cycleDays <= 0 || cycleDays > maxCycleDays)
      {
        throw new ArgumentException();
      }

      var loginID = MVCHelper.GetLoginIdFromSession(HttpContext);

      planService.ReAddPlan(loginID.Value, cycleDays);
      return Redirect("/Plan/CycleDays");
    }
    [Login]
    [HttpGet]
    public ActionResult CycleDays()
    {
      var loginID = MVCHelper.GetLoginIdFromSession(HttpContext).Value;
      var dtos = planService.GetUserPlans(loginID);
      ViewBag.PlanCount = planService.GetPlanCount(loginID);
      if (dtos == null || dtos.Length <= 0)
      {
        return Redirect("/Plan/CycleDays");
      }

      return View(dtos);
    }

    [Login]
    [HttpGet]
    public ActionResult CreateSechedule()
    {
      var loginID = MVCHelper.GetLoginIdFromSession(HttpContext).Value;
      var maxSecheduleDays = kvService.GetIntValue(DBKeys.PLAN_MAX_SECHEDULEDAYS);
      secheduleService.CreateSechedule(loginID, maxSecheduleDays, DateTimeHelper.GetNow());
      return View();
    }

    [Login]
    public ActionResult Sechedule()
    {
      return View();
    }

    [Login]
    [HttpPost]
    public ActionResult GetSechedule(string start, string end)
    {
      var loginID = MVCHelper.GetLoginIdFromSession(HttpContext).Value;
      var dtos = planService.GetSechedule(loginID, start, end);
      return MVCHelper.GetJsonResult(new AjaxResult { Data = dtos });
    }

    [Login]
    [HttpPost]
    public ActionResult ComparePartialAndCombine()
    {
      var loginID = MVCHelper.GetLoginIdFromSession(HttpContext).Value;
      var dtos = planService.CompareCombineAndPartial(loginID);
      var colorStr = kvService.GetValue(DBKeys.COLOR_FOR_2);
      var colorArr = colorStr.Split(Consts.SPLITER);
      for (int i = 0; i < dtos.Length; i++)
      {
        dtos[i].Color = colorArr[i];
      }
      return MVCHelper.GetJsonResult(new AjaxResult { Data = dtos });
    }

    [Login]
    [HttpPost]
    public ActionResult CompareMuscleGroups()
    {
      var loginID = MVCHelper.GetLoginIdFromSession(HttpContext).Value;
      var dtos = planService.CompareMuscleGroups(loginID);
      var colorStr = kvService.GetValue(DBKeys.COLOR_FOR_8);
      var colorArr = colorStr.Split(Consts.SPLITER);
      for(int i= 0;i<dtos.Length;i++)
      {
        dtos[i].Color = colorArr[i];
      }
      return MVCHelper.GetJsonResult(new AjaxResult { Data = dtos });
    }

  }
}