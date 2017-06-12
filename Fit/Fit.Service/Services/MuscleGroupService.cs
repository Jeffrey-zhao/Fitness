using Fit.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fit.DTO;
using Fit.Service.Repository;
using Fit.Service.Entities;
using Fit.Common;
using System.Data.Entity;

namespace Fit.Service.Services
{
  public class MuscleGroupService : IMuscleGroupService
  {
    IRepository<MuscleGroupEntity> repository;

    public MuscleGroupService(IRepository<MuscleGroupEntity> repository)
    {
      this.repository = repository;
    }

    public MuscleGroupDTO[] GetAll()
    {
      return repository.GetAll().AsNoTracking().OrderByDescending(a => a.CreatedDateTime).ToList()
        .Select(a => ToDTO(a)).ToArray();
    }

    public MuscleGroupDTO[] GetPagedData(int startIndex, int pageSize)
    {
      return repository.GetAll().AsNoTracking().OrderByDescending(a => a.CreatedDateTime)
        .Skip(startIndex).Take(pageSize).ToList()
        .Select(a => ToDTO(a)).ToArray();
    }

    public long GetTotalCount()
    {
      return repository.GetAll().AsNoTracking().Count();
    }

    private MuscleGroupDTO ToDTO(MuscleGroupEntity entity)
    {
      if (entity == null)
      {
        throw new ArgumentException(ExceptionMsg.GetObjNullMsg("MuscleGroupEntity"));
      }
      var dto = new MuscleGroupDTO {
        Id=entity.ID,
        Name=entity.Name,
        Description=entity.Description
      };
      return dto;
    }
  }
}
