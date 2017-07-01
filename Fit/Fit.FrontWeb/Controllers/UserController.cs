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
    IKeyValueService kvService;
    public UserController(IUserService userService, IKeyValueService kyService)
    {
      this.userService = userService;
      this.kvService = kyService;
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
      if (TempData[SessionKeys.VERIFYCODE] == null
        || !TempData[SessionKeys.VERIFYCODE].ToString().Equals(model.VerifyCode))
      {
        return MVCHelper.GetJsonResult(AjaxResultEnum.error, Consts.VERIFY_CODE_ERROR);
      }
      long? id = userService.CheckLogin(model.Email, model.Password);
      if (id.HasValue)
      {
        MVCHelper.SetLoginInfoToSession(HttpContext, id.Value, model.Email);
        return MVCHelper.GetJsonResult(AjaxResultEnum.ok);
      }
      else
      {
        return MVCHelper.GetJsonResult(AjaxResultEnum.error, Consts.LOGIN_FAILED);
      }
    }
    [HttpGet]
    public ActionResult Register()
    {
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
      SendActivateEmail(resultDto.ID, resultDto.OperateCode, model.Email);
      TempData[SessionKeys.SESSION_EMAIL] = model.Email;
      return MVCHelper.GetJsonResult(AjaxResultEnum.ok);
    }

    public ActionResult Activate(long id, string operateCode)
    {
      if (userService.Activate(id, operateCode))
      {
        return View((object)Consts.ACTIVATE_SUCCEED);
      }
      else
      {
        return View((object)Consts.ACTIVATE_FAILED);
      }
    }

    public ActionResult CreateVerifyCode()
    {
      var verifyCode = CommonHelper.GenerateCaptchaCode(4);
      TempData[SessionKeys.VERIFYCODE] = verifyCode;
      MemoryStream ms = ImageFactory.GenerateImage(verifyCode, 42, 70, 14, 1);
      return File(ms, "image/jpeg");
    }

    [HttpGet]
    public ActionResult EmailPage()
    {
      object model = new object();
      if (TempData[SessionKeys.SESSION_EMAIL] != null && !string.IsNullOrWhiteSpace(TempData[SessionKeys.SESSION_EMAIL].ToString()))
      {
        model = TempData[SessionKeys.SESSION_EMAIL];
      }
      return View(model);
    }

    [HttpGet]
    public ActionResult ChangePwd1_Captcha()
    {
      return View();
    }
    [HttpPost]
    public ActionResult ChangePwd1_Captcha(string email, string captcha)
    {
      if (!userService.IsEmailExist(email))
      {
        return MVCHelper.GetJsonResult(AjaxResultEnum.error, Consts.EMAIL_NOT_REGISTER);
      }
      var sessionVerifyCode = TempData[SessionKeys.VERIFYCODE];
      if (sessionVerifyCode == null || string.IsNullOrEmpty(sessionVerifyCode.ToString()))
      {
        throw new ArgumentException("TempData is Null");
      }
      if (!captcha.Equals(sessionVerifyCode.ToString()))
      {
        return MVCHelper.GetJsonResult(AjaxResultEnum.error, Consts.VERIFY_CODE_ERROR);
      }

      var verifyCode = CommonHelper.GenerateCaptchaCode(kvService.GetIntValue(DBKeys.COM_VERIFYCODE_LENGTH));
      Session[SessionKeys.VERIFYCODE] = verifyCode;
      Session[SessionKeys.CHANGEPWD_EMAIL] = email;
      SendChangePwdEmail(verifyCode, email);
      return MVCHelper.GetJsonResult(AjaxResultEnum.ok);
    }

    [HttpGet]
    public ActionResult ChangePwd2_Email()
    {
      return View(Session[SessionKeys.CHANGEPWD_EMAIL]);
    }
    [HttpPost]
    public ActionResult ChangePwd2_Email(string verifyCode)
    {
      var sessionVerifyCode = Session[SessionKeys.VERIFYCODE];
      if (sessionVerifyCode == null || string.IsNullOrEmpty(sessionVerifyCode.ToString()))
      {
        throw new ArgumentException("Session is Null");
      }
      if (!sessionVerifyCode.ToString().Equals(verifyCode))
      {
        return MVCHelper.GetJsonResult(AjaxResultEnum.error, Consts.VERIFY_CODE_ERROR);
      }

      TempData[SessionKeys.EMAIL_CODE_PASSED] = true;
      return MVCHelper.GetJsonResult(AjaxResultEnum.ok);
    }

    [HttpGet]
    public ActionResult ChangePwd3_Reset()
    {
      if (TempData[SessionKeys.EMAIL_CODE_PASSED] == null)
      {
        return Redirect("/User/ChangePwd2_Email");
      }

      return View(Session[SessionKeys.CHANGEPWD_EMAIL]);
    }
    [HttpPost]
    public ActionResult ChangePwd3_Reset(ChangePwdModel model)
    {
      if (!ModelState.IsValid)
      {
        return MVCHelper.GetJsonResult(AjaxResultEnum.error, MVCHelper.GetValidMsg(ModelState));
      }
      var dto = new UserDTO
      {
        Email = Session[SessionKeys.CHANGEPWD_EMAIL].ToString(),
        Password = model.Password
      };
      userService.Update(dto);

      return MVCHelper.GetJsonResult(AjaxResultEnum.ok);
    }

    [HttpGet]
    public ActionResult ChangePwd4_Succeed()
    {
      Session[SessionKeys.VERIFYCODE] = null;
      Session[SessionKeys.CHANGEPWD_EMAIL] = null;
      return View("Activate", (object)Consts.ACTIVATE_SUCCEED);
    }

    [ChildActionOnly]
    public ActionResult LoginEmail()
    {
      var email = MVCHelper.GetLoginEmailFromSession(HttpContext);
      return PartialView("LoginEmail", email);
    }

    private void SendActivateEmail(long id, string operateCode, string address)
    {
      string template = kvService.GetValue(DBKeys.EMAIL_EMAIL_TEMPLATE_ACTIVATE);
      var body = string.Format(template, id, operateCode);
      SendEmail(address, body, Consts.EMAIL_SUBJECT_ACTIVATE);
    }
    private void SendChangePwdEmail(string verifyCode, string address)
    {
      string template = kvService.GetValue(DBKeys.EMAIL_EMAIL_TEMPLATE_CHANGE_PWD);
      var body = string.Format(template, verifyCode);
      SendEmail(address, body, Consts.EMAIL_SUBJECT_CHANGEPWD);
    }
    private void SendEmail(string address, string body, string subject)
    {
      EmailDTO emailDto = new EmailDTO()
      {
        Addresses = address,
        From = kvService.GetValue(DBKeys.EMAIL_SMTP_EMAIL),
        Subject = subject,
        SmtpPassword = kvService.GetValue(DBKeys.EMAIL_SMTP_PASSWORD),
        SmtpUserName = kvService.GetValue(DBKeys.EMAIL_SMTP_USERNAME),
        SmtpServer = kvService.GetValue(DBKeys.EMAIL_SMTP_SERVER),
        Body = body
      };
      EmailHelper.Send(emailDto);
    }
  }
}