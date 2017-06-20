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
      muscleGroupList.Insert(0, new DTO.MuscleGroupDTO { Id = 0, Name = "请选择肌群..." });
      return View(muscleGroupList);
    }
    [HttpPost]
    public ActionResult Add(MotionAddModel model)
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
      if (model.MuscleID > 0)
      {
        dto.MuscleID = model.MuscleID;
      }
      motionService.Add(dto);
      return MVCHelper.GetJsonResult(AjaxResultEnum.ok);
    }


    [HttpPost]
    public ActionResult LoadMuscle(long id)
    {
      var muscleList = muscleService.GetByMuscleGroupID(id).ToList();
      muscleList.Insert(0, new DTO.MuscleDTO { Id = 0, Name = "请选择肌肉..." });
      return MVCHelper.GetJsonResult(new AjaxResult { Data = muscleList, Status = AjaxResultEnum.ok.ToString() });
    }

    [HttpPost]
    public ActionResult UploadImg(int motionID, int type)
    {
      HttpPostedFileBase file;
      string saveKey;
      for (int i = 0; i < Request.Files.Count; i++)
      {
        file = Request.Files[i];
        saveKey = SaveImgInCloud.Save(file);
        SaveImgUrlInDB(motionID, (PicType)type, saveKey);

      }
      return MVCHelper.GetJsonResult(AjaxResultEnum.ok);
    }

    private void SaveImgUrlInDB(int motionID, PicType type, string url)
    {
      var dto = new MotionPicDTO
      {
        PicType = type,
        Url = url,
        MotionID = motionID
      };
      var id = picService.Add(dto);
      if (id <= 0) throw new Exception("adding failed");
    }
  }
}