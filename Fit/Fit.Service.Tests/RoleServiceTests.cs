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
  public class RoleServiceTests : IRoleService
  {
    public RoleDTO[] GetAll()
    {
      throw new NotImplementedException();
    }
    [Test]
    public void GetAll_ReturnAll()
    {
      var roleRepository = GetFakeRoleRepository();
      var adminRepository = GetFakeAdminRepository();
      var service = new RoleService(roleRepository, adminRepository);
      roleRepository.GetAll().Returns(GetFakeEntities());
      var result = service.GetAll();

      Assert.IsTrue(result.Length>0);
    }

    public long[] GetIDsByAdmin(long adminID)
    {
      throw new NotImplementedException();
    }
    [Test]
    [Ignore("Not tested")]
    public void GetIDsByAdmin() { }
    public void EditAdminRole(long adminId, long[] roleIDs)
    {
      throw new NotImplementedException();
    }
    [Test]
    [Ignore("Not tested")]
    public void EditAdminRole() { }

    public long Add(RoleDTO dto)
    {
      throw new NotImplementedException();
    }
    [Test]
    public void Add_NotExist_ReturnId()
    {
      var dto = GetFakeDTO();
      var roleRepository = GetFakeRoleRepository();
      var adminRepository = GetFakeAdminRepository();
      var service = new RoleService(roleRepository, adminRepository);
      roleRepository.Add(Arg.Any<RoleEntity>()).Returns(1);

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

      var roleRepository = GetFakeRoleRepository();
      var adminRepository = GetFakeAdminRepository();
      var service = new RoleService(roleRepository, adminRepository);
      roleRepository.GetAll().Returns(entities);

      Assert.Throws<ArgumentException>(() => service.Add(dto));
    }


    public void Delete(long id)
    {
      throw new NotImplementedException();
    }
    [Test]
    public void Delete()
    {
      var roleRepository = GetFakeRoleRepository();
      var adminRepository = GetFakeAdminRepository();
      var service = new RoleService(roleRepository, adminRepository);
      service.Delete(1);

      roleRepository.Received().DeleteById(1);
    }

    public RoleDTO GetById(long id)
    {
      throw new NotImplementedException();
    }
    [Test]
    public void GetById_IdExist_ReturnDTO()
    {
      var entity = GetFakeEntity();
      var roleRepository = GetFakeRoleRepository();
      var adminRepository = GetFakeAdminRepository();
      var service = new RoleService(roleRepository, adminRepository);
      roleRepository.GetById(Arg.Any<long>()).Returns(entity);

      var dto = service.GetById(1);

      Assert.AreEqual(entity.ID, dto.Id, "id");
      Assert.AreEqual(entity.Name, dto.Name, "Name");
      Assert.AreEqual(entity.Description, dto.Description, "Description");
    }
    [Test]
    public void GetById_IdNotExist_Throw()
    {
      var roleRepository = GetFakeRoleRepository();
      var adminRepository = GetFakeAdminRepository();
      var service = new RoleService(roleRepository, adminRepository);

      Assert.Throws<ArgumentException>(() => service.GetById(1));
    }


    public RoleDTO[] GetPagedData(int startIndex, int pageSize)
    {
      throw new NotImplementedException();
    }
    [Test]
    public void GetPagedData_InOnePage_ReturnAll()
    {
      var entities = GetFakeEntities(5);
      var roleRepository = GetFakeRoleRepository();
      var adminRepository = GetFakeAdminRepository();
      var service = new RoleService(roleRepository, adminRepository);
      roleRepository.GetAll().Returns(entities);

      var pagedData = service.GetPagedData(0, 10);

      Assert.AreEqual(5, pagedData.Count());
    }
    [Test]
    public void GetPagedData_NotInOnePage_ReturnPaged()
    {
      var entities = GetFakeEntities(5);
      var roleRepository = GetFakeRoleRepository();
      var adminRepository = GetFakeAdminRepository();
      var service = new RoleService(roleRepository, adminRepository);
      roleRepository.GetAll().Returns(entities);

      var pagedData = service.GetPagedData(0, 3);

      Assert.AreEqual(3, pagedData.Count());
    }


    public long GetTotalCount()
    {
      throw new NotImplementedException();
    }
    [Test]
    public void GetTotalCount_HaveData_ReturnCount()
    {
      var entities = GetFakeEntities(5);
      var roleRepository = GetFakeRoleRepository();
      var adminRepository = GetFakeAdminRepository();
      var service = new RoleService(roleRepository, adminRepository);
      roleRepository.GetAll().Returns(entities);
      var count = service.GetTotalCount();

      Assert.AreEqual(5, count);
    }
    [Test]
    public void GetTotalCount_NoData_ReturnCount()
    {
      var roleRepository = GetFakeRoleRepository();
      var adminRepository = GetFakeAdminRepository();
      var service = new RoleService(roleRepository, adminRepository);
      var count = service.GetTotalCount();

      Assert.AreEqual(0, count);
    }


    public void Update(RoleDTO dto)
    {
      throw new NotImplementedException();
    }
    [Test]
    public void Update_Test_EntityReceived()
    {
      var entity = GetFakeEntity();
      var dto = new RoleDTO
      {
        Id = entity.ID,
        Name = entity.Name,
        Description = entity.Description
      };

      var roleRepository = GetFakeRoleRepository();
      var adminRepository = GetFakeAdminRepository();
      var service = new RoleService(roleRepository, adminRepository);
      var count = service.GetTotalCount();

      service.Update(dto);

      roleRepository.Received().Update(entity);
    }

    private IRepository<RoleEntity> GetFakeRoleRepository()
    {
      return Substitute.For<IRepository<RoleEntity>>();
    }
    private IRepository<AdminUserEntity> GetFakeAdminRepository()
    {
      return Substitute.For<IRepository<AdminUserEntity>>();
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
