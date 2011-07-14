using System;
using System.IO;
using System.Web;
using Tridion.Extensions.DynamicDelivery.ContentModel;
using Tridion.Extensions.DynamicDelivery.ContentModel.Factories;
using System.Web.Caching;
using System.Configuration;

namespace Tridion.Extensions.DynamicDelivery.Mvc.Handlers
{
    public class BinaryHandler : IHttpHandler
    {
        #region static members
        private const string BinaryExtensionsConfigKey = "BinaryFileExtensions";
        private const string BinaryHandlerCachingKey = "BinaryHandlerCaching";

        #endregion

        public void ProcessRequest(HttpContext context)
        {
            IBinary binary = null;

            string url = context.Request.Path;
            Cache cache = HttpContext.Current.Cache;

            if (cache[url] != null)
            {
                binary = (IBinary)cache[url];
            }
            else
            {
                binary = BinaryFactory.FindBinary(context.Request.Path);
                int cacheSetting = Convert.ToInt32(ConfigurationManager.AppSettings[BinaryHandlerCachingKey]);
                cache.Insert(url, binary, null, DateTime.Now.AddSeconds(cacheSetting), TimeSpan.Zero);
            }

            var localPath = ConvertUrl(context.Request.Path, context);
            var info = new FileInfo(localPath);

            if (binary == null)
            {
                // there appears to be no binary present in Tridion
                // now we must check if there is still an (old) cached copy on the local file system
                if (info.Exists)
                    RemoveFromFS(info);

                // that's all for us, the file is not on the FS, so 
                // the default FileHandler will cause a 404 exception
                context.Response.StatusCode = 404;
                context.Response.End();
                return;
            }

            TimeSpan ts = binary.LastPublishedDate.Subtract(info.LastWriteTimeUtc);
            if (ts.TotalMilliseconds > 0 || !info.Exists)
                WriteToFS(binary, info);
            
            FillResponse(context.Response, info);
        }

        private void FillResponse(HttpResponse response, FileInfo info)
        {
            response.Clear();

            byte[] imageData = File.ReadAllBytes(info.FullName);
            response.ContentType = GetContentType(info.FullName);
            response.BinaryWrite(imageData);
        }

        public bool IsReusable
        {
            get { return false; }
        }

        #region private/protected
        private IBinaryFactory binaryFactory = null;
        protected IBinaryFactory BinaryFactory
        {
            get
            {
                if (binaryFactory == null)
                {
                    binaryFactory = ServiceLocator.GetInstance<IBinaryFactory>();
                }
                return binaryFactory;
            }
        }
        private string ConvertUrl(string url, HttpContext context)
        {
            return context.Server.MapPath(url);
        }
        private void WriteToFS(IBinary binary, FileInfo info)
        {
            if (info.Exists)
            {
                RemoveFromFS(info);
            }

            Stream stream = null;
            try
            {
                stream = new FileStream(info.FullName, FileMode.Create);
                using (var writer = new BinaryWriter(stream))
                {
                    stream = null;
                    writer.Write(binary.BinaryData);
                }
            }
            finally
            {
                if (stream != null)
                    stream.Dispose();
            }
        }

        private void RemoveFromFS(FileInfo info)
        {
            File.Delete(info.FullName);
        }

        private string GetContentType(string fileName)
        {
            string contentType = "application/octetstream";
            string ext = System.IO.Path.GetExtension(fileName).ToLower();

            Microsoft.Win32.RegistryKey registryKey = Microsoft.Win32.Registry.ClassesRoot.OpenSubKey(ext);

            if (registryKey != null && registryKey.GetValue("Content Type") != null)
                contentType = registryKey.GetValue("Content Type").ToString();

            return contentType;
        }

        #endregion
    }
}
