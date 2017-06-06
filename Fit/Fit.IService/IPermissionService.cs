using Fit.DTO.RBAC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fit.IService
{
  public interface IPermissionService : IServiceSupport
  {
    PermissionDTO[] GetPagedData(int startIndex, int pageSize);

    PermissionDTO GetById(long id);

    long Add(PermissionDTO dto);

    void Update(PermissionDTO dto);

    void Delete(long id);
  }
}
