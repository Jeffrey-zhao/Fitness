using Fit.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fit.IService
{
  public interface IUserService : IServiceSupport
  {
    UserInfoDTO[] GetPagedData(int startIndex, int pageSize);

    long GetTotalCount();

    UserDTO Add(UserDTO dto);

    bool Activate(long id, string operateCode);

    bool IsEmailExist(string email);

    void Update(UserDTO dto);
  }
}
