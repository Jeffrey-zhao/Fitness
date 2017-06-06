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
    IRepository<PermissionEntity> repository;
    public PermissionService(IRepository<PermissionEntity> repository)
    {
      this.repository = repository;
    }

    public long Add(PermissionDTO dto)
    {
      var checkExist = repository.GetAll().Where(a => a.Name == dto.Name).FirstOrDefault();
      if (checkExist != null) throw new ArgumentException(ExceptionMsg.GetObjExistMsg("Permission",dto.Name));

      var entity = new PermissionEntity
      {
        Name = dto.Name,
        Description = dto.Description
      };
      return repository.Add(entity);
    }

    public void Delete(long id)
    {
      repository.DeleteById(id);
    }

    public PermissionDTO GetById(long id)
    {
      var entity = repository.GetById(id);
      var dto = new PermissionDTO
      {
        Id = entity.ID,
        Name = entity.Name,
        Description = entity.Description
      };
      return dto;
    }

    public PermissionDTO[] GetPagedData(int startIndex, int pageSize)
    {
      var entity = repository.GetAll().OrderByDescending(a => a.CreatedDateTime).Skip(startIndex).Take(pageSize);
      return entity.ToList().Select(a => ToDTO(a)).ToArray();
    }

    public void Update(PermissionDTO dto)
    {
      var entity = repository.GetById(dto.Id);
      entity.Name = dto.Name;
      entity.Description = dto.Description;
    }

    private PermissionDTO ToDTO(PermissionEntity entity)
    {
      if (entity == null) throw new ArgumentException(ExceptionMsg.GetObjectNullMsg("PermissionEntity"));
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
