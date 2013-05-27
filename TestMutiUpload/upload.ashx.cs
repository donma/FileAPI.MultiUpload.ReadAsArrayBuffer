using System;
using System.IO;
using System.Web;

namespace TestMutiUpload
{
    /// <summary>
    /// Summary description for upload
    /// </summary>
    public class upload : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {

            //如果沒有回傳指定檔名就回傳錯誤
            if (string.IsNullOrEmpty(context.Request["filename"]))
            {
                context.Response.Write("error:filename");
                return;
            }

            //暫存在images檔案夾下面，如果沒有此檔案夾就建立
            if (!Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + "images\\"))
            {
                Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + "images\\");
            }

            //寫入檔案
            File.WriteAllBytes(AppDomain.CurrentDomain.BaseDirectory + "images\\" + context.Request["filename"] + ".jpg", StreamToBytes(context.Request.InputStream));

            //回傳寫入的檔名
            context.Response.Write(context.Request["filename"]);
        }

        /// <summary>
        /// 將Stream 轉成  Byte[]
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        private byte[] StreamToBytes(Stream stream)
        {
            stream.Position = 0;
            var buffer = new byte[stream.Length];
            for (int totalBytesCopied = 0; totalBytesCopied < stream.Length; )
                totalBytesCopied += stream.Read(buffer, totalBytesCopied, Convert.ToInt32(stream.Length) - totalBytesCopied);
            return buffer;
        }


        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}