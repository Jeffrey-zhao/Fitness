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
  public class RoleService : IRoleService
  {
    IRepository<RoleEntity> repository;
    public RoleService(IRepository<RoleEntity> repository)
    {
      this.repository = repository;
    }
    public long Add(RoleDTO dto)
    {
      var existedRole = repository.GetAll().Where(a => a.Name == dto.Name).FirstOrDefault();
      if (existedRole != null) throw new ArgumentException(ExceptionMsg.GetObjExistMsg("RoleEntity",dto.Name));
      
      var entity = new RoleEntity
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

    public RoleDTO GetById(long id)
    {
      var entity = repository.GetById(id);
      return ToDTO(entity);
    }

    public RoleDTO[] GetPagedData(int startIndex, int pageSize)
    {
      var entities = repository.GetAll().OrderByDescending(a => a.CreatedDateTime).Skip(startIndex).Take(pageSize);
      return entities.ToList().Select(a => ToDTO(a)).ToArray();
    }

    public long GetTotalCount()
    {
      return repository.GetAll().Count();
    }

    public void Update(RoleDTO dto)
    {
      var entity = new RoleEntity
      {
        ID=dto.Id,
        Name=dto.Name,
        Description=dto.Description
      };
      repository.Update(entity);
    }

    private RoleDTO ToDTO(RoleEntity entity)
    {
      if (entity == null) throw new ArgumentException(ExceptionMsg.GetObjectNullMsg("RoleEntity"));
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
