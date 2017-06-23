using Fit.Common;
using Fit.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Fit.AdminWeb.Controllers
{
  public class AdminLogController : Controller
  {
    IAdminLogService service;
    public AdminLogController(IAdminLogService service)
    {
      this.service = service;
    }
    // GET: AdminLog
    public ActionResult List(int pageIndex = 1)
    {
      var dtos = service.GetPagedData((pageIndex - 1) * Consts.PAGE_SIZE_NUM, Consts.PAGE_SIZE_NUM);
      ViewBag.TotalCount = service.GetTotalCount();
      ViewBag.PageIndex = pageIndex;
      return View(dtos);
    }
  }
}