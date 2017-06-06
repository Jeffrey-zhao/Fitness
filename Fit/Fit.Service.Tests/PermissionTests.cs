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

namespace Fit.Service.Tests
{
  [TestFixture]
  public class PermissionTests
  {
    [Test]
    public void Add_Normal_ReturnId()
    {
      var dto = new PermissionDTO
      {
        Id = 2
      };
      var repository = Substitute.For<IRepository<PermissionEntity>>();
      repository.Add(Arg.Any<PermissionEntity>()).Returns(dto.Id);
      var service = new PermissionService(repository);

      var id = service.Add(dto);

      Assert.AreEqual(dto.Id, id);
    }
    [Test]
    public void Add_Exist_Throw()
    {
      var entities = new List<PermissionEntity>() { new PermissionEntity() }.AsQueryable();
      var repository = Substitute.For<IRepository<PermissionEntity>>();
      repository.GetAll().Returns(entities);
      var service = new PermissionService(repository);

      Assert.Throws<ArgumentException>(() => service.Add(new PermissionDTO()));
    }

    [Test]
    public void Delete_IdExist_MarkDelete()
    {
      var repository = Substitute.For<IRepository<PermissionEntity>>();
      var service = new PermissionService(repository);

      service.Delete(2);

      repository.Received().DeleteById(2);
    }
  }
}
