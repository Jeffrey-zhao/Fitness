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

namespace Fit.Service.Services.RBAC
{
  public class PermissionService : IPermissionService
  {
    IRepository<PermissionEntity> permissionRepository;
    IRepository<RoleEntity> roleRepository;
    public PermissionService(IRepository<PermissionEntity> permissionRepository, IRepository<RoleEntity> roleRepository)
    {
      this.permissionRepository = permissionRepository;
      this.roleRepository = roleRepository;
    }

    public long Add(PermissionDTO dto)
    {
      var checkExist = permissionRepository.GetAll().Where(a => a.Name == dto.Name).FirstOrDefault();
      if (checkExist != null) throw new ArgumentException(ExceptionMsg.GetObjExistMsg("Permission", dto.Name));

      var entity = new PermissionEntity
      {
        Name = dto.Name,
        Description = dto.Description
      };
      return permissionRepository.Add(entity);
    }

    public void AddRolePermission(long roleId, long[] permissionIDs)
    {
      //var role = roleRepository.GetById(roleId);
      //if (role == null) throw new ArgumentException(ExceptionMsg.GetObjNullMsg("RoleEntity"));
      //if (permissionIDs.Length <= 0) return;

      //role.Permissions.Clear();
    }

    public void Delete(long id)
    {
      permissionRepository.DeleteById(id);
    }

    public void EditRolePermission(long roleId, long[] permissionIDs)
    {
      var role = roleRepository.GetById(roleId);
      if (role == null) throw new ArgumentException(ExceptionMsg.GetObjNullMsg("RoleEntity"));
      if (permissionIDs.Length <= 0) return;

      var allPermission=roleRepository.Ctx.Permissions.Where(a => a.IsDeleted == false);
      var updatings = allPermission.Where(p => permissionIDs.Contains(p.ID));
      if (updatings == null) throw new ArgumentException(ExceptionMsg.GetObjNullMsg("PermissionEntities"));

      role.Permissions.Clear();
      foreach (var item in updatings)
      {
        role.Permissions.Add(item);
      }

      roleRepository.Update(role);
    }

    public PermissionDTO[] GetAll()
    {
      var entities = permissionRepository.GetAll().OrderBy(a => a.Name);
      return entities.ToList().Select(a => ToDTO(a)).ToArray();
    }

    public PermissionDTO GetById(long id)
    {
      permissionRepository.GetById(id);
      return ToDTO(permissionRepository.GetById(id));
    }

    public PermissionDTO[] GetPagedData(int startIndex, int pageSize)
    {
      var entity = permissionRepository.GetAll().OrderByDescending(a => a.CreatedDateTime).Skip(startIndex).Take(pageSize);
      return entity.ToList().Select(a => ToDTO(a)).ToArray();
    }

    public long GetTotalCount()
    {
      return permissionRepository.GetAll().Count();
    }

    public void Update(PermissionDTO dto)
    {
      var entity = new PermissionEntity
      {
        ID = dto.Id,
        Name = dto.Name,
        Description = dto.Description
      };
      permissionRepository.Update(entity);
    }

    private PermissionDTO ToDTO(PermissionEntity entity)
    {
      if (entity == null) throw new ArgumentException(ExceptionMsg.GetObjNullMsg("PermissionEntity"));
      var dto = new PermissionDTO
      {
        Id = entity.ID,
        Name = entity.Name,
        Description = entity.Description
      };
      return dto;
    }
  }
}
