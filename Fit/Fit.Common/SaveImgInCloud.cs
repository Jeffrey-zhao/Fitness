using Qiniu.Common;
using Qiniu.Http;
using Qiniu.IO;
using Qiniu.IO.Model;
using Qiniu.Util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Fit.Common
{
  public class SaveImgInCloud
  {
    private const string AK = "mPRqfvRKWQoGB2X0SOluytNuTA6Rn41K7XQlDM7c";
    private const string SK = "hM_YswqE79hwSTb4TVPZf7exG5RTclXeI53APU3z";
    private const string BUCKET = "fitness";
    public static string Save(HttpPostedFileBase file)
    {
      Mac mac = new Mac(AK, SK);
      string bucket = BUCKET;
      Config.AutoZone(AK, bucket, false);
      var extension = Path.GetExtension(file.FileName);
      var fileMD5 = CommonHelper.CalcMD5(file.InputStream);
      file.InputStream.Position = 0;
      var saveKey = fileMD5 + extension;

      PutPolicy putPolicy = new PutPolicy()
      {
        Scope = bucket //+ ":" + saveKey
      };
      putPolicy.SetExpires(3600);
      string jstr = putPolicy.ToJsonString();
      string token = Auth.CreateUploadToken(mac, jstr);

      FormUploader fu = new FormUploader();
      var result = fu.UploadStream(file.InputStream, saveKey, token);

      return saveKey;
    }
  }
}
