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

namespace Fit.Service.Services
{
  public class UserService : IUserService
  {
    IRepository<UserEntity> userRep;

    public UserService(IRepository<UserEntity> userRep)
    {
      this.userRep = userRep;
    }

    public UserInfoDTO[] GetPagedData(int startIndex, int pageSize)
    {
      var entities = userRep.GetAll().OrderByDescending(a => a.CreatedDateTime).Skip(startIndex).Take(pageSize).ToList();
      return entities.Select(a => ToDTO(a)).ToArray();
    }

    public long GetTotalCount()
    {
      return userRep.GetAll().Count();
    }

    private UserInfoDTO ToDTO(UserEntity entity)
    {
      if (entity == null)
      {
        throw new Exception(ExceptionMsg.GetObjNullMsg("UserEntity"));
      }

      var dto = new UserInfoDTO
      {
        ID = entity.ID,
        Name = entity.Name,
        Email = entity.Email,
        IsActivated = entity.IsActivated,
        CreatedDateTime = entity.CreatedDateTime
      };

      return dto;
    }
  }
}
