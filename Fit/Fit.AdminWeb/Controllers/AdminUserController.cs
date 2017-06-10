using CaptchaGen;
using Fit.AdminWeb.App_Start;
using Fit.AdminWeb.Models;
using Fit.Common;
using Fit.DTO.RBAC;
using Fit.IService;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Fit.AdminWeb.Controllers
{
  public class AdminUserController : Controller
  {
    private IRoleService roleService;
    private IAdminUserService auService;
    public AdminUserController(IRoleService roleService, IAdminUserService auService)
    {
      this.roleService = roleService;
      this.auService = auService;
    }

    [HttpGet]
    public ActionResult Login()
    {
      return View();
    }
    [HttpPost]
    public ActionResult Login(LoginModel model)
    {
      if (!ModelState.IsValid)
      {
        return MVCHelper.GetJsonResult(AjaxResultEnum.error, MVCHelper.GetValidMsg(ModelState));
      }
      if (TempData[Consts.VERIFY_CODE_KEY] == null
        || !TempData[Consts.VERIFY_CODE_KEY].ToString().Equals(model.VerifyCode))
      {
        return MVCHelper.GetJsonResult(AjaxResultEnum.error, "Verify Code Error");
      }
      long? id = auService.CheckLogin(model.Email, model.Password);
      if (id.HasValue)
      {
        MVCHelper.SetLoginInfoToSession(HttpContext, id.Value, model.Email);
        return MVCHelper.GetJsonResult(AjaxResultEnum.ok);
      }
      else
      {
        return MVCHelper.GetJsonResult(AjaxResultEnum.error, "Email or Password is wrong");
      }
    }

    public ActionResult CreateVerifyCode()
    {
      var verifyCode = CommonHelper.GenerateCaptchaCode(4);
      TempData[Consts.VERIFY_CODE_KEY] = verifyCode;
      MemoryStream ms = ImageFactory.GenerateImage(verifyCode, 42, 70, 14, 1);
      return File(ms, "image/jpeg");
    }

    //[Permission("AdminUser.List")]
    public ActionResult List(int pageIndex = 1)
    {
      var adminUsers = auService.GetPagedData((pageIndex - 1) * Consts.PAGE_SIZE_NUM, Consts.PAGE_SIZE_NUM);
      ViewBag.TotalCount = auService.GetTotalCount();
      ViewBag.PageIndex = pageIndex;
      return View(adminUsers);
    }

    //[Permission("AdminUser.Edit")]
    [HttpGet]
    public ActionResult Edit(long id)
    {
      var adminUser = auService.GetById(id);
      var roles = roleService.GetAll();
      var roleIDs = roleService.GetIDsByAdmin(id);
      var model = new AdminUserEditViewModel
      {
        ID = adminUser.ID,
        Name = adminUser.Name,
        PhoneNum = adminUser.PhoneNum,
        Email = adminUser.Email,
        AllRoles = roles,
        RoleIDs = roleIDs
      };

      return View(model);
    }
    //[Permission("AdminUser.Edit")]
    [HttpPost]
    public ActionResult Edit(AdminUserEditModel model)
    {
      if (!ModelState.IsValid)
      {
        return MVCHelper.GetJsonResult(AjaxResultEnum.error, MVCHelper.GetValidMsg(ModelState));
      }
      var dto = new AdminUserDTO
      {
        ID = model.ID,
        Name = model.Name,
        PhoneNum = model.PhoneNum,
        Email = model.Email,
        Password = model.Password,
        WillUpdatePwd = !string.IsNullOrWhiteSpace(model.Password)
      };
      auService.Update(dto);
      roleService.EditAdminRole(model.ID, model.RoleIDs);
      return MVCHelper.GetJsonResult(AjaxResultEnum.ok);
    }

    //[Permission("AdminUser.Add")]
    [HttpGet]
    public ActionResult Add()
    {
      var roles = roleService.GetAll();
      return View(roles);
    }
    //[Permission("AdminUser.Add")]
    [HttpPost]
    public ActionResult Add(AdminUserAddModel model)
    {
      if (!ModelState.IsValid)
      {
        return MVCHelper.GetJsonResult(AjaxResultEnum.error, MVCHelper.GetValidMsg(ModelState));
      }
      var id = auService.AddAdminUser(model.Name, model.PhoneNum, model.Email, model.Password);
      roleService.EditAdminRole(id, model.RoleIDs);
      return MVCHelper.GetJsonResult(AjaxResultEnum.ok);
    }

    //[Permission("AdminUser.Delete")]
    public ActionResult Delete(long id)
    {
      auService.MarkDeleted(id);
      return MVCHelper.GetJsonResult(AjaxResultEnum.ok);
    }

    [ChildActionOnly]
    public ActionResult LoginEmail()
    {
      var email = MVCHelper.GetLoginEmailFromSession(HttpContext);
      return PartialView("LoginEmail",email);
    }
  }
}