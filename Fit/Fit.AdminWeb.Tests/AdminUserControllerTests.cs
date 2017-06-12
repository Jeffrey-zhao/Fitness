using Fit.AdminWeb.Controllers;
using Fit.AdminWeb.Models;
using Fit.Common;
using Fit.DTO.RBAC;
using Fit.IService;
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
    public void LoginPost_VerifyCodeError_ReturnErrorJson()
    {
      var loginModel = new LoginModel
      {
        Email = "abc",
        Password = "1",
        VerifyCode = "VerifyCode"
      };
      var auController = GetController();
      auController.TempData[Consts.VERIFY_CODE_KEY] = "VerifyCode_Error";
      var actionResult = auController.Login(loginModel) as JsonResult;

      Assert.AreEqual("Verify Code Error", (actionResult.Data as AjaxResult).Msg);
    }

    [Test]
    public void CreateVerifyCode_Normal_ReturnCaptchaImg()
    {
      var auController = GetController();
      var result = auController.CreateVerifyCode() as FileResult;

      Assert.IsTrue(auController.TempData[Consts.VERIFY_CODE_KEY].ToString().Length == 4);
      Assert.AreEqual("image/jpeg", result.ContentType);
    }

    [Test]
    public void List_PageExist_ReturnData()
    {
      int count = 4;
      var entities = GetFakeAuEntities(count);
      var auController = GetController(null, entities);

      var result = auController.List() as ViewResult;

      Assert.IsTrue((result.Model as AdminUserDTO[]).Length == count);
      Assert.AreEqual(count, result.ViewBag.TotalCount);
      Assert.AreEqual(1, result.ViewBag.PageIndex);
    }
    [Test]
    public void List_PageNotExist_NotReturnData()
    {
      int count = 4;
      var entities = GetFakeAuEntities(count);
      var auController = GetController(null, entities);

      var result = auController.List(2) as ViewResult;

      Assert.IsTrue((result.Model as AdminUserDTO[]).Length == 0);
      Assert.AreEqual(count, result.ViewBag.TotalCount);
      Assert.AreEqual(2, result.ViewBag.PageIndex);
    }

    [Test]
    public void EditGet_Normal_ReturnAdminWithRole()
    {
      int roleCount = 2;
      var admin = GetFakeAdminEntity();
      var roles = GetFakeRoleEntities(roleCount);
      var controller = GetController(roles, null, admin);

      var result = controller.Edit(1) as ViewResult;
      var model = result.Model as AdminUserEditViewModel;

      Assert.AreEqual(admin.ID, model.ID);
      Assert.AreEqual(admin.Email, model.Email);
      Assert.AreEqual(roleCount, model.AllRoles.Length);
    }
    [Test]
    public void EditPost_NotEditPwd_PwdNotChanged()
    {
      var adminRep = GetFakeAdminRep();
      var roleRep = GetFakeRoleRep();
      var auService = new AdminUserService(adminRep);
      var roleService = new RoleService(roleRep, adminRep);
      var auController = new AdminUserController(roleService, auService);
      var entity = new AdminUserEntity
      {
        ID = 1,
        Name = "Entity_Name",
        Email = "Entity_Email",
        PhoneNum = "Entity_PhoneNum",
        PasswordHash = "123"
      };
      adminRep.GetById(Arg.Any<long>()).Returns(entity);
      var model = new AdminUserEditModel()
      {
        ID = 1,
        Name = "Model_Name",
        Email = "Model_Email",
        PhoneNum = "Model_PhoneNum"
      };

      auController.Edit(model);
      var updatedEntity = new AdminUserEntity
      {
        ID = 1,
        Name = "Model_Name",
        Email = "Model_Email",
        PhoneNum = "Model_PhoneNum",
        PasswordHash = "123"
      };
      adminRep.Received().Update(updatedEntity);
    }
    [Test]
    public void EditPost_EditPwd_PwdChanged()
    {
      var adminRep = GetFakeAdminRep();
      var roleRep = GetFakeRoleRep();
      var auService = new AdminUserService(adminRep);
      var roleService = new RoleService(roleRep, adminRep);
      var auController = new AdminUserController(roleService, auService);
      var entity = new AdminUserEntity
      {
        ID = 1,
        Name = "Entity_Name",
        Email = "Entity_Email",
        PhoneNum = "Entity_PhoneNum",
        PasswordHash = "123"
      };
      adminRep.GetById(Arg.Any<long>()).Returns(entity);
      var model = new AdminUserEditModel()
      {
        ID = 1,
        Name = "Model_Name",
        Email = "Model_Email",
        PhoneNum = "Model_PhoneNum",
        Password = "222"
      };

      auController.Edit(model);
      var updatedEntity = new AdminUserEntity
      {
        ID = 1,
        Name = "Model_Name",
        Email = "Model_Email",
        PhoneNum = "Model_PhoneNum",
        PasswordHash = CommonHelper.CalcMD5(model.Password)
      };
      adminRep.Received().Update(updatedEntity);
    }

    [Test]
    public void AddGet_Nomal_ReturnRoles()
    {
      int roleCount = 2;
      var admin = GetFakeAdminEntity();
      var roles = GetFakeRoleEntities(roleCount);
      var controller = GetController(roles, null, null);

      var result = controller.Add() as ViewResult;
      var model = result.Model as RoleDTO[];

      Assert.AreEqual(roleCount, model.Length);
    }
    [Test]
    public void AddPost_Nomal_ReturnJson()
    {
      var adminRep = GetFakeAdminRep();
      var auService = new AdminUserService(adminRep);
      var roleService = Substitute.For<IRoleService>();
      var auController = new AdminUserController(roleService, auService);
      adminRep.Add(Arg.Any<AdminUserEntity>()).Returns(1);
      var arr = new List<long>() { 1, 2 }.ToArray();
      var model = new AdminUserAddModel
      {
        RoleIDs = arr
      };

      var result = auController.Add(model) as JsonResult;

      roleService.Received().EditAdminRole(1, arr);
      Assert.AreEqual("ok", (result.Data as AjaxResult).Status);
    }

    [Test]
    public void Delete_Normal_ReturnJson()
    {
      var adminRep = GetFakeAdminRep();
      var roleRep = GetFakeRoleRep();
      var auService = new AdminUserService(adminRep);
      var roleService = new RoleService(roleRep, adminRep);
      var auController = new AdminUserController(roleService, auService);

      var result= auController.Delete(1)as JsonResult;

      adminRep.Received().DeleteById(1);
      Assert.AreEqual("ok",(result.Data as AjaxResult).Status);
    }

    [Test]
    public void LoginEmail_SessionHasEmail_ReturnEmail()
    {
      var adminRep = GetFakeAdminRep();
      var roleRep = GetFakeRoleRep();
      var auService = new AdminUserService(adminRep);
      var roleService = new RoleService(roleRep, adminRep);
      var auController = new AdminUserController(roleService, auService);
      var context = Substitute.For<ControllerContext>();
      context.HttpContext.Session[Consts.LOGIN_EMAIL].Returns("email");
      auController.ControllerContext = context;

      var result = auController.LoginEmail() as PartialViewResult;

      Assert.AreEqual("email", result.Model.ToString());
    }

    //[Test]
    //public void Index_Test()
    //{
    //  var auController = GetController();

    //  var httpContext = Substitute.For<HttpContextBase>();
    //  var httpRequest = Substitute.For<HttpRequestBase>();
    //  NameValueCollection queryString = new NameValueCollection();
    //  queryString.Add("testMsg", "testMsg");
    //  httpRequest.QueryString.Returns(queryString);
    //  httpContext.Request.Returns(httpRequest);
    //  ControllerContext controllerContext = new ControllerContext();
    //  controllerContext.HttpContext = httpContext;
    //  auController.ControllerContext = controllerContext;
    //  var result = auController.Index() as ViewResult;
    //  var viewBag = result.ViewBag;

    //  Assert.AreEqual("testMsg", viewBag.Message);
    //}


    public AdminUserEntity GetFakeAdminEntity(int id = 1, string email = "")
    {
      var entity = new AdminUserEntity
      {
        ID = id,
        Email = email
      };
      return entity;
    }
    public IQueryable<AdminUserEntity> GetFakeAuEntities(int num)
    {
      var list = new List<AdminUserEntity>();
      for (int i = 1; i <= num; i++)
      {
        list.Add(GetFakeAdminEntity(i));
      }
      return list.AsQueryable();
    }
    public RoleEntity GetFakeRoleEntity(int id = 1, string name = "")
    {
      var entity = new RoleEntity
      {
        ID = id,
        Name = name
      };
      return entity;
    }
    public IQueryable<RoleEntity> GetFakeRoleEntities(int num)
    {
      var list = new List<RoleEntity>();
      for (int i = 1; i <= num; i++)
      {
        list.Add(GetFakeRoleEntity(i));
      }
      return list.AsQueryable();
    }

    public IRepository<AdminUserEntity> GetFakeAdminRep()
    {
      return Substitute.For<IRepository<AdminUserEntity>>();
    }
    public IRepository<RoleEntity> GetFakeRoleRep()
    {
      return Substitute.For<IRepository<RoleEntity>>();
    }
    public AdminUserController GetController(IQueryable<RoleEntity> roleEntitiesForGetAll = null
      , IQueryable<AdminUserEntity> auEntitiesForGetAll = null
      , AdminUserEntity auEntityForGetById = null)
    {
      var adminRep = GetFakeAdminRep();
      if (auEntitiesForGetAll != null)
      {
        adminRep.GetAll().Returns(auEntitiesForGetAll);
      }
      if (auEntityForGetById != null)
      {
        adminRep.GetById(Arg.Any<long>()).Returns(auEntityForGetById);
      }
      var roleRep = GetFakeRoleRep();
      if (roleEntitiesForGetAll != null)
      {
        roleRep.GetAll().Returns(roleEntitiesForGetAll);
      }
      var auService = new AdminUserService(adminRep);
      var roleService = new RoleService(roleRep, adminRep);
      return new AdminUserController(roleService, auService);
    }
  }
}
