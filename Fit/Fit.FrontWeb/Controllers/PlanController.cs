using Fit.Common;
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

    [HttpGet]
    public ActionResult SetCycleDays()
    {
      var maxCycleDays = kvService.GetIntValue(DBKeys.COM_MAX_CYCLEDAYS);
      return View(maxCycleDays);
    }
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

    [HttpGet]
    public ActionResult CreateSechedule()
    {
      var loginID = MVCHelper.GetLoginIdFromSession(HttpContext).Value;
      var maxSecheduleDays = kvService.GetIntValue(DBKeys.PLAN_MAX_SECHEDULEDAYS);
      secheduleService.CreateSechedule(loginID, maxSecheduleDays, DateTimeHelper.GetNow());
      return View();
    }

    public ActionResult Sechedule()
    {
      return View();
    }

    [HttpPost]
    public ActionResult GetSechedule(string start, string end)
    {
      var loginID = MVCHelper.GetLoginIdFromSession(HttpContext).Value;
      var dtos = planService.GetSechedule(loginID, start, end);
      return MVCHelper.GetJsonResult(new AjaxResult { Data = dtos });
    }

  }
}