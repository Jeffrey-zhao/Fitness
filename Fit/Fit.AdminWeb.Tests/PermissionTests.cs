using Fit.AdminWeb.Controllers;
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
  class PermissionTests
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
    public PermissionController GetController(  IQueryable<PermissionEntity> entities)
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
