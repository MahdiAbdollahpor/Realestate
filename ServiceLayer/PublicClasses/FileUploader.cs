using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.PublicClasses
{
    public interface IFileUploader
    {
        string UploadFile(IFormFile files, string path);
    }

    public class FileUploader : IFileUploader
    {
        public string UploadFile(IFormFile files, string path)
        {
            string fileName = "";
            string rootPath = Directory.GetCurrentDirectory() + "\\wwwroot";

           
                fileName = Guid.NewGuid().ToString().Replace("-", "").ToLower() + Path.GetExtension(files.FileName);
                string currentPath = rootPath + path + fileName;

                using (var fs = new FileStream(currentPath, FileMode.Create))
                {
                files.CopyTo(fs);
                }

                

            
            return fileName;
        }
    }
}
