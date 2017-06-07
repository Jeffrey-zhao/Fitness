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
  public class PermissionServiceTests
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

    [Test]
    public void GetById_IdExist_ReturnById()
    {
      var repository = GetRepository();
      var service = new PermissionService(repository);
      repository.GetById(Arg.Any<long>()).Returns(GetFakeEntity());

      var dto = service.GetById(1);

      Assert.AreEqual(1, dto.Id);
    }
    [Test]
    public void GetById_IdNotExist_Throw()
    {
      var repository = GetRepository();
      var service = new PermissionService(repository);

      Assert.Throws<ArgumentException>(() => service.GetById(1));
    }

    [Test]
    public void GetPagedData_AllInOnePage_ReturnAllData()
    {
      var repository = GetRepository();
      repository.GetAll().Returns(GetFakeEntities());
      var service = new PermissionService(repository);

      var dtos = service.GetPagedData(0, 5);

      Assert.AreEqual(2, dtos.Length);
    }
    [Test]
    public void GetPagedData_NotAllInOnePage_ReturnPagedData()
    {
      var repository = GetRepository();
      repository.GetAll().Returns(GetFakeEntities());
      var service = new PermissionService(repository);

      var dtos = service.GetPagedData(0, 1);

      Assert.AreEqual(1, dtos.Length);
    }

    [Test]
    public void GetTotalCount_ReturnCount()
    {
      var repository = GetRepository();
      repository.GetAll().Returns(GetFakeEntities());
      var service = new PermissionService(repository);

      var count = service.GetTotalCount();

      Assert.AreEqual(2, count);
    }
    [Test]
    public void Update_IdExist_Updated()
    {
      var dto = GetFakeDTO();
      var repository = GetRepository();
      var entity = GetFakeEntity();
      var service = new PermissionService(repository);
      repository.GetById(Arg.Any<long>()).Returns(entity);

      service.Update(dto);

      entity.Name = "UpdateName";
      entity.Description = "UpdateDescription";

      repository.Received().Update(entity);
    }

    private IQueryable<PermissionEntity> GetFakeEntities()
    {
      var entity1 = new PermissionEntity
      {
        ID = 1,
        Name = "E1",
        IsDeleted = false
      };
      var entity2 = new PermissionEntity
      {
        ID = 2,
        Name = "E2",
        IsDeleted = false
      };
      var entities = new List<PermissionEntity>
      {
        entity1,entity2
      }.AsQueryable();

      return entities;
    }
    private PermissionEntity GetFakeEntity()
    {
      var entity1 = new PermissionEntity
      {
        ID = 1,
        Name = "E1",
        IsDeleted = false
      };
      return entity1;
    }
    private PermissionDTO GetFakeDTO()
    {
      var dto = new PermissionDTO
      {
        Id = 1,
        Name = "UpdateName",
        Description = "UpdateDescription"
      };
      return dto;
    }
    private IRepository<PermissionEntity> GetRepository()
    {
      return Substitute.For<IRepository<PermissionEntity>>();
    }

  }
}
