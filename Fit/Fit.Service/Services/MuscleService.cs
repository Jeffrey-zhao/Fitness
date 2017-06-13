using Fit.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fit.DTO;
using Fit.Service.Entities;
using Fit.Service.Repository;
using Fit.Common;
using System.Data.Entity;

namespace Fit.Service.Services
{
  public class MuscleService : IMuscleService
  {
    IRepository<MuscleEntity> repository;

    public MuscleService(IRepository<MuscleEntity> repository)
    {
      this.repository = repository;
    }

    public MuscleDTO[] GetAll()
    {
      return repository.GetAll().AsNoTracking().OrderByDescending(a => a.CreatedDateTime).ToList()
        .Select(a => ToDTO(a)).ToArray();
    }

    public MuscleDTO[] GetByMuscleGroupID(long id)
    {
      return repository.GetAll().Include(a=>a.MuscleGroup).AsNoTracking()
        .Where(a=>a.MuscleGroupID==id).OrderByDescending(a => a.CreatedDateTime).ToList()
       .Select(a => ToDTO(a)).ToArray();
    }

    public MuscleDTO[] GetPagedData(int startIndex, int pageSize)
    {
      return repository.GetAll().Include(a=>a.MuscleGroup).AsNoTracking().OrderByDescending(a => a.CreatedDateTime).ToList()
        .Skip(startIndex).Take(pageSize)
        .Select(a => ToDTO(a)).ToArray();
    }

    public long GetTotalCount()
    {
      return repository.GetAll().AsNoTracking().Count();
    }

    public MuscleDTO ToDTO(MuscleEntity entity)
    {
      if (entity == null)
      {
        throw new Exception(ExceptionMsg.GetObjNullMsg("MuscleEntity"));
      }
      var dto = new MuscleDTO
      {
        Id = entity.ID,
        Name = entity.Name,
        Description = entity.Description
      };
      if (entity.MuscleGroup != null)
      {
        dto.MuscleGroupName = entity.MuscleGroup.Name;
      }
      return dto;
    }
  }
}
