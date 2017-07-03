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

    long index, planCount, planID;
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

    [HttpPost]
    public ActionResult LoadMuscle(long id)
    {
      var muscleList = muscleService.GetByMuscleGroupID(id).ToList();
      muscleList.Insert(0, new DTO.MuscleDTO { Id = 0, Name = Consts.TEXT_SELECT_MUSCLE });
      return MVCHelper.GetJsonResult(new AjaxResult { Data = muscleList, Status = AjaxResultEnum.ok.ToString() });
    }

    [HttpPost]
    public ActionResult LoadMotion(long id)
    {
      var motionList = motionService.GetByMuscleID(id).ToList();
      motionList.Insert(0, new DTO.MotionDTO { Id = 0, Name = Consts.TEXT_SELECT_MOTION });
      return MVCHelper.GetJsonResult(new AjaxResult { Data = motionList, Status = AjaxResultEnum.ok.ToString() });
    }

    [HttpPost]
    public ActionResult LoadMotionDetails(long id)
    {
      var dto = motionService.GetByID(id);
      return MVCHelper.GetJsonResult(new AjaxResult { Data = dto, Status = AjaxResultEnum.ok.ToString() });
    }

    public ActionResult GetImgUrls(int type, int motionID)
    {
      var dtos = picService.GetByMotionAndType(type, motionID);
      return MVCHelper.GetJsonResult(new AjaxResult { Data = dtos, Status = AjaxResultEnum.ok.ToString() });
    }
  }
}