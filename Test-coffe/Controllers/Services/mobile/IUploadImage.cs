using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Test_coffe.Controllers.mobile.Services
{
    public interface IUploadImage
    {
        string upload(IHostingEnvironment _hostingEnvironment, IFormFile httpPostedFile, string folderName, int id);

        string changeImage(IHostingEnvironment _hostingEnvironment, IFormFile httpPostedFile, string folderName, int id);
    }
}
