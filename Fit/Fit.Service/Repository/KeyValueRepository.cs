using Fit.Service.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fit.Service.Repository
{
  public class KeyValueRepository : IRepository<KeyValueEntity>
  {
    public FitDbContext Ctx { get; }

    public KeyValueRepository()
    {
      this.Ctx = new FitDbContext();
    }

    public long Add(KeyValueEntity entity)
    {
      throw new NotImplementedException();
    }

    public void DeleteById(long id)
    {
      throw new NotImplementedException();
    }

    public IQueryable<KeyValueEntity> GetAll()
    {
      return Ctx.KeyValues;
    }
    public KeyValueEntity GetByKey(string key)
    {
      return GetAll().FirstOrDefault(a => a.Key == key);
    }

    public KeyValueEntity GetById(long id)
    {
      throw new NotImplementedException();
    }

    public void Update(KeyValueEntity entity)
    {
      throw new NotImplementedException();
    }
  }
}
