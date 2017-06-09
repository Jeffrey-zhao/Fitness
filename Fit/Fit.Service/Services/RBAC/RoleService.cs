using Fit.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fit.DTO.RBAC;
using Fit.Service.Repository;
using Fit.Service.Entities.RBAC;
using Fit.Common;
using System.Data.Entity;

namespace Fit.Service.Services.RBAC
{
  public class RoleService : IRoleService
  {
    IRepository<RoleEntity> roleRepository;
    IRepository<AdminUserEntity> adminRepository;
    public RoleService(IRepository<RoleEntity> roleRepository, IRepository<AdminUserEntity> adminRepository)
    {
      this.roleRepository = roleRepository;
      this.adminRepository = adminRepository;
    }

    public long Add(RoleDTO dto)
    {
      var existedRole = roleRepository.GetAll().Where(a => a.Name == dto.Name).FirstOrDefault();
      if (existedRole != null) throw new ArgumentException(ExceptionMsg.GetObjExistMsg("RoleEntity", dto.Name));

      var entity = new RoleEntity
      {
        Name = dto.Name,
        Description = dto.Description
      };
      return roleRepository.Add(entity);
    }

    public void Delete(long id)
    {
      roleRepository.DeleteById(id);
    }

    public void EditAdminRole(long adminId, long[] roleIDs)
    {
      var admin = adminRepository.GetById(adminId);
      if (admin == null) throw new ArgumentException(
        ExceptionMsg.GetObjNullMsg("AdminUserEntity"));

      admin.Roles.Clear();
      if (roleIDs != null && roleIDs.Length > 0)
      {
        var allRoles = adminRepository.Ctx.Roles.Where(a => a.IsDeleted == false);
        var updatings = allRoles.Where(p => roleIDs.Contains(p.ID));
        if (updatings == null) throw new ArgumentException(
          ExceptionMsg.GetObjNullMsg("RoleEntities"));

        foreach (var item in updatings)
        {
          admin.Roles.Add(item);
        }
      }

      adminRepository.Update(admin);
    }

    public RoleDTO[] GetAll()
    {
      return roleRepository.GetAll().ToList().Select(a => ToDTO(a)).ToArray();
    }

    public RoleDTO GetById(long id)
    {
      var entity = roleRepository.GetById(id);
      return ToDTO(entity);
    }

    public long[] GetIDsByAdmin(long adminID)
    {
      var list = new List<long>();
      var roles = roleRepository.GetAll().Include(a => a.AdminUsers);
      foreach (var role in roles)
      {
        if (role.AdminUsers.Select(a => a.ID).Contains(adminID)) list.Add(role.ID);
      }
      return list.ToArray();
    }

    public RoleDTO[] GetPagedData(int startIndex, int pageSize)
    {
      var entities = roleRepository.GetAll().OrderByDescending(a => a.CreatedDateTime).Skip(startIndex).Take(pageSize);
      return entities.ToList().Select(a => ToDTO(a)).ToArray();
    }

    public long GetTotalCount()
    {
      return roleRepository.GetAll().Count();
    }

    public void Update(RoleDTO dto)
    {
      var entity = new RoleEntity
      {
        ID = dto.Id,
        Name = dto.Name,
        Description = dto.Description
      };
      roleRepository.Update(entity);
    }

    private RoleDTO ToDTO(RoleEntity entity)
    {
      if (entity == null) throw new ArgumentException(ExceptionMsg.GetObjNullMsg("RoleEntity"));
      var dto = new RoleDTO
      {
        Id = entity.ID,
        Name = entity.Name,
        Description = entity.Description
      };
      return dto;
    }
  }
}
