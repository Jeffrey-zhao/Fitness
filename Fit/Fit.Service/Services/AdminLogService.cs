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

namespace Fit.Service.Services
{
  public class AdminLogService : IAdminLogService
  {
    IRepository<AdminLogEntity> rep;
    public AdminLogService(IRepository<AdminLogEntity> rep)
    {
      this.rep = rep;
    }
    public void Add(AdminLogDTO dto)
    {
      var entity = new AdminLogEntity
      {
        Name = dto.Name,
        AdminUserID = dto.AdminID
      };
      rep.Add(entity);
    }

    public AdminLogDTO[] GetPagedData(int startIndex, int pageSize)
    {
      var entities = rep.GetAll().OrderByDescending(a => a.CreatedDateTime)
        .OrderBy(a => a.Name).Skip(startIndex).Take(pageSize).ToList();

      return entities.Select(a => ToDTO(a)).ToArray();
    }

    public long GetTotalCount()
    {
      return rep.GetAll().Count();
    }

    public AdminLogDTO ToDTO(AdminLogEntity entity)
    {
      if (entity == null) throw new ArgumentException(ExceptionMsg.GetObjNullMsg("AdminLogEntity"));
      var dto = new AdminLogDTO
      {
        Name = entity.Name,
        CreateDateTime = entity.CreatedDateTime,
        AdminName = entity.AdminUser.Name
      };
      return dto;
    }
  }
}
