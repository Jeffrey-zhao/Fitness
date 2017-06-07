using Fit.DTO.RBAC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fit.IService
{
  public interface IRoleService:IServiceSupport
  {
    RoleDTO[] GetPagedData(int startIndex, int pageSize);

    long GetTotalCount();

    RoleDTO GetById(long id);

    long Add(RoleDTO dto);

    void Update(RoleDTO dto);

    void Delete(long id);
  }
}
