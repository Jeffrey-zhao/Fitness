using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fit.Service.Repository
{
  public interface IRepository<T> where T : class
  {
    T GetById(long id);

    IQueryable<T> GetAll();

    long Add(T entity);

    void Update(T entity);

    void DeleteById(long id);
  }
}
