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

    [HttpGet]
    [Login]
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

    [HttpPost]
    [Login]
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
        Weight = model.Weight.Value,
        UpperArm = model.UpperArm,
        LowerArm = model.LowerArm,
        Chest = model.Chest,
        Waist = model.Waist,
        Hip = model.Hip,
        UpperLeg = model.UpperLeg,
        LowerLeg = model.LowerLeg
      };

      circumService.AddOrUpdate(dto);
      return Redirect("/Home/Index");
    }
  }
}