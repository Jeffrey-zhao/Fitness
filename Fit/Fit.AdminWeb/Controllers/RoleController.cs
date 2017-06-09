using Fit.AdminWeb.App_Start;
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
    private IPermissionService permissionService;
    public RoleController(IPermissionService permissionService, IRoleService roleService)
    {
      this.roleService = roleService;
      this.permissionService = permissionService;
    }

    //[Permission("Role.List")]
    public ActionResult List(int pageIndex = 1)
    {
      var roles = roleService.GetPagedData((pageIndex - 1) * Consts.PAGE_SIZE_NUM, Consts.PAGE_SIZE_NUM);
      ViewBag.PageIndex = pageIndex;
      ViewBag.TotalCount = roleService.GetTotalCount();
      return View(roles);
    }

    //[Permission("Role.Add")]
    [HttpGet]
    public ActionResult Add()
    {
      var permissions = permissionService.GetAll();
      return View(permissions);
    }
    //[Permission("Role.Add")]
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

      var roleId = roleService.Add(dto);
      permissionService.EditRolePermission(roleId, model.PermissionIDs);
      return MVCHelper.GetJsonResult(AjaxResultEnum.ok);
    }

    //[Permission("Role.Edit")]
    [HttpGet]
    public ActionResult Edit(long id)
    {
      var permissions = permissionService.GetAll();
      var permissionIDs = permissionService.GetIDsByRole(id);
      var role = roleService.GetById(id);
      var model = new RoleModel
      {
        ID = id,
        Name = role.Name,
        Description = role.Description,
        PermissionIDs = permissionIDs,
        AllPermissions = permissions
      };
      return View(model);
    }
    //[Permission("Role.Edit")]
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
      permissionService.EditRolePermission(model.ID, model.PermissionIDs);
      return MVCHelper.GetJsonResult(AjaxResultEnum.ok);
    }

    //[Permission("Role.Delete")]
    public ActionResult Delete(long id)
    {
      roleService.Delete(id);
      return MVCHelper.GetJsonResult(AjaxResultEnum.ok);
    }
  }
}