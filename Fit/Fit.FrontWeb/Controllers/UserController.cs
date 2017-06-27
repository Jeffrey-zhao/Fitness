using Fit.Common;
using Fit.FrontWeb.Models;
using Fit.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Fit.FrontWeb.Controllers
{
  public class UserController : Controller
  {
    private IUserService userService;
    public UserController(IUserService userService)
    {
      this.userService = userService;
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
  }
}