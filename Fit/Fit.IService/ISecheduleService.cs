using Fit.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fit.IService
{
  public interface ISecheduleService : IServiceSupport
  {
    /// <summary>
    /// create sechedules within the given days: if the sechedule is empty, create all, 
    /// else if less than the given days, insert sechedules so that the count of new sechedules equal the given days
    /// </summary>
    void CreateSechedule(long userID, int days, DateTime startDate);

    long GetPersistDays(long userID);

    CurrentItemDTO[] GetCurrentItems(long userID);

    void CompleteItems(string itemIDs);

    bool FinishSechedule(long userID);

    bool IsSecheduleFinished(long userID);
  }
}
