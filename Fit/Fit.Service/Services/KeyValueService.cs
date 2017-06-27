using Fit.IService;
using Fit.Service.Entities;
using Fit.Service.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fit.Service.Services
{
  public class KeyValueService : IKeyValueService
  {
    KeyValueRepository rep = new KeyValueRepository();

    public string GetValue(string key)
    {
      var entity = rep.GetByKey(key);
      return entity != null ? entity.Value : string.Empty;
    }
  }
}
