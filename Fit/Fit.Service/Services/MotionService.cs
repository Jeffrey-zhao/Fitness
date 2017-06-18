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
using static Fit.Common.Enums;

namespace Fit.Service.Services
{
  public class MotionService : IMotionService
  {
    IRepository<MotionEntity> motionRep;
    IRepository<MotionPicEntity> motionPicRep;
    public MotionService(IRepository<MotionEntity> motionRep, IRepository<MotionPicEntity> motionPicRep)
    {
      this.motionRep = motionRep;
      this.motionPicRep = motionPicRep;
    }

    public long Add(MotionDTO dto)
    {
      var isExists = motionRep.GetAll().Where(a => a.Name == dto.Name).Any();
      if (isExists) throw new ArgumentException(ExceptionMsg.GetObjExistMsg("MotionEntity", dto.Name));

      var entity = new MotionEntity
      {
        Name = dto.Name,
        Description = dto.Description,
        Detail = dto.Detail,
        Attention = dto.Attention,
        MainPoint = dto.MainPoint
      };
      if (dto.MuscleID.HasValue)
      {
        entity.MuscleID = dto.MuscleID.Value;
      }
      return motionRep.Add(entity);
    }

    public void Delete(long id)
    {
      motionRep.DeleteById(id);
    }

    public void Update(MotionDTO dto)
    {
      var getByID = motionRep.GetById(dto.Id);
      if (getByID == null)
      {
        throw new ArgumentException(ExceptionMsg.GetObjNullMsg("MotionEntity"));
      }

      getByID.Name = dto.Name;
      getByID.Description = dto.Description;
      getByID.Attention = dto.Attention;
      getByID.Detail = dto.Detail;
      getByID.MainPoint = dto.MainPoint;

      motionRep.Update(getByID);
    }

    public MotionDTO[] GetByMuscleID(long id)
    {
      var entities = motionRep.GetAll().Include(a => a.Muscle)
        .Include(a => a.Muscle.MuscleGroup).AsNoTracking()
         .Where(a => a.MuscleID == id);
      
      return entities.Select(a => ToDTO(a)).ToArray();
    }

    public MotionDTO[] GetPagedData(int startIndex, int pageSize)
    {
      var entities = motionRep.GetAll().Include(a => a.Muscle)
        .Include(a=>a.Muscle.MuscleGroup).AsNoTracking().OrderBy(a=>a.MuscleID)
          .Skip(startIndex).Take(pageSize).ToList();

      return entities.Select(a => ToDTO(a)).ToArray();
    }

    public long GetTotalCount()
    {
      return motionRep.GetAll().Count();
    }

    private MotionDTO ToDTO(MotionEntity entity)
    {
      if (entity == null) throw new ArgumentException(ExceptionMsg.GetObjNullMsg("MotionEntity"));

      var dto = new MotionDTO
      {
        Id = entity.ID,
        Name = entity.Name,
        Description = entity.Description,
        Detail = entity.Detail,
        Attention = entity.Attention,
        MainPoint = entity.MainPoint,
      };
      if (entity.Muscle != null)
      {
        dto.MuscleID = entity.Muscle.ID;
        dto.MuscleName = entity.Muscle.Name;
        if (entity.Muscle.MuscleGroup != null)
        {
          dto.MuscleGroupID = entity.Muscle.MuscleGroup.ID;
          dto.MuscleGroupName = entity.Muscle.MuscleGroup.Name;
        }
      }
    
      return dto;
    }

    private Dictionary<long, string> GetPicsDic(PicType picType, MotionEntity entity)
    {
      Dictionary<long, string> dic = new Dictionary<long, string>();
      
      var detailPics = entity.MotionPics.Where(a => a.PicType == (int)picType)
        .OrderBy(a => a.CreatedDateTime);
      foreach (var pic in detailPics)
      {
        dic.Add(pic.ID,pic.Url);
      }

      return dic;
    }
  }
}
