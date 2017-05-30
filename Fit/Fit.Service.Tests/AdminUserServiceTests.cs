using Fit.Service.Services.RBAC;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fit.Service.Tests
{
  [TestFixture]
  public class AdminUserServiceTests
  {
    [Test]
    public void AddAdminUser_Start()
    {
      var adminUser = new AdminUserService();
      adminUser.AddAdminUser("Test1","123","123","123@123");
    }
  }
}
