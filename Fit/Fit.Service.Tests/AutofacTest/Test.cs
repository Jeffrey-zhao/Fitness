using Autofac;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fit.Service.Tests.AutofacTest
{
  [TestFixture]
  class Test
  {
    [Test]
    public void TestReslove()
    {
      var builder = new ContainerBuilder();
      builder.RegisterGeneric(typeof(Dal<>)).As(typeof(Idal<>)).InstancePerDependency();

      builder.RegisterGeneric(typeof(Repository<>)).As(typeof(IRepository<>)).InstancePerDependency();

      builder.Register(c => new PersonBll((IRepository<Person>)c.Resolve(typeof(IRepository<Person>))));

      builder.Register(c => new CustomBll((IRepository<Custom>)c.Resolve(typeof(IRepository<Custom>))));

      using (var container = builder.Build(Autofac.Builder.ContainerBuildOptions.None))
      {
        Person p = new Person
        {
          Name = "person",
          Age = 23
        };
        var personBll = container.Resolve<PersonBll>();
        personBll.Insert(p);

        Custom c = new Custom
        {
          CustomName = "person",
          CustomID = 2
        };
        var customBll = container.Resolve<CustomBll>();
        customBll.Insert(c);
      }
    }
  }
}
