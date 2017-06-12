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
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Fit.AdminWeb.Tests
{
  [TestFixture]
  class RoleControllerTests
  {
    [Test]
    public void List_PageExist_ReturnData()
    {
      int count = 4;
      var entities = GetFakeRoleEntities(count);
      var roleController = GetController(entities);

      var result = roleController.List() as ViewResult;

      Assert.IsTrue((result.Model as RoleDTO[]).Length == count);
      Assert.AreEqual(count, result.ViewBag.TotalCount);
      Assert.AreEqual(1, result.ViewBag.PageIndex);
    }
    [Test]
    public void List_PageNotExist_NotReturnData()
    {
      int count = 4;
      var entities = GetFakeRoleEntities(count);
      var roleController = GetController(entities);

      var result = roleController.List(2) as ViewResult;

      Assert.IsTrue((result.Model as RoleDTO[]).Length == 0);
      Assert.AreEqual(count, result.ViewBag.TotalCount);
      Assert.AreEqual(2, result.ViewBag.PageIndex);
    }

    [Test]
    public void AddGet_Nomal_ReturnPermissions()
    {
      var permissionCount = 3;
      var permissionRep = GetFakePermissionRep();
      var roleRep = GetFakeRoleRep();
      var adminRep = GetFakeAdminRep();
      var permissionService = new PermissionService(permissionRep, roleRep);
      var roleService = new RoleService(roleRep, adminRep);
      var controller = new RoleController(permissionService, roleService);
      var permissionEntities = GetFakePermissionEntities(permissionCount);
      permissionRep.GetAll().Returns(permissionEntities);

      var result = controller.Add() as ViewResult;
      var model = result.Model as PermissionDTO[];

      Assert.AreEqual(permissionCount, model.Length);
    }
    [Test]
    public void AddPost_Nomal_ReturnJson()
    {
      var adminRep = GetFakeAdminRep();
      var roleRep = GetFakeRoleRep();
      var roleService = new RoleService(roleRep, adminRep);
      var permissionService = Substitute.For<IPermissionService>();
      var roleController = new RoleController(permissionService, roleService);
      roleRep.Add(Arg.Any<RoleEntity>()).Returns(1);
      var arr = new List<long>() { 1, 2 }.ToArray();
      var model = new RoleModel
      {
        PermissionIDs = arr
      };

      var result = roleController.Add(model) as JsonResult;

      permissionService.Received().EditRolePermission(1, arr);
      Assert.AreEqual("ok", (result.Data as AjaxResult).Status);
    }

    [Test]
    public void EditGet_IdExist_ReturnRoleWithPermission()
    {
      var permissionCount = 3;
      var adminRep = GetFakeAdminRep();
      var roleRep = GetFakeRoleRep();
      var permissionRep = GetFakePermissionRep();
      var roleService = new RoleService(roleRep, adminRep);
      var permissionService = new PermissionService(permissionRep, roleRep);
      var roleController = new RoleController(permissionService, roleService);
      var role = GetFakeRoleEntity(2);
      roleRep.GetById(Arg.Any<long>()).Returns(role);
      permissionRep.GetAll().Returns(GetFakePermissionEntities(permissionCount));

      var result = roleController.Edit(2) as ViewResult;
      var model = result.Model as RoleModel;

      Assert.AreEqual(role.ID, model.ID);
      Assert.AreEqual(role.Name, model.Name);
      Assert.AreEqual(permissionCount, model.AllPermissions.Length);
    }
    [Test]
    public void EditGet_IdNotExist_ReturnRoleWithPermission()
    {
      var adminRep = GetFakeAdminRep();
      var roleRep = GetFakeRoleRep();
      var permissionRep = GetFakePermissionRep();
      var roleService = new RoleService(roleRep, adminRep);
      var permissionService = new PermissionService(permissionRep, roleRep);
      var roleController = new RoleController(permissionService, roleService);

      Assert.Throws<ArgumentException>(() => roleController.Edit(2));
    }
    
    [Test]
    public void Delete_Normal_ReturnJson()
    {
      var adminRep = GetFakeAdminRep();
      var roleRep = GetFakeRoleRep();
      var permissionRep = GetFakePermissionRep();
      var roleService = new RoleService(roleRep, adminRep);
      var permissionService = new PermissionService(permissionRep, roleRep);
      var roleController = new RoleController(permissionService, roleService);

      var result = roleController.Delete(1) as JsonResult;

      roleRep.Received().DeleteById(1);
      Assert.AreEqual("ok", (result.Data as AjaxResult).Status);
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
    public IRepository<PermissionEntity> GetFakePermissionRep()
    {
      return Substitute.For<IRepository<PermissionEntity>>();
    }
    public RoleController GetController(IQueryable<RoleEntity> roleEntitiesForGetAll)
    {
      var permissionRep = GetFakePermissionRep();
      var roleRep = GetFakeRoleRep();
      if (roleEntitiesForGetAll != null)
      {
        roleRep.GetAll().Returns(roleEntitiesForGetAll);
      }
      var adminRep = GetFakeAdminRep();

      var permissionService = new PermissionService(permissionRep, roleRep);
      var roleService = new RoleService(roleRep, adminRep);
      return new RoleController(permissionService, roleService);
    }
  }
}
