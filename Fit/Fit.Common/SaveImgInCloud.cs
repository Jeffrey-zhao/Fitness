using Qiniu.Common;
using Qiniu.Http;
using Qiniu.IO;
using Qiniu.IO.Model;
using Qiniu.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Fit.Common
{
  public class SaveImgInCloud
  {
    public static void Save()
    {
      Mac mac = new Mac("mPRqfvRKWQoGB2X0SOluytNuTA6Rn41K7XQlDM7c"
                                            , "hM_YswqE79hwSTb4TVPZf7exG5RTclXeI53APU3z");
      string bucket = "fitness";
      Config.AutoZone("mPRqfvRKWQoGB2X0SOluytNuTA6Rn41K7XQlDM7c", bucket, true);
      string saveKey = "1.txt";
      string localFile = "D:\\temp.txt";
      // 上传策略，参见 
      // https://developer.qiniu.com/kodo/manual/put-policy
      PutPolicy putPolicy = new PutPolicy();
      // 如果需要设置为"覆盖"上传(如果云端已有同名文件则覆盖)，请使用 SCOPE = "BUCKET:KEY"
      // putPolicy.Scope = bucket + ":" + saveKey;
      putPolicy.Scope = bucket;
      // 上传策略有效期(对应于生成的凭证的有效期)          
      putPolicy.SetExpires(3600);
      // 上传到云端多少天后自动删除该文件，如果不设置（即保持默认默认）则不删除
      putPolicy.DeleteAfterDays = 1;
      // 生成上传凭证，参见
      // https://developer.qiniu.com/kodo/manual/upload-token            
      string jstr = putPolicy.ToJsonString();
      string token = Auth.CreateUploadToken(mac, jstr);
      UploadManager um = new UploadManager();
      HttpResult result = um.UploadFile(localFile, saveKey, token);

    }
  }
}
