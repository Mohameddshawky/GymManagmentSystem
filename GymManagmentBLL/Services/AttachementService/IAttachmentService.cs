using System;
using System.Collections.Generic;
using System.Text;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
namespace GymManagmentBLL.Services.AttachementService
{
    public interface IAttachmentService
    {
        public string? Upload(string folderName, IFormFile file);
        public bool Delete(string FileName,string folderName);
    }
}
