using CaptchaGen;
using Fit.Common;
using Fit.DTO;
using Fit.FrontWeb.Models;
using Fit.IService;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Fit.FrontWeb.Controllers
{
  public class UserController : Controller
  {
    IUserService userService;
    IKeyValueService kyService;
    public UserController(IUserService userService, IKeyValueService kyService)
    {
      this.userService = userService;
      this.kyService = kyService;
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
      //long? id = auService.CheckLogin(model.Email, model.Password);
      //if (id.HasValue)
      //{
      //  MVCHelper.SetLoginInfoToSession(HttpContext, id.Value, model.Email);
      //  return MVCHelper.GetJsonResult(AjaxResultEnum.ok);
      //}
      //else
      //{
      //  return MVCHelper.GetJsonResult(AjaxResultEnum.error, "Email or Password is wrong");
      //}
      return View();
    }
    [HttpGet]
    public ActionResult Register()
    {
      var a = kyService.GetValue("a");
      return View();
    }
    [HttpPost]
    public ActionResult Register(RegisterModel model)
    {
      if (!ModelState.IsValid)
      {
        return MVCHelper.GetJsonResult(AjaxResultEnum.error, MVCHelper.GetValidMsg(ModelState));
      }
      var dto = new UserDTO
      {
        Name = model.Name,
        Email = model.Email,
        Password = model.Password
      };
      var resultDto = userService.Add(dto);
      TempData["email"] = model.Email;
      //TempData["id"] = resultDto.ID;
      //TempData["operateCode"] = resultDto.OperateCode;
      return MVCHelper.GetJsonResult(AjaxResultEnum.ok);
    }

    public ActionResult CreateVerifyCode()
    {
      var verifyCode = CommonHelper.GenerateCaptchaCode(4);
      TempData[Consts.VERIFY_CODE_KEY] = verifyCode;
      MemoryStream ms = ImageFactory.GenerateImage(verifyCode, 42, 70, 14, 1);
      return File(ms, "image/jpeg");
    }

    [HttpGet]
    public ActionResult EmailPage()
    {
      object model = new object();
      if (TempData["email"] != null && !string.IsNullOrWhiteSpace(TempData["email"].ToString()))
      {
        model = TempData["email"];
      }
      return View(model);
    }
  }
}