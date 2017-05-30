using Fit.Service.Entities.RBAC;
using Fit.Service.Repository;
using Fit.Service.Services.RBAC;
using NSubstitute;
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
    public void AddAdminUser_NormalValue_ReturnId()
    {
      var repository = Substitute.For<IRepository<AdminUserEntity>>();
      repository.Add(Arg.Any<AdminUserEntity>()).Returns(2);
      var adminUser = new AdminUserService(repository);

      var id = adminUser.AddAdminUser("Test1", "1231", "123", "123@123");

      Assert.AreEqual(2, id);
    }
    [Test]
    public void AddAdminUser_PhoneExist_ThrowException()
    {
      var phoneNum = "1231";
      var data = new List<AdminUserEntity>
      {
        new AdminUserEntity{ PhoneNum=phoneNum}
      }.AsQueryable();
      var repository = Substitute.For<IRepository<AdminUserEntity>>();
      repository.GetAll().Returns(data);
      var adminUser = new AdminUserService(repository);

      Assert.Throws<ArgumentException>(() => adminUser.AddAdminUser("Test1", phoneNum, "123", "123@123"));
    }

  }
}
