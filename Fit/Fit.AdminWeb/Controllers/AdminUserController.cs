using CaptchaGen;
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
    private IAdminUserService auService;
    public AdminUserController(IAdminUserService service)
    {
      auService = service;
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
      if (!TempData[Consts.VERIFY_CODE_KEY].ToString().Equals(model.VerifyCode))
      {
        return MVCHelper.GetJsonResult(AjaxResultEnum.error, "Verify Code Error");
      }
      bool result = auService.CheckLogin(model.Email, model.Password);
      if (result)
      {
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

    public ActionResult List(int pageIndex = 1)
    {
      var adminUsers = auService.GetPagedData((pageIndex - 1) * Consts.PAGE_SIZE_NUM, Consts.PAGE_SIZE_NUM);
      ViewBag.TotalCount = auService.GetTotalCount();
      ViewBag.PageIndex = pageIndex;
      return View(adminUsers);
    }

    [HttpGet]
    public ActionResult Edit(long id)
    {
      var adminUser = auService.GetById(id);
      return View(adminUser);
    }
    [HttpPost]
    public ActionResult Edit(AdminUserEditModel model)
    {
      if (!ModelState.IsValid)
      {
        return MVCHelper.GetJsonResult(AjaxResultEnum.error, MVCHelper.GetValidMsg(ModelState));
      }
      var dto = new AdminUserDTO
      {
        ID=model.ID,
        Name=model.Name,
        PhoneNum=model.PhoneNum,
        Email=model.Email,
        Password=model.Password,
        WillUpdatePwd=!string.IsNullOrWhiteSpace(model.Password)
      };

      auService.Update(dto);
      return View();
    }
  }
}