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

    public AdminUserService(
      IRepository<AdminUserEntity> repository)
    {
      this.repository = repository;
    }

    public long AddAdminUser(string name, string phoneNum, string email, string password)
    {
      bool isEmailExist = repository.GetAll().Where(a => a.Email == email).FirstOrDefault() != null;

      if (isEmailExist)
      {
        throw new ArgumentException(ExceptionMsg
          .GetObjExistMsg("email", email));
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

    public bool CheckLogin(string email, string password)
    {
      var entity = repository.GetAll().Where(a => a.Email == email).FirstOrDefault();
      if (entity == null) return false;

      string hashForCheck = CommonHelper.CalcMD5(entity.PasswordSalt + password);
      return hashForCheck.Equals(entity.PasswordHash);
    }

    public bool CheckPermission(long adminId, string permissionName)
    {
      var admin = repository.GetById(adminId);
      return admin.Roles.SelectMany(a => a.Permissions)
        .Any(p => p.Name == permissionName);
    }

    public AdminUserDTO[] GetAll()
    {
      return repository.GetAll().ToList().Select(a => ToDTO(a)).ToArray();
    }

    public AdminUserDTO GetByEmail(string email)
    {
      var entity = repository.GetAll().Where(a => a.Email == email).FirstOrDefault();
      return ToDTO(entity);
    }

    public AdminUserDTO GetById(long id)
    {
      return ToDTO(repository.GetById(id));
    }

    public AdminUserDTO[] GetPagedData(int startIndex, int pageSize)
    {
      var adminUsers = repository.GetAll()
        .OrderByDescending(a => a.CreatedDateTime)
        .Skip(startIndex)
        .Take(pageSize);
      return adminUsers.ToList().Select(a => ToDTO(a)).ToArray();
    }

    public long GetTotalCount()
    {
      return GetAll().Count();
    }
    public void MarkDeleted(long id)
    {
      repository.DeleteById(id);
    }

    public void RecordLoginError(long id)
    {
      var entity = repository.GetById(id);
      if (entity == null)
      {
        throw new ArgumentException(ExceptionMsg.GetObjNullMsg("AdminUserEntity"));
      }
      entity.LoginErrorTimes++;
      entity.LastLoginErrorDateTime = DateTimeHelper.GetNow();
      repository.Update(entity);
    }

    public void ResetLoginError(long id)
    {
      var entity = repository.GetById(id);
      if (entity == null)
      {
        throw new ArgumentException(ExceptionMsg.GetObjNullMsg("AdminUserEntity"));
      }
      entity.LoginErrorTimes = 0;
      repository.Update(entity);
    }

    public void Update(AdminUserDTO dto)
    {
      var entity = new AdminUserEntity
      {
        ID = dto.ID,
        Name = dto.Name,
        PhoneNum = dto.PhoneNum,
        Email = dto.Email
      };

      entity.PasswordHash = dto.WillUpdatePwd ?
        CommonHelper.CalcMD5(entity.PasswordSalt + dto.Password)
        : entity.PasswordHash;
      repository.Update(entity);
    }

    public void UpdateAdminUser(long id, string name, string password)
    {
      var entity = repository.GetById(id);
      if (entity == null)
      {
        throw new ArgumentException(ExceptionMsg.GetObjNullMsg("AdminUserEntity"));
      }
      entity.Name = name;
      entity.PasswordHash = CommonHelper.CalcMD5(entity.PasswordSalt + password);
      repository.Update(entity);
    }

    private AdminUserDTO ToDTO(AdminUserEntity entity)
    {
      if (entity == null) throw new ArgumentException(ExceptionMsg.GetObjNullMsg("AdminUserEntity"));

      var dto = new AdminUserDTO
      {
        ID = entity.ID,
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
