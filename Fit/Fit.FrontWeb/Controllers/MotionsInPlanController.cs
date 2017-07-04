using Fit.Common;
using Fit.DTO;
using Fit.FrontWeb.Models;
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
    IKeyValueService kvService;

    public MotionsInPlanController(IMotionsInPlanService mipService, IPlanService planService
                      , IMuscleGroupService muscleGroupService, IMuscleService muscleService
                      , IMotionService motionService, IMotionPicService picService, IKeyValueService kvService)
    {
      this.mipService = mipService;
      this.planService = planService;
      this.muscleGroupService = muscleGroupService;
      this.muscleService = muscleService;
      this.motionService = motionService;
      this.picService = picService;
      this.kvService = kvService;
    }

    public ActionResult List(long planId, long index, long planCount)
    {
      ViewBag.PlanId = planId;
      ViewBag.Index = index;
      ViewBag.PlanCount = planCount;
      var dtos = mipService.GetByPlanID(planId);
      return View(dtos);
    }

    [HttpGet]
    public ActionResult Add(long planId, long index, long planCount)
    {
      ViewBag.PlanId = planId;
      ViewBag.Index = index;
      ViewBag.PlanCount = planCount;
      var muscleGroupList = muscleGroupService.GetAll().ToList();
      muscleGroupList.Insert(0, new DTO.MuscleGroupDTO { Id = 0, Name = Consts.TEXT_SELECT_MUSCLE_GROUP });

      return View(muscleGroupList);
    }

    [HttpPost]
    public ActionResult AddPartial(PartialMotionAddModel model)
    {
      if (!ModelState.IsValid)
      {
        return View("Error");
      }
      var dto = new MotionsInPlanInputDTO
      {
        PlanID = model.PlanId,
        MotionID = model.MotionPartial,
        Groups = model.Groups,
        Times = model.Times
      };
      if (model.Number.HasValue)
      {
        dto.Number = model.Number.Value;
      }
      mipService.Add(dto);
      return Redirect(string.Format("/MotionsInPlan/List?planId={0}&index={1}&planCount={2}",model.PlanId,model.Index,model.PlanCount));
    }
    [HttpPost]
    public ActionResult AddCombine(CombineMotionAddModel model)
    {
      if (!ModelState.IsValid)
      {
        return View("Error");
      }
      var dto = new MotionsInPlanInputDTO
      {
        PlanID = model.PlanId,
        MotionID=model.MotionCombine,
        Number = model.Number
      };
      mipService.Add(dto);
      return Redirect(string.Format("/MotionsInPlan/List?planId={0}&index={1}&planCount={2}", model.PlanId, model.Index, model.PlanCount));
    }

    [HttpPost]
    public ActionResult Delete(long id)
    {
      mipService.Delete(id);
      return MVCHelper.GetJsonResult(AjaxResultEnum.ok);
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

    [HttpPost]
    public ActionResult GetImgUrls(int type, int motionID)
    {
      var dtos = picService.GetByMotionAndType(type, motionID);
      return MVCHelper.GetJsonResult(new AjaxResult { Data = dtos, Status = AjaxResultEnum.ok.ToString() });
    }

    [HttpPost]
    public ActionResult LoadGroups(long id)
    {
      var list = new List<string>();
      list.Insert(0, Consts.TEXT_SELECT_GROUPS);
      var max = kvService.GetIntValue(DBKeys.PLAN_MAX_GROUP);
      for (int i = 1; i <= max; i++)
      {
        list.Add(i + Consts.MEASUREMENT_GROUPS);
      }
      return MVCHelper.GetJsonResult(new AjaxResult { Data = list, Status = AjaxResultEnum.ok.ToString() });
    }

    [HttpPost]
    public ActionResult LoadTimes(long id)
    {
      var list = new List<string>();
      list.Insert(0, Consts.TEXT_SELECT_TIMES);
      var max = kvService.GetIntValue(DBKeys.PLAN_MAX_TIME);
      for (int i = 1; i <= max; i++)
      {
        list.Add(i + Consts.MEASUREMENT_TIMES);
      }
      return MVCHelper.GetJsonResult(new AjaxResult { Data = list, Status = AjaxResultEnum.ok.ToString() });
    }

    [HttpPost]
    public ActionResult LoadMeasurement(long id)
    {
      var measurement = motionService.GetMeasurement(id);
      return MVCHelper.GetJsonResult(new AjaxResult { Data = measurement, Status = AjaxResultEnum.ok.ToString() });
    }
  }
}