using Fit.AdminWeb.Models;
using Fit.Common;
using Fit.DTO.RBAC;
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

    public ActionResult List(int pageIndex = 1)
    {
      var pms = pmService.GetPagedData((pageIndex - 1) * Consts.PAGE_SIZE_NUM, Consts.PAGE_SIZE_NUM);
      ViewBag.PageIndex = pageIndex;
      ViewBag.TotalCount = pmService.GetTotalCount();
      return View(pms);
    }
    [HttpGet]
    public ActionResult Add()
    {
      return View();
    }
    [HttpPost]
    public ActionResult Add(PermissionModel model)
    {
      if (!ModelState.IsValid)
      {
        return MVCHelper.GetJsonResult(AjaxResultEnum.error, MVCHelper.GetValidMsg(ModelState));
      }
      var dto = new PermissionDTO
      {
        Name=model.Name,
        Description=model.Description
      };
      pmService.Add(dto);
      return MVCHelper.GetJsonResult(AjaxResultEnum.ok);
    }

    [HttpGet]
    public ActionResult Edit(long id)
    {
      var dto = pmService.GetById(id);
      return View(dto);
    }
    [HttpPost]
    public ActionResult Edit(PermissionModel model)
    {
      if (!ModelState.IsValid)
      {
        return MVCHelper.GetJsonResult(AjaxResultEnum.error, MVCHelper.GetValidMsg(ModelState));
      }
      var dto = new PermissionDTO
      {
        Id=model.ID,
        Name=model.Name,
        Description=model.Description
      };
      pmService.Update(dto);
      return MVCHelper.GetJsonResult(AjaxResultEnum.ok);
    }

    public ActionResult Delete(long id)
    {
      pmService.Delete(id);
      return MVCHelper.GetJsonResult(AjaxResultEnum.ok);
    }
  }
}