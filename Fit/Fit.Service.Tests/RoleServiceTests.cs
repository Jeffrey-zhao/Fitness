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
  public class RoleServiceTests
  {
    [Test]
    public void Add_NotExist_ReturnId()
    {
      var dto = GetFakeDTO();
      var repository = GetFakeRepository();
      var service = new RoleService(repository);
      repository.Add(Arg.Any<RoleEntity>()).Returns(1);

      var id = service.Add(dto);

      Assert.AreEqual(1, id);
    }
    [Test]
    public void Add_Exist_Throw()
    {
      var dto = GetFakeDTO();
      var entities = GetFakeEntities();
      var repository = GetFakeRepository();
      var service = new RoleService(repository);
      repository.GetAll().Returns(entities);

      Assert.Throws<ArgumentException>(() => service.Add(dto));
    }


    private IRepository<RoleEntity> GetFakeRepository()
    {
      return Substitute.For<IRepository<RoleEntity>>();
    }
    private RoleDTO GetFakeDTO()
    {
      var dto = new RoleDTO
      {
        Id = 1,
        Name = "TestRole",
        Description = "Description"
      };
      return dto;
    }
    private RoleEntity GetFakeEntity()
    {
      var entity = new RoleEntity
      {
        ID = 1,
        Name = "RoleEntity",
        Description = "RoleDescription"
      };
      return entity;
    }
    private IQueryable<RoleEntity> GetFakeEntities()
    {
      var entity1 = new RoleEntity
      {
        ID = 1,
        Name = "RoleEntity1",
        Description = "RoleDescription1"
      };
      var entity2 = new RoleEntity
      {
        ID = 2,
        Name = "RoleEntity2",
        Description = "RoleDescription2"
      };

      return new List<RoleEntity> { entity1, entity2 }.AsQueryable();
    }
  }
}
