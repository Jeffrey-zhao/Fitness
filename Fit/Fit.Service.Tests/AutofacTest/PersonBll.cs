using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fit.Service.Tests.AutofacTest
{
  class PersonBll:IDependency
  {
    private readonly IRepository<Person> repository;

    public PersonBll(IRepository<Person> repository)
    {
      this.repository = repository;
    }

    public void Insert(Person c)
    {
      repository.Insert(c);
    }

    public void Update(Person c)
    {
      repository.Update(c);
    }

    public void Delete(Person c)
    {
      repository.Delete(c);
    }
  }
}
