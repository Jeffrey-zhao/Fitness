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
    public const string CLOUD_DOMAIN = "http://orivq9ow0.bkt.clouddn.com/";
    public const string TEXT_COMBINE = "综合训练";
    public const int CAPTCHA_LENGTH = 5;

    public const char SPLITER = ',';
    public const string EMAIL_NOT_REGISTER = "邮箱尚未注册";
    public const string EMAIL_HAD_REGISTER = "邮箱已被注册";
    public const string VERIFY_CODE_ERROR = "验证码错误";
    public const string LOGIN_FAILED = "账户或密码错误";

    #region Common
    public const string ACTIVATE_SUCCEED = "激活成功";
    public const string ACTIVATE_FAILED = "激活失败";
    public const string PWD_CHANGED = "重置密码成功";
    #endregion

    public const string EMAIL_SUBJECT_ACTIVATE = "Fit账号激活";
    public const string EMAIL_SUBJECT_CHANGEPWD = "修改Fit账号密码";
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

  }
}
