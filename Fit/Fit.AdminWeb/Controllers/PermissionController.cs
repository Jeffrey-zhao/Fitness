using Fit.Common;
using Fit.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Fit.AdminWeb.Controllers
{
  public class PermissionController : Controller
  {
    private IPermissionService pmService;
    public PermissionController(IPermissionService service)
    {
      this.pmService = service;
    }

    // GET: Permission
    public ActionResult List(int pageIndex = 1)
    {
      var pms = pmService.GetPagedData((pageIndex - 1) * Consts.PAGE_SIZE_NUM, Consts.PAGE_SIZE_NUM);
      ViewBag.PageIndex = pageIndex;
      ViewBag.TotalCount = pmService.GetTotalCount();
      return View(pms);
    }
  }
}