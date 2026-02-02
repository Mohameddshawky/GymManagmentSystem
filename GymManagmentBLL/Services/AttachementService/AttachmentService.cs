using Microsoft.AspNetCore.Http;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Text;

namespace GymManagmentBLL.Services.AttachementService
{
    public class AttachmentService : IAttachmentService
    {
       
        List<string> AllowedExtensions = [".png", ".jpg", ".jpeg"];
        private readonly long MaxSize = 5 * 1024 * 1024;
        public bool Delete(string FileName, string folderName)
        {
            try
            {
                if (string.IsNullOrEmpty(FileName) || string.IsNullOrEmpty(folderName))
                {
                    return false;
                }   

                var folderpath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot",  "images", folderName,FileName);

                if (File.Exists(folderpath))
                {
                    File.Delete(folderpath);
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
           
        }

        public string? Upload(string folderName, IFormFile file)
        {
            try
            {
                if (folderName is null || file is null || file.Length == 0)
                {
                    return null;
                }
                var exe = Path.GetExtension(file.FileName).ToLower();
                if (!AllowedExtensions.Contains(exe) || file.Length > MaxSize)
                {
                    return null;
                }
                var folderpath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", folderName);
                if (!Directory.Exists(folderpath))
                {
                    Directory.CreateDirectory(folderpath);
                }

                var filename = $"{Guid.NewGuid()}{exe}";

                var filepath = Path.Combine(folderpath, filename);

                using FileStream f = new FileStream(filepath, FileMode.Create);

                file.CopyTo(f);
                return filename;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
