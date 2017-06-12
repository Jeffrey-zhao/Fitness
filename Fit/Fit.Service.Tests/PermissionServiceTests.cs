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

namespace Fit.Service.Tests
{
  [TestFixture]
  public class PermissionServiceTests : IPermissionService
  {
    public PermissionDTO[] GetPagedData(int startIndex, int pageSize)
    {
      throw new NotImplementedException();
    }
    [Test]
    public void GetPagedData_AllInOnePage_ReturnAllData()
    {
      var service = GetService();
      var dtos = service.GetPagedData(0, 5);

      Assert.AreEqual(2, dtos.Length);
    }
    [Test]
    public void GetPagedData_NotAllInOnePage_ReturnPagedData()
    {
      var service = GetService();
      var dtos = service.GetPagedData(0, 1);

      Assert.AreEqual(1, dtos.Length);
    }


    public long GetTotalCount()
    {
      throw new NotImplementedException();
    }
    [Test]
    public void GetTotalCount_ReturnCount()
    {
      var service = GetService();
      var count = service.GetTotalCount();

      Assert.AreEqual(2, count);
    }


    public PermissionDTO GetById(long id)
    {
      throw new NotImplementedException();
    }
    [Test]
    public void GetById_IdExist_ReturnById()
    {
      var service = GetService();
      var dto = service.GetById(1);

      Assert.AreEqual(1, dto.Id);
    }
    [Test]
    public void GetById_IdNotExist_Throw()
    {
      var permissionRepository = GetFakePermissionRepository();
      var roleRepository = GetFakeRoleRepository();
      permissionRepository.GetAll().Returns(GetFakeEntities());
      var service = new PermissionService(permissionRepository, roleRepository);
      Assert.Throws<ArgumentException>(() => service.GetById(1));
    }


    public PermissionDTO[] GetAll()
    {
      throw new NotImplementedException();
    }
    [Test]
    public void GetAll_ReturnAll()
    {
      var service = GetService();
      var result = service.GetAll();

      Assert.IsTrue(result.Length > 0);
    }


    public long[] GetIDsByRole(long id)
    {
      throw new NotImplementedException();
    }
    [Test]
    [Ignore("Not tested")]
    public void GetIDsByRole() { }

    public long Add(PermissionDTO dto)
    {
      throw new NotImplementedException();
    }
    [Test]
    public void Add_Normal_ReturnId()
    {
      var dto = new PermissionDTO
      {
        Id = 2
      };
      var permissionRepository = GetFakePermissionRepository();
      var roleRepository = GetFakeRoleRepository();
      permissionRepository.Add(Arg.Any<PermissionEntity>()).Returns(dto.Id);
      var service = new PermissionService(permissionRepository, roleRepository);

      var id = service.Add(dto);

      Assert.AreEqual(dto.Id, id);
    }
    [Test]
    public void Add_Exist_Throw()
    {
      var entities = new List<PermissionEntity>() { new PermissionEntity() }.AsQueryable();
      var permissionRepository = GetFakePermissionRepository();
      var roleRepository = GetFakeRoleRepository();
      permissionRepository.GetAll().Returns(entities);
      var service = new PermissionService(permissionRepository, roleRepository);

      Assert.Throws<ArgumentException>(() => service.Add(new PermissionDTO()));
    }


    public void Update(PermissionDTO dto)
    {
      throw new NotImplementedException();
    }
    [Test]
    public void Update_IdExist_Updated()
    {
      var dto = GetFakeDTO();
      var permissionRepository = GetFakePermissionRepository();
      var roleRepository = GetFakeRoleRepository();
      var entity = GetFakeEntity();
      var service = new PermissionService(permissionRepository, roleRepository);
      permissionRepository.GetById(Arg.Any<long>()).Returns(entity);

      service.Update(dto);

      entity.Name = "UpdateName";
      entity.Description = "UpdateDescription";

      permissionRepository.Received().Update(entity);
    }


    public void Delete(long id)
    {
      throw new NotImplementedException();
    }
    [Test]
    public void Delete_IdExist_MarkDelete()
    {
      var permissionRepository = GetFakePermissionRepository();
      var roleRepository = GetFakeRoleRepository();
      var service = new PermissionService(permissionRepository, roleRepository);

      service.Delete(2);

      permissionRepository.Received().DeleteById(2);
    }


    public void EditRolePermission(long roleId, long[] permissionIDs)
    {
      throw new NotImplementedException();
    }
    [Test]
    [Ignore("Not tested")]
    public void EditRolePermission() { }

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
    private IRepository<PermissionEntity> GetFakePermissionRepository()
    {
      return Substitute.For<IRepository<PermissionEntity>>();
    }
    private IRepository<RoleEntity> GetFakeRoleRepository()
    {
      return Substitute.For<IRepository<RoleEntity>>();
    }
    private PermissionService GetService()
    {
      var permissionRepository = GetFakePermissionRepository();
      var roleRepository = GetFakeRoleRepository();
      permissionRepository.GetAll().Returns(GetFakeEntities());
      permissionRepository.GetById(Arg.Any<long>()).Returns(GetFakeEntity());
      return new PermissionService(permissionRepository, roleRepository);
    }
  }
}
