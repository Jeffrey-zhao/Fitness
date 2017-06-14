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
using System.Web;

namespace Fit.Common
{
  public class SaveImgInCloud
  {
    private const string AK = "mPRqfvRKWQoGB2X0SOluytNuTA6Rn41K7XQlDM7c";
    private const string SK = "hM_YswqE79hwSTb4TVPZf7exG5RTclXeI53APU3z";
    private const string BUCKET = "fitness";
    public static void Save(HttpPostedFileBase file,HttpServerUtilityBase server)
    {
      Mac mac = new Mac(AK, SK);
      string bucket = BUCKET;
      Config.AutoZone(AK, bucket, false);
      string saveKey = "1.txt";
      // 上传策略，参见 
      // https://developer.qiniu.com/kodo/manual/put-policy
      PutPolicy putPolicy = new PutPolicy();
      // 如果需要设置为"覆盖"上传(如果云端已有同名文件则覆盖)，请使用 SCOPE = "BUCKET:KEY"
      putPolicy.Scope = bucket + ":" + saveKey;
      // 上传策略有效期(对应于生成的凭证的有效期)          
      putPolicy.SetExpires(3600);
      // 上传到云端多少天后自动删除该文件，如果不设置（即保持默认默认）则不删除
      //putPolicy.DeleteAfterDays = 1;
      // 生成上传凭证，参见
      // https://developer.qiniu.com/kodo/manual/upload-token            
      string jstr = putPolicy.ToJsonString();
      string token = Auth.CreateUploadToken(mac, jstr);
      //string dToken = Auth.CreateDownloadToken(mac, "http://orivq9ow0.bkt.clouddn.com/1.txt");

      FormUploader fu = new FormUploader();
      var result = fu.UploadStream(file.InputStream, saveKey, token);

      //var download = DownloadManager.CreateSignedUrl(mac, "http://orivq9ow0.bkt.clouddn.com/1.txt");

      //var r = DownloadManager.Download(download, server.MapPath("~/2.txt"));
    }
  }
}
