using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fit.Common
{
  public class Consts
  {
    public const string VERIFY_CODE_KEY = "VerifyCode";
    public const string PAGE_SIZE_NAME = "PageSize";
    public const int PAGE_SIZE_NUM = 10;
    public const string LOGIN_EMAIL = "LoginEmail";
    public const string LOGIN_ID = "LoginID";
    public const string TEXT_SELECT_MUSCLE_GROUP = "请选择肌群...";
    public const string TEXT_SELECT_MUSCLE = "请选择肌肉...";
    public const string TEXT_SELECT_MOTION = "请选择锻炼项目...";
    public const string TEXT_SELECT_GROUPS = "请选择组数...";
    public const string MEASUREMENT_GROUPS = "组";
    public const string TEXT_SELECT_TIMES = "请选择次数...";
    public const string MEASUREMENT_TIMES = "次";
    public const string TEXT_SELECT_BURDERN = "请选择负荷...";

    public const string CLOUD_DOMAIN = "http://orivq9ow0.bkt.clouddn.com/";
    public const string TEXT_COMBINE = "综合训练";
    public const int CAPTCHA_LENGTH = 5;

    public const char SPLITER = ',';
    public const string VERIFY_CODE_ERROR = "验证码错误";
    public const string LOGIN_FAILED = "账户或密码错误";

    public const string ACTIVATE_SUCCEED = "激活成功";
    public const string ACTIVATE_FAILED = "激活失败";
    public const string PWD_CHANGED = "重置密码成功";

    public const string PLAN_TEMPLATE_DAY = "第{0}天";
    public const string PLAN_TEMPLATE_DETAIL1 = "{0}组，每组{1}次{2}";
    public const string PLAN_TEMPLATE_DETAIL1_1 = "，负荷{0}kg";
    /// <summary>
    /// 30分钟，5公里
    /// </summary>
    public const string PLAN_TEMPLATE_DETAIL2 = "{0}{1}";
    public const string PLAN_REST = "休息";

    public const string EMAIL_NOT_REGISTER = "邮箱尚未注册";
    public const string EMAIL_HAD_REGISTER = "邮箱已被注册";
    public const string EMAIL_SUBJECT_ACTIVATE = "Fit账号激活";
    public const string EMAIL_SUBJECT_CHANGEPWD = "修改Fit账号密码";

    public const string COLOR_UNFINISHED = "#1AB394";
    public const string COLOR_FINISHED = "#BEBEBE";
    public const string COLOR_TODAY = "# CDAD00";

    public const string BODY_WEIGHT = "体重";
    public const string BODY_UPPER_ARM= "上臂围";
    public const string BODY_LOWER_ARM = "小臂围";
    public const string BODY_CHEST = "胸围";
    public const string BODY_WAIST = "腰围";
    public const string BODY_HIP = "臀围";
    public const string BODY_UPPER_LEG = "大腿围";
    public const string BODY_LOWER_LEG = "小腿围";
  }

  public class SessionKeys
  {
    #region TempData
    public const string VERIFYCODE = "VerifyCode";
    public const string SESSION_EMAIL = "RegisterEmail";
    public const string EMAIL_CODE_PASSED = "IsEmailCodePassed";
    #endregion

    #region Session
    public const string CHANGEPWD_EMAIL = "ChangePwdEmail";
    #endregion
  }

  public class DBKeys
  {
    #region Common
    public const string COM_VERIFYCODE_LENGTH = "VerifyCodeLength";
    public const string COM_MAX_CYCLEDAYS = "MaxCycleDays";

    #endregion

    #region Email
    public const string EMAIL_ADDRESSES = "Addresses";
    public const string EMAIL_SMTP_SERVER = "SmtpServer";
    public const string EMAIL_SMTP_USERNAME = "SmtpUserName";
    public const string EMAIL_SMTP_PASSWORD = "SmtpPassword";
    public const string EMAIL_SMTP_EMAIL = "SmtpEmail";
    public const string EMAIL_EMAIL_TEMPLATE_ACTIVATE = "EmailTemplate_Activate";
    public const string EMAIL_EMAIL_TEMPLATE_CHANGE_PWD = "EmailTemplate_ChangePwd";
    #endregion

    #region PLAN
    public const string PLAN_REMARK_LENGTH = "PlanRemarkLength";
    public const string PLAN_MAX_GROUP = "MaxGroup";
    public const string PLAN_MAX_TIME = "MaxTime";
    public const string PLAN_MAX_SECHEDULEDAYS = "MaxSechaduleDays";
    #endregion



  }
}
