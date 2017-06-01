using CaptchaGen;
using Fit.AdminWeb.Models;
using Fit.Common;
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
    public IAdminUserService AuSerivice { get; set; }

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
      bool result = AuSerivice.CheckLogin(model.Email, model.Password);
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

  }
}