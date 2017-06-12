using Fit.AdminWeb.Controllers;
using Fit.AdminWeb.Models;
using Fit.Common;
using Fit.DTO.RBAC;
using Fit.Service.Entities.RBAC;
using Fit.Service.Repository;
using Fit.Service.Services.RBAC;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Fit.AdminWeb.Tests
{
  [TestFixture]
  class PermissionControllerTests
  {
    [Test]
    public void List_PageExist_ReturnData()
    {
      int count = 4;
      var entities = GetFakePermissionEntities(count);
      var permissionController = GetController(entities);

      var result = permissionController.List() as ViewResult;

      Assert.IsTrue((result.Model as PermissionDTO[]).Length == count);
      Assert.AreEqual(count, result.ViewBag.TotalCount);
      Assert.AreEqual(1, result.ViewBag.PageIndex);
    }
    [Test]
    public void List_PageNotExist_NotReturnData()
    {
      int count = 4;
      var entities = GetFakePermissionEntities(count);
      var permissionController = GetController(entities);

      var result = permissionController.List(2) as ViewResult;

      Assert.IsTrue((result.Model as PermissionDTO[]).Length == 0);
      Assert.AreEqual(count, result.ViewBag.TotalCount);
      Assert.AreEqual(2, result.ViewBag.PageIndex);
    }

    [Test]
    public void AddPost_Normal_ReturnJson()
    {
      var permissionController = GetController();

      var result = permissionController.Add(new PermissionModel()) as JsonResult;

      Assert.AreEqual("ok", (result.Data as AjaxResult).Status);
    }
    [Test]
    public void EditGet_IdExist_ReturnData()
    {
      var permissionRep = GetFakePermissionRep();
      var entity = GetFakePermissionEntity();
      permissionRep.GetById(Arg.Any<long>()).Returns(entity);
      var service = new PermissionService(permissionRep, GetFakeRoleRep());
      var controller = new PermissionController(service);

      var result = controller.Edit(1) as ViewResult;

      Assert.IsTrue((result.Model as PermissionDTO) != null);
    }
    [Test]
    public void EditGet_IdNotExist_ReturnNull()
    {
      var permissionRep = GetFakePermissionRep();
      var entity = GetFakePermissionEntity();
      var service = new PermissionService(permissionRep, GetFakeRoleRep());
      var controller = new PermissionController(service);

      Assert.Throws<ArgumentException>(() => controller.Edit(1));
    }

    [Test]
    public void Delete_Normal()
    {
      var permissionRep = GetFakePermissionRep();
      var entity = GetFakePermissionEntity();
      var service = new PermissionService(permissionRep, GetFakeRoleRep());
      var controller = new PermissionController(service);

      controller.Delete(1);

      permissionRep.Received().DeleteById(1);
    }

    public PermissionEntity GetFakePermissionEntity(int id = 1)
    {
      var entity = new PermissionEntity
      {
        ID = id
      };
      return entity;
    }
    public IQueryable<PermissionEntity> GetFakePermissionEntities(int num)
    {
      var list = new List<PermissionEntity>();
      for (int i = 1; i <= num; i++)
      {
        list.Add(GetFakePermissionEntity(i));
      }
      return list.AsQueryable();
    }
    public IRepository<PermissionEntity> GetFakePermissionRep()
    {
      return Substitute.For<IRepository<PermissionEntity>>();
    }
    public IRepository<RoleEntity> GetFakeRoleRep()
    {
      return Substitute.For<IRepository<RoleEntity>>();
    }
    public PermissionController GetController(IQueryable<PermissionEntity> entities = null)
    {
      var permissionRep = GetFakePermissionRep();
      var service = new PermissionService(permissionRep, GetFakeRoleRep());
      if (entities != null)
      {
        permissionRep.GetAll().Returns(entities);
      }
      return new PermissionController(service);
    }
  }
}
