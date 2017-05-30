using Fit.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fit.DTO.RBAC;
using Fit.Service.Entities.RBAC;
using Fit.Common;
using Fit.Service.Repository;

namespace Fit.Service.Services.RBAC
{
  public class AdminUserService : IAdminUserService
  {
    private IRepository<AdminUserEntity> repository;

    public AdminUserService(IRepository<AdminUserEntity> repository)
    {
      this.repository = repository;
    }

    public long AddAdminUser(string name, string phoneNum, string password, string email)
    {
      bool isPhoneNumExist = repository.GetAll()
        .Any(a => a.PhoneNum == phoneNum);

      if (isPhoneNumExist)
      {
        throw new ArgumentException(ExceptionMsg
          .GetPhoneNumExistMsg(phoneNum));
      }

      var entity = new AdminUserEntity
      {
        Name = name,
        PhoneNum = phoneNum,
        Email = email
      };
      entity.PasswordSalt = CommonHelper.GenerateCaptchaCode(5);
      entity.PasswordHash = CommonHelper.CalcMD5(entity.PasswordSalt + password);

      var id = repository.Add(entity);
      return id;
    }

    public bool CheckLogin(string phoneNum, string password)
    {
      throw new NotImplementedException();
    }

    public AdminUserDTO[] GetAll()
    {
      return repository.GetAll().Select(a => ToDTO(a)).ToArray();
    }

    public AdminUserDTO GetByPhoneNum(string phoneNum)
    {
      var entity = repository.GetAll().Where(a => a.PhoneNum == phoneNum).FirstOrDefault();
      return ToDTO(entity);
    }

    public void MarkDeleted(long id)
    {
      repository.DeleteById(id);
    }

    public void RecordLoginError(long id)
    {
      throw new NotImplementedException();
    }

    public void ResetLoginError(long id)
    {
      throw new NotImplementedException();
    }

    public void UpdateAdminUser(long id, string name, string phoneNum, string password, string email)
    {
      throw new NotImplementedException();
    }

    private AdminUserDTO ToDTO(AdminUserEntity entity)
    {
      if (entity == null) throw new ArgumentException(ExceptionMsg.GetObjectNullMsg(entity));

      var dto = new AdminUserDTO
      {
        Name = entity.Name,
        PhoneNum = entity.PhoneNum,
        Email = entity.Email,
        LoginErrorTimes = entity.LoginErrorTimes,
        LastLoginErrorDateTime = entity.LastLoginErrorDateTime
      };
      return dto;
    }
  }
}
