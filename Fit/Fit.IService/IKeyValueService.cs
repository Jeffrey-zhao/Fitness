using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fit.IService
{
  public interface IKeyValueService:IServiceSupport
  {
    string GetValue(string key);
  }
}
