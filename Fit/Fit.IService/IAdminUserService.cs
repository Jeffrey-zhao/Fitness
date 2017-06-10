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
    long AddAdminUser(string name,string phoneNum, string email, string password);

    void UpdateAdminUser(long id, string name, string password);

    void Update(AdminUserDTO dto);

    AdminUserDTO[] GetAll();

    AdminUserDTO[] GetPagedData(int startIndex, int pageSize);

    long GetTotalCount();

    AdminUserDTO GetByEmail(string email);

    /// <summary>
    ///
    /// </summary>
    /// <param name="email"></param>
    /// <param name="password"></param>
    /// <returns>if login false, return null value, else return the admin Id</returns>
    long? CheckLogin(string email, string password);

    void MarkDeleted(long id);

    void RecordLoginError(long id);

    void ResetLoginError(long id);

    AdminUserDTO GetById(long id);

    bool CheckPermission(long adminId, string permissionName);
  }
}
