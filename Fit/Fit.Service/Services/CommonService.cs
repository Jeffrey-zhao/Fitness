using Fit.Service.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fit.Service.Services
{
  public class CommonService<T> where T : BaseEntity
  {
    private FitDbContext ctx;

    public CommonService(FitDbContext ctx)
    {
      this.ctx = ctx;
    }

    public IQueryable<T> GetAll()
    {
      return ctx.Set<T>().Where(e => e.IsDeleted == false);
    }

    public T GetById(long id)
    {
      return GetAll().Where(a => a.ID == id).SingleOrDefault();
    }

    public long GetTotalCount()
    {
      return GetAll().LongCount();
    }

    public IQueryable<T> GetPagedData(int startIndex, int num)
    {
      return GetAll().OrderBy(e => e.CreatedDateTime).Skip(startIndex).Take(num);
    }

    public void MarkDeleted(long id)
    {
      var result = GetById(id);
      result.IsDeleted = true;
      ctx.SaveChanges();
    }
  }
}
