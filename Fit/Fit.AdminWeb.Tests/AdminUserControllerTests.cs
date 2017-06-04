using Fit.AdminWeb.Controllers;
using Fit.AdminWeb.Models;
using Fit.Common;
using Fit.Service.Entities.RBAC;
using Fit.Service.Repository;
using Fit.Service.Services.RBAC;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Fit.AdminWeb.Tests
{
  [TestFixture]
  public class AdminUserControllerTests
  {
    [Test]
    public void Login_VerifyCodeError_ReturnErrorJson()
    {
      var loginModel = new LoginModel
      {
        Email = "abc",
        Password = "1",
        VerifyCode = "VerifyCode"
      };
      var repository = Substitute.For<IRepository<AdminUserEntity>>();
      var auService = new AdminUserService(repository);
      var auController = new AdminUserController(auService);
      auController.TempData[Consts.VERIFY_CODE_KEY] = "VerifyCode_Error";
      var actionResult = auController.Login(loginModel) as JsonResult;

      Assert.AreEqual("Verify Code Error", (actionResult.Data as AjaxResult).Msg);
    }

    [Test]
    public void Index_Test()
    {
      var repository = Substitute.For<IRepository<AdminUserEntity>>();
      var auService = new AdminUserService(repository);
      var auController = new AdminUserController(auService);

      var httpContext = Substitute.For<HttpContextBase>();
      var httpRequest = Substitute.For<HttpRequestBase>();
      NameValueCollection queryString = new NameValueCollection();
      queryString.Add("testMsg", "testMsg");
      httpRequest.QueryString.Returns(queryString);
      httpContext.Request.Returns(httpRequest);
      ControllerContext controllerContext = new ControllerContext();
      controllerContext.HttpContext = httpContext;
      auController.ControllerContext = controllerContext;
      var result = auController.Index() as ViewResult;
      var viewBag = result.ViewBag;

      Assert.AreEqual("testMsg", viewBag.Message);
    }
  }
}
