using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace CrossCutting.Helpers
{
    public class DownloadHelper
    {
        public static async Task<(MemoryStream, string, string)> GetDataToDownload(string path)
        {
            var memory = new MemoryStream();
            try
            {
                using (var stream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    await stream.CopyToAsync(memory);
                }
            }
            catch (IOException ex)
            {

                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;

            }
            memory.Position = 0;
            return (memory, GetContentType(path), Path.GetFileName(path));
        }

        private static string GetContentType(string path)
        {
            var types = GetMimeTypes();
            var ext = Path.GetExtension(path).ToLowerInvariant();
            return types[ext];
        }

        private static Dictionary<string, string> GetMimeTypes()
        {
            return new Dictionary<string, string>
            {
                {".txt", "text/plain"},
                {".pdf", "application/pdf"},
                {".doc", "application/vnd.ms-word"},
                {".docx", "application/vnd.ms-word"},
                {".xls", "application/vnd.ms-excel"},
                {".xlsx", "application/vnd.openxmlformatsofficedocument.spreadsheetml.sheet"},
                {".png", "image/png"},
                {".jpg", "image/jpeg"},
                {".jpeg", "image/jpeg"},
                {".gif", "image/gif"},
                {".csv", "text/csv"},
                {".mp4", "video/mp4"},
                {".vlc", "application/x-vlc-plugin"},
                {".avi", "video/x-msvideo"},
                {".wmv", "	video/x-ms-wmv"},
            };
        }
    }

}
