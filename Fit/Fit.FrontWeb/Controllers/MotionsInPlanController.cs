using Fit.Common;
using Fit.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Fit.FrontWeb.Controllers
{
  public class MotionsInPlanController : Controller
  {
    IMotionsInPlanService mipService;
    IPlanService planService;
    IMuscleGroupService muscleGroupService;
    IMuscleService muscleService;
    IMotionService motionService;
    IMotionPicService picService;

    long index, planCount,planID;
    public MotionsInPlanController(IMotionsInPlanService mipService, IPlanService planService
                      , IMuscleGroupService muscleGroupService, IMuscleService muscleService
                      , IMotionService motionService, IMotionPicService picService)
    {
      this.mipService = mipService;
      this.planService = planService;
      this.muscleGroupService = muscleGroupService;
      this.muscleService = muscleService;
      this.motionService = motionService;
      this.picService = picService;
    }

    public ActionResult List(long planId, long index, long planCount)
    {
      ViewBag.Index = this.index = index;
      ViewBag.PlanCount = this.planCount = planCount;
      ViewBag.PlanId = planId;
      var dtos = mipService.GetByPlanID(planId);
      return View(dtos);
    }

    [HttpGet]
    public ActionResult Add(long id)
    {
      planID = id;
      var muscleGroupList = muscleGroupService.GetAll().ToList();
      muscleGroupList.Insert(0, new DTO.MuscleGroupDTO { Id = 0, Name = Consts.TEXT_SELECT_MUSCLE_GROUP });

      return View(muscleGroupList);
    }
  }
}