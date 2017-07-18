using Fit.Common;
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
  public class HomeController : Controller
  {
    ISecheduleService secheduleService;
    IKeyValueService kvService;

    public HomeController(ISecheduleService secheduleService, IKeyValueService kvService)
    {
      this.secheduleService = secheduleService;
      this.kvService = kvService;
    }

    [Login]
    public ActionResult Index()
    {
      var loginID = MVCHelper.GetLoginIdFromSession(HttpContext).Value;
      var dtos = secheduleService.GetCurrentItems(loginID);
      ViewBag.PersistDays = secheduleService.GetPersistDays(loginID);

      ViewBag.IsSecheduleFinished = (dtos != null && dtos.Count() > 0)
        ? secheduleService.IsSecheduleFinished(loginID) : true;
      return View(dtos);
    }

    [Login]
    public ActionResult CompleteItems(string itemIDs)
    {
      if (string.IsNullOrWhiteSpace(itemIDs))
      {
        return MVCHelper.GetJsonResult(AjaxResultEnum.error);
      }

      secheduleService.CompleteItems(itemIDs);
      return MVCHelper.GetJsonResult(AjaxResultEnum.ok);
    }

    [Login]
    public ActionResult FinishSechedule()
    {
      var loginID = MVCHelper.GetLoginIdFromSession(HttpContext).Value;
      var result = secheduleService.FinishSechedule(loginID);
      return result ? MVCHelper.GetJsonResult(AjaxResultEnum.ok)
                              : MVCHelper.GetJsonResult(AjaxResultEnum.error);
    }

    public ActionResult Introduce()
    {
      var introduces = kvService.GetValue(DBKeys.INTRODUCE_IMAGE_URLS);
      var introduceArr = introduces.Split(Consts.SPLITER);
      var models =new List<IntroduceModel>();
      for (int i = 0; i < introduceArr.Length; i++)
      {
        var detalModelArr = introduceArr[i].Split(Consts.SPLITER_DASH);
        var introduceModel = new IntroduceModel
        {
          Title = detalModelArr[0],
          Introduce = detalModelArr[1],
          Url =Consts.CLOUD_DOMAIN+"/"+ detalModelArr[2],
        };
        models.Add(introduceModel);
      }
      return View(models);
    }
  }
}