using Fit.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fit.DTO.RBAC;
using Fit.Service.Entities.RBAC;
using Fit.Common;

namespace Fit.Service.Services.RBAC
{
  public class AdminUserService : IAdminUserService
  {
    private FitDbContext ctx;
    public AdminUserService()
    {
      ctx = new FitDbContext();
    }
    //~AdminUserService()
    //{
    //  ctx.Dispose();
    //}

    public long AddAdminUser(string name, string phoneNum, string password, string email)
    {
      var cs = new CommonService<AdminUserEntity>(ctx);
      bool isPhoneNumExist = cs.GetAll().Any(a => a.PhoneNum == phoneNum);

      if (isPhoneNumExist)
      {
        throw new ArgumentException(ExceptionMsg.GetPhoneNumExistMsg(phoneNum));
      }

      var entity = new AdminUserEntity
      {
        Name = name,
        PhoneNum = phoneNum,
        Email = email
      };

      entity.PasswordSalt = CommonHelper.GenerateCaptchaCode(5);
      entity.PasswordHash = CommonHelper.CalcMD5(entity.PasswordSalt + password);

      ctx.AdminUsers.Add(entity);
      ctx.SaveChanges();
      return entity.ID;
    }

    public bool CheckLogin(string phoneNum, string password)
    {
      throw new NotImplementedException();
    }

    public AdminUserDTO[] GetAll()
    {
      throw new NotImplementedException();
    }

    public AdminUserDTO GetByPhoneNum(string phoneNum)
    {
      throw new NotImplementedException();
    }

    public void MarkDeleted(long id)
    {
      throw new NotImplementedException();
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
  }
}
