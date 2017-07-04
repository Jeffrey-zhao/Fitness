using Fit.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fit.DTO;
using NUnit.Framework;
using Fit.Service.Services;
using NSubstitute;
using Fit.Service.Repository;
using Fit.Service.Entities;
using Fit.Common;

namespace Fit.Service.Tests
{
  [TestFixture]
  public class MotionServiceTests : IMotionService
  {
    public long Add(MotionDTO dto)
    {
      throw new NotImplementedException();
    }
    [Test]
    public void Add_Exist_Throw()
    {
      var motionRep = GetMotionRep();
      var picRep = GetPicRep();
      var service = new MotionService(motionRep, picRep);

      var dto = new MotionDTO
      {
        Name = "Name"
      };

      var entity = new MotionEntity { Name = "Name" };
      var entities = new List<MotionEntity> { entity }.AsQueryable();
      motionRep.GetAll().Returns(entities);

      Assert.Throws<ArgumentException>(() => service.Add(dto));
    }
    [Test]
    public void Add_NotExist_ReturnId()
    {
      var motionRep = GetMotionRep();
      var picRep = GetPicRep();
      var service = new MotionService(motionRep, picRep);

      motionRep.Add(Arg.Any<MotionEntity>()).Returns(1);

      var dto = new MotionDTO
      {
        Name = "Name"
      };
      var result = service.Add(dto);

      Assert.AreEqual(1, result);
    }

    public void Delete(long id)
    {
      throw new NotImplementedException();
    }
    [Test]
    public void Delete_Test()
    {
      var motionRep = GetMotionRep();
      var picRep = GetPicRep();
      var service = new MotionService(motionRep, picRep);

      service.Delete(1);

      motionRep.Received().DeleteById(1);
    }

    public MotionDTO[] GetByMuscleID(long id)
    {
      throw new NotImplementedException();
    }
    [Test]
    public void GetByMuscleID_IdExist_ReturnData()
    {
      var motionRep = GetMotionRep();
      var picRep = GetPicRep();
      var service = new MotionService(motionRep, picRep);

      var motionEntities = GetFakeMotionEntities(3);

      motionRep.GetAll().Returns(motionEntities);

      var result = service.GetByMuscleID(2);

      Assert.AreEqual(3, result.Length);
    }
    [Test]
    public void GetByMuscleID_IdNotExist_ReturnEmpty()
    {
      var motionRep = GetMotionRep();
      var picRep = GetPicRep();
      var service = new MotionService(motionRep, picRep);

      var result = service.GetByMuscleID(2);

      Assert.IsTrue(result.Length == 0);
    }

    public MotionDTO[] GetPagedData(int startIndex, int pageSize)
    {
      throw new NotImplementedException();
    }
    [Test]
    public void GetPagedData_InOnePage_ReturnAll()
    {
      var motionRep = GetMotionRep();
      var picRep = GetPicRep();
      var service = new MotionService(motionRep, picRep);
      var motionEntities = GetFakeMotionEntities(5);
      motionRep.GetAll().Returns(motionEntities);

      var result = service.GetPagedData(0, 10);

      Assert.AreEqual(5, result.Length);
    }
    [Test]
    public void GetPagedData_InMultiPage_ReturnPageSizeData()
    {
      var motionRep = GetMotionRep();
      var picRep = GetPicRep();
      var service = new MotionService(motionRep, picRep);
      var motionEntities = GetFakeMotionEntities(5);
      motionRep.GetAll().Returns(motionEntities);

      var result = service.GetPagedData(0, 4);

      Assert.AreEqual(4, result.Length);
    }
    [Test]
    public void GetPagedData_NotInPage_ReturnEmpty()
    {
      var motionRep = GetMotionRep();
      var picRep = GetPicRep();
      var service = new MotionService(motionRep, picRep);
      var motionEntities = GetFakeMotionEntities(5);
      motionRep.GetAll().Returns(motionEntities);

      var result = service.GetPagedData(8, 4);

      Assert.AreEqual(0, result.Length);
    }

    public long GetTotalCount()
    {
      throw new NotImplementedException();
    }
    [Test]
    public void GetTotalCount_Test_ReturnCount()
    {
      var motionRep = GetMotionRep();
      var picRep = GetPicRep();
      var service = new MotionService(motionRep, picRep);
      var motionEntities = GetFakeMotionEntities(5);
      motionRep.GetAll().Returns(motionEntities);

      var result = service.GetTotalCount();

      Assert.AreEqual(5, result);
    }

    [Test]
    [Ignore("")]
    public void Update(MotionDTO dto)
    {
      throw new NotImplementedException();
    }

    private IRepository<MotionEntity> GetMotionRep()
    {
      return Substitute.For<IRepository<MotionEntity>>();
    }

    private IRepository<MotionPicEntity> GetPicRep()
    {
      return Substitute.For<IRepository<MotionPicEntity>>();
    }

    private MotionEntity GetFakeMotionEntity(long id)
    {
      var entity = new MotionEntity
      {
        ID = id,
        MuscleID = 2
      };

      return entity;
    }
    private MotionPicEntity GetFakePicEntity(long id)
    {
      var entity = new MotionPicEntity
      {
        ID = id,
        PicType = (int)Enums.PicType.Attention
      };

      return entity;
    }
    private IQueryable<MotionEntity> GetFakeMotionEntities(int num)
    {
      var entities = new List<MotionEntity>();
      for (int i = 1; i <= num; i++)
      {
        entities.Add(GetFakeMotionEntity(i));
      }
      return entities.AsQueryable();
    }
    private ICollection<MotionPicEntity> GetFakePicEntities(int num)
    {
      var entities = new List<MotionPicEntity>();
      for (int i = 1; i <= num; i++)
      {
        entities.Add(GetFakePicEntity(i));
      }
      return entities;
    }

    public MotionDTO GetByID(long id)
    {
      throw new NotImplementedException();
    }

    public string GetMeasurement(long id)
    {
      throw new NotImplementedException();
    }
  }
}
