using Fit.AdminWeb.App_Start;
using Fit.Common;
using Fit.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Fit.AdminWeb.Controllers
{
  public class MuscleGroupController : Controller
  {
    IMuscleGroupService service;
    public MuscleGroupController(IMuscleGroupService service)
    {
      this.service = service;
    }
    [Permission("MuscleGroup.List")]
    public ActionResult List(int pageIndex = 1)
    {
      var groups = service.GetPagedData((pageIndex - 1) * Consts.PAGE_SIZE_NUM, Consts.PAGE_SIZE_NUM);
      ViewBag.TotalCount = service.GetTotalCount();
      ViewBag.PageIndex = pageIndex;
      return View(groups);
    }
  }
}