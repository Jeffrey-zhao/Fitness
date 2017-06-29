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

    public bool Activate(long id, string operateCode)
    {
      var result = false;
      var entity = userRep.GetById(id);
      if (entity == null) return result;

      if (operateCode.Equals(entity.OperateCode))
      {
        entity.IsActivated = true;
        userRep.Update(entity);
        result = true;
      }

      return result;
    }

    public UserDTO Add(UserDTO dto)
    {
      var entity = new UserEntity
      {
        Name = dto.Name,
        Email = dto.Email
      };
      entity.PasswordSalt = CommonHelper.GenerateCaptchaCode(Consts.CAPTCHA_LENGTH);
      entity.PasswordHash = CommonHelper.CalcMD5(entity.PasswordSalt + dto.Password);
      entity.OperateCode = CommonHelper.GenerateCaptchaCode(Consts.CAPTCHA_LENGTH);
      var id = userRep.Add(entity);
      var dtoResult = new UserDTO
      {
        ID = id,
        OperateCode = entity.OperateCode
      };
      return dtoResult;
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

    public bool IsEmailExist(string email)
    {
      return !(GetByEmail(email) == null);
    }

    private UserEntity GetByEmail(string email)
    {
      return userRep.GetAll().FirstOrDefault(a => a.Email.Equals(email));
    }

    public void Update(UserDTO dto)
    {
      var entity = GetByEmail(dto.Email);
      if (entity == null)
      {
        throw new ArgumentException(ExceptionMsg.GetObjNullMsg("UserEntity"));
      }
      if (!string.IsNullOrWhiteSpace(dto.Name))
      {
        entity.Name = dto.Name;
      }
      if (!string.IsNullOrWhiteSpace(dto.Password))
      {
        entity.PasswordHash = CommonHelper.CalcMD5(entity.PasswordSalt + dto.Password);
      }

      userRep.Update(entity);
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
