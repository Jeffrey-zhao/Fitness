using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fit.Service.Tests.AutofacTest
{
  class Dal<T> : Idal<T> where T : class
  {
    public void Delete(T entity)
    {
      Console.WriteLine("Delete: "+entity.GetType().FullName);
      Debug.Print("delete");
    }

    public void Insert(T entity)
    {
      Console.WriteLine("Insert: " + entity.GetType().FullName);
    }

    public void Update(T entity)
    {
      Console.WriteLine("Update: " + entity.GetType().FullName);
    }
  }
}
