using Fit.Common;
using Fit.DTO;
using Fit.FrontWeb.App_Start;
using Fit.FrontWeb.Models;
using Fit.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Fit.FrontWeb.Controllers
{
  public class BodyCircumferenceController : Controller
  {
    IBodyCircumferenceService circumService;
    public BodyCircumferenceController(IBodyCircumferenceService circumService)
    {
      this.circumService = circumService;
    }

    [Login]
    [HttpGet]
    public ActionResult Record()
    {
      var userID = MVCHelper.GetLoginIdFromSession(HttpContext).Value;
      var dto = circumService.GetCurrentByUserID(userID);
      if (dto == null)
      {
        return View("AddRecord", userID);
      }
      else
      {
        return View("EditRecord", dto);
      }
    }

    [Login]
    [HttpPost]
    public ActionResult Record(BodyCircumferenceModel model)
    {
      if (!ModelState.IsValid)
      {
        return Content(MVCHelper.GetValidMsg(ModelState));
      }
      var dto = new BodyCircumferenceDTO
      {
        ID = model.ID,
        UserID = model.UserID,
        Weight = model.Weight ?? 0,
        UpperArm = model.UpperArm ?? 0,
        LowerArm = model.LowerArm ?? 0,
        Chest = model.Chest ?? 0,
        Waist = model.Waist ?? 0,
        Hip = model.Hip ?? 0,
        UpperLeg = model.UpperLeg ?? 0,
        LowerLeg = model.LowerLeg ?? 0
      };

      circumService.AddOrUpdate(dto);
      return Redirect("/Home/Index");
    }

    [Login]
    public ActionResult Chart()
    {
      return View();
    }

    [Login]
    [HttpPost]
    public ActionResult LoadWeightData()
    {
      var userID = MVCHelper.GetLoginIdFromSession(HttpContext).Value;
      var dtos = circumService.GetWeightHistory(userID);
      var model = new ChartDataModel { Data = dtos, Legend = Consts.BODY_WEIGHT };
      return MVCHelper.GetJsonResult(new AjaxResult { Data = model });
    }

    [Login]
    [HttpPost]
    public ActionResult LoadUpperArmData()
    {
      var userID = MVCHelper.GetLoginIdFromSession(HttpContext).Value;
      var dtos = circumService.GetUpperArmHistory(userID);
      var model = new ChartDataModel { Data = dtos, Legend = Consts.BODY_UPPER_ARM };
      return MVCHelper.GetJsonResult(new AjaxResult { Data = model });
    }

    [Login]
    [HttpPost]
    public ActionResult LoadLowerArmData()
    {
      var userID = MVCHelper.GetLoginIdFromSession(HttpContext).Value;
      var dtos = circumService.GetLowerArmHistory(userID);
      var model = new ChartDataModel { Data = dtos, Legend = Consts.BODY_LOWER_ARM };
      return MVCHelper.GetJsonResult(new AjaxResult { Data = model });
    }

    [Login]
    [HttpPost]
    public ActionResult LoadChestData()
    {
      var userID = MVCHelper.GetLoginIdFromSession(HttpContext).Value;
      var dtos = circumService.GetChestHistory(userID);
      var model = new ChartDataModel { Data = dtos, Legend = Consts.BODY_CHEST };
      return MVCHelper.GetJsonResult(new AjaxResult { Data = model });
    }

    [Login]
    [HttpPost]
    public ActionResult LoadWaistData()
    {
      var userID = MVCHelper.GetLoginIdFromSession(HttpContext).Value;
      var dtos = circumService.GetWaistHistory(userID);
      var model = new ChartDataModel { Data = dtos, Legend = Consts.BODY_WAIST };
      return MVCHelper.GetJsonResult(new AjaxResult { Data = model });
    }

    [Login]
    [HttpPost]
    public ActionResult LoadHipData()
    {
      var userID = MVCHelper.GetLoginIdFromSession(HttpContext).Value;
      var dtos = circumService.GetHipHistory(userID);
      var model = new ChartDataModel { Data = dtos, Legend = Consts.BODY_HIP };
      return MVCHelper.GetJsonResult(new AjaxResult { Data = model });
    }

    [Login]
    [HttpPost]
    public ActionResult LoadUpperLegData()
    {
      var userID = MVCHelper.GetLoginIdFromSession(HttpContext).Value;
      var dtos = circumService.GetUpperLegHistory(userID);
      var model = new ChartDataModel { Data = dtos, Legend = Consts.BODY_UPPER_LEG };
      return MVCHelper.GetJsonResult(new AjaxResult { Data = model });
    }

    [Login]
    [HttpPost]
    public ActionResult LoadLowerLegData()
    {
      var userID = MVCHelper.GetLoginIdFromSession(HttpContext).Value;
      var dtos = circumService.GetLowerLegHistory(userID);
      var model = new ChartDataModel { Data = dtos, Legend = Consts.BODY_LOWER_LEG };
      return MVCHelper.GetJsonResult(new AjaxResult { Data = model });
    }

    [Login]
    public ActionResult AnalyzePlan()
    {
      return View();
    }
  }
}