using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fit.Service.Tests.AutofacTest
{
  interface IRepository<T> where T:class
  {
    void Insert(T entity);
    void Update(T entity);
    void Delete(T entity);
  }
}
