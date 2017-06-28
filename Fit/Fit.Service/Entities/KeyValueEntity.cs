using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fit.Service.Entities
{
  public class KeyValueEntity
  {
    public long ID { get; set; }
    public string Key { get; set; }
    public string Value { get; set; }
    public string Type { get; set; }
  }
}
