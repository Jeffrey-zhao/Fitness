using Fit.DTO.RBAC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fit.IService
{
  public interface IAdminUserService : IServiceSupport
  {
    long AddAdminUser(string name, string phoneNum,
      string password, string email);

    void UpdateAdminUser(long id, string name, string phoneNum,
      string password, string email);

    AdminUserDTO[] GetAll();

    AdminUserDTO GetByPhoneNum(string phoneNum);

    bool CheckLogin(string phoneNum, string password);

    void MarkDeleted(long id);

    void RecordLoginError(long id);

    void ResetLoginError(long id);
  }
}
