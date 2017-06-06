using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fit.Service.Tests.AutofacTest
{
  class Repository<T> : IRepository<T> where T : class
  {
    private Idal<T> dal;
    public Repository(Idal<T> dal)
    {
      this.dal = dal;
    }

    public void Delete(T entity)
    {
      dal.Delete(entity);
    }

    public void Insert(T entity)
    {
      dal.Insert(entity);
    }

    public void Update(T entity)
    {
      dal.Update(entity);
    }
  }
}
