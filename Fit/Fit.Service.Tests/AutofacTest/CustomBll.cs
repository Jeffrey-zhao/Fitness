using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fit.Service.Tests.AutofacTest
{
  class CustomBll : IDependency
  {
    private readonly IRepository<Custom> repository;

    public CustomBll(IRepository<Custom> repository)
    {
      this.repository = repository;
    }

    public void Insert(Custom c)
    {
      repository.Insert(c);
    }

    public void Update(Custom c)
    {
      repository.Update(c);
    }

    public void Delete(Custom c)
    {
      repository.Delete(c);
    }
  }
}
