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
      dto.Name = "e";
      var entity = GetFakeEntity();
      entity.Name = "e";
      var entities = new List<RoleEntity> { entity }.AsQueryable();

      var repository = GetFakeRepository();
      var service = new RoleService(repository);
      repository.GetAll().Returns(entities);

      Assert.Throws<ArgumentException>(() => service.Add(dto));
    }

    [Test]
    public void Delete()
    {
      var repository = GetFakeRepository();
      var service = new RoleService(repository);
      service.Delete(1);

      repository.Received().DeleteById(1);
    }

    [Test]
    public void GetById_IdExist_ReturnDTO()
    {
      var entity = GetFakeEntity();
      var repository = GetFakeRepository();
      var service = new RoleService(repository);
      repository.GetById(Arg.Any<long>()).Returns(entity);

      var dto = service.GetById(1);

      Assert.AreEqual(entity.ID, dto.Id, "id");
      Assert.AreEqual(entity.Name, dto.Name, "Name");
      Assert.AreEqual(entity.Description, dto.Description, "Description");
    }
    [Test]
    public void GetById_IdNotExist_Throw()
    {
      var repository = GetFakeRepository();
      var service = new RoleService(repository);

      Assert.Throws<ArgumentException>(() => service.GetById(1));
    }

    [Test]
    public void GetPagedData_InOnePage_ReturnAll()
    {
      var entities = GetFakeEntities(5);
      var repository = GetFakeRepository();
      var service = new RoleService(repository);
      repository.GetAll().Returns(entities);

      var pagedData = service.GetPagedData(0, 10);

      Assert.AreEqual(5, pagedData.Count());
    }
    [Test]
    public void GetPagedData_NotInOnePage_ReturnPaged()
    {
      var entities = GetFakeEntities(5);
      var repository = GetFakeRepository();
      var service = new RoleService(repository);
      repository.GetAll().Returns(entities);

      var pagedData = service.GetPagedData(0, 3);

      Assert.AreEqual(3, pagedData.Count());
    }

    [Test]
    public void GetTotalCount_HaveData_ReturnCount()
    {
      var entities = GetFakeEntities(5);
      var repository = GetFakeRepository();
      var service = new RoleService(repository);
      repository.GetAll().Returns(entities);
      var count = service.GetTotalCount();

      Assert.AreEqual(5, count);
    }
    [Test]
    public void GetTotalCount_NoData_ReturnCount()
    {
      var repository = GetFakeRepository();
      var service = new RoleService(repository);
      var count = service.GetTotalCount();

      Assert.AreEqual(0, count);
    }

    [Test]
    public void Update_Test_EntityReceived()
    {
      var entity = GetFakeEntity();
      var dto = new RoleDTO
      {
        Id=entity.ID,
        Name=entity.Name,
        Description=entity.Description
      };

      var repository = GetFakeRepository();
      var service = new RoleService(repository);
      var count = service.GetTotalCount();

      service.Update(dto);

      repository.Received().Update(entity);
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
    private IQueryable<RoleEntity> GetFakeEntities(int count = 3)
    {
      var list = new List<RoleEntity>();
      for (int i = 1; i <= count; i++)
      {
        var entity = new RoleEntity
        {
          ID = i,
          Name = "RoleEntity" + i,
          Description = "RoleDescription" + i
        };
        list.Add(entity);
      }

      return list.AsQueryable();
    }
  }
}
