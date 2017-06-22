using Fit.AdminWeb.Models;
using Fit.Common;
using Fit.DTO;
using Fit.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static Fit.Common.Enums;

namespace Fit.AdminWeb.Controllers
{
  public class MotionController : Controller
  {
    IMuscleGroupService muscleGroupService;
    IMuscleService muscleService;
    IMotionService motionService;
    IMotionPicService picService;

    public MotionController(IMuscleGroupService muscleGroupService, IMuscleService muscleService
      , IMotionService motionService, IMotionPicService picService)
    {
      this.muscleGroupService = muscleGroupService;
      this.muscleService = muscleService;
      this.motionService = motionService;
      this.picService = picService;
    }

    public ActionResult List(int pageIndex = 1)
    {
      var dtos = motionService.GetPagedData((pageIndex - 1) * Consts.PAGE_SIZE_NUM, Consts.PAGE_SIZE_NUM);
      ViewBag.PageIndex = pageIndex;
      ViewBag.TotalCount = motionService.GetTotalCount();
      return View(dtos);
    }

    [HttpGet]
    public ActionResult Add()
    {
      var muscleGroupList = muscleGroupService.GetAll().ToList();
      muscleGroupList.Insert(0, new DTO.MuscleGroupDTO { Id = 0, Name = Consts.TEXT_SELECT_MUSCLE_GROUP });
      picService.DeleteNoReference();
      return View(muscleGroupList);
    }
    [HttpPost]
    public ActionResult Add(MotionModel model)
    {
      if (!ModelState.IsValid)
      {
        return MVCHelper.GetJsonResult(AjaxResultEnum.error, MVCHelper.GetValidMsg(ModelState));
      }
      var dto = new MotionDTO
      {
        Name = model.Name,
        Description = model.Description,
        Detail = model.Detail,
        Attention = model.Attention,
        MainPoint = model.MainPoint
      };
      if (model.MotionType == MotionType.Partial.ToString())
      {
        dto.MuscleID = model.MuscleID;
      }
      var id = motionService.Add(dto);
      if (id < 0) throw new Exception("Motion adding failed");
      picService.LinkPicToMotion(id);
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
    public ActionResult UploadImg(int type, int motionID = 0)
    {
      HttpPostedFileBase file;
      string saveKey;
      for (int i = 0; i < Request.Files.Count; i++)
      {
        file = Request.Files[i];
        saveKey = SaveImgInCloud.Save(file);
        SaveImgUrlInDB((PicType)type, saveKey, motionID);
      }
      return MVCHelper.GetJsonResult(AjaxResultEnum.ok);
    }
    [HttpPost]
    public ActionResult DeleteImg(long id)
    {
      picService.Delete(id);
      return MVCHelper.GetJsonResult(AjaxResultEnum.ok);
    }

    private void SaveImgUrlInDB(PicType type, string url, int motionID)
    {
      var dto = new MotionPicDTO
      {
        PicType = (int)type,
        Url = url,
        MotionID = motionID
      };
      var id = picService.Add(dto);
      if (id <= 0) throw new Exception("adding failed");
    }

    public ActionResult GetImgUrls(int type, int motionID = 0)
    {
      var dtos = picService.GetByMotionAndType(type, motionID);
      return MVCHelper.GetJsonResult(new AjaxResult { Data = dtos, Status = AjaxResultEnum.ok.ToString() });
    }

    [HttpGet]
    public ActionResult Edit(long id)
    {
      var motionDto = motionService.GetByID(id);

      var muscleGroupList = muscleGroupService.GetAll().ToList();
      muscleGroupList.Insert(0, new DTO.MuscleGroupDTO { Id = 0, Name = Consts.TEXT_SELECT_MUSCLE_GROUP });
      var muscleList = muscleService.GetByMuscleGroupID(motionDto.MuscleGroupID).ToList();
      muscleList.Insert(0, new DTO.MuscleDTO { Id = 0, Name = Consts.TEXT_SELECT_MUSCLE });

      var model = new MotionEditViewModel
      {
        Motion = motionDto,
        MuscleGroups = muscleGroupList,
        Muscles = muscleList
      };
      model.MotionType = motionDto.MuscleID.HasValue ? MotionType.Partial.ToString() : MotionType.Combine.ToString();

      return View(model);
    }
    [HttpPost]
    public ActionResult Edit(MotionModel model)
    {
      if (!ModelState.IsValid)
      {
        return MVCHelper.GetJsonResult(AjaxResultEnum.error, MVCHelper.GetValidMsg(ModelState));
      }
      var dto = new MotionDTO
      {
        Id = model.ID,
        Description = model.Description,
        Detail = model.Detail,
        Attention = model.Attention,
        MainPoint = model.MainPoint,
        Name = model.Name,
      };
      if (model.MotionType.ToLower().Equals(MotionType.Partial.ToString().ToLower()))
      {
        dto.MuscleID = model.MuscleID;
      }
      motionService.Update(dto);
      return MVCHelper.GetJsonResult(AjaxResultEnum.ok);
    }
  }
}