using Fit.Service.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fit.Service.Repository
{
  public class UserRepository : IRepository<UserEntity>
  {
    public FitDbContext Ctx { get; }

    public UserRepository()
    {
      Ctx = new FitDbContext();
    }

    public long Add(UserEntity entity)
    {
      Ctx.Users.Add(entity);
      Ctx.SaveChanges();
      return entity.ID;
    }

    public void DeleteById(long id)
    {
      throw new NotImplementedException();
    }

    public IQueryable<UserEntity> GetAll()
    {
      return Ctx.Users.Where(a => a.IsDeleted == false);
    }

    public UserEntity GetById(long id)
    {
      throw new NotImplementedException();
    }

    public void Update(UserEntity entity)
    {
      Ctx.SaveChanges();
    }
  }
}
