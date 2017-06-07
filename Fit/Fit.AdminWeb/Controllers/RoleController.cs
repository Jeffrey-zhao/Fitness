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
  public class RoleController : Controller
  {
    private IRoleService roleService;
    public RoleController(IRoleService service)
    {
      this.roleService = service;
    }
    // GET: Role
    public ActionResult List(int pageIndex = 1)
    {
      var roles = roleService.GetPagedData((pageIndex - 1) * Consts.PAGE_SIZE_NUM, Consts.PAGE_SIZE_NUM);
      ViewBag.PageIndex = pageIndex;
      ViewBag.TotalCount = roleService.GetTotalCount();
      return View(roles);
    }

    [HttpGet]
    public ActionResult Add()
    {
      return View();
    }
    [HttpPost]
    public ActionResult Add(RoleModel model)
    {
      if (!ModelState.IsValid)
      {
        return MVCHelper.GetJsonResult(AjaxResultEnum.error, MVCHelper.GetValidMsg(ModelState));
      }
      var dto = new RoleDTO
      {
        Name = model.Name,
        Description = model.Description
      };

      roleService.Add(dto);
      return MVCHelper.GetJsonResult(AjaxResultEnum.ok);
    }

    [HttpGet]
    public ActionResult Edit(long id)
    {
      var role = roleService.GetById(id);
      return View(role);
    }
    [HttpPost]
    public ActionResult Edit(RoleModel model)
    {
      if (!ModelState.IsValid)
      {
        return MVCHelper.GetJsonResult(AjaxResultEnum.error, MVCHelper.GetValidMsg(ModelState));
      }
      var dto = new RoleDTO
      {
        Id = model.ID,
        Name = model.Name,
        Description = model.Description
      };

      roleService.Update(dto);
      return MVCHelper.GetJsonResult(AjaxResultEnum.ok);
    }

    public ActionResult Delete(long id)
    {
      roleService.Delete(id);
      return MVCHelper.GetJsonResult(AjaxResultEnum.ok);
    }
  }
}