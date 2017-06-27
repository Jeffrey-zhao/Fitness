using Fit.Common;
using Fit.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Fit.AdminWeb.Controllers
{
  public class UserInfoController : Controller
  {
    IUserService userService;
    public UserInfoController(IUserService userService)
    {
      this.userService = userService;
    }
    // GET: UserInfo
    public ActionResult List(int pageIndex = 1)
    {
      var dtos = userService.GetPagedData((pageIndex - 1) * Consts.PAGE_SIZE_NUM, Consts.PAGE_SIZE_NUM);
      ViewBag.TotalCount = userService.GetTotalCount();
      ViewBag.PageIndex = pageIndex;
      return View(dtos);
    }
  }
}