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
    long AddAdminUser(string name, string email, string password);

    void UpdateAdminUser(long id, string name, string password, string email);

    AdminUserDTO[] GetAll();

    AdminUserDTO GetByEmail(string email);

    bool CheckLogin(string email, string password);

    void MarkDeleted(long id);

    void RecordLoginError(long id);

    void ResetLoginError(long id);

    AdminUserDTO GetById(long id);
  }
}
