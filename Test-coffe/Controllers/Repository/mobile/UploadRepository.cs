using LazZiya.ImageResize;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Test_coffe.Controllers.mobile.Services;

namespace Test_coffe.Controllers.mobile.Repository
{
    public class UploadRepository : IUploadImage
    {
        public string upload(IHostingEnvironment _hostingEnvironment, IFormFile httpPostedFile, string folderName, int id)
        {

            if (httpPostedFile != null)
            {
                string directory = Path.Combine(_hostingEnvironment.WebRootPath, "uploads/" + folderName);
                if (!Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }

                string subDirectory = directory + "/" + id;
                if (!Directory.Exists(subDirectory))
                {
                    Directory.CreateDirectory(subDirectory);
                }

                string uniqueFileName = null;
                string uniqueFileName1 = null;
                string uploadsFolder = subDirectory;
                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                // string uploadsFolder = Path.Combine(_hostingEnvironment.WebRootPath, "uploads/avatar");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + httpPostedFile.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                httpPostedFile.CopyTo(new FileStream(filePath, FileMode.Create));

                // shop.avatar = uniqueFileName;

                //string uploadsFolder1 = subDirectory + "/thumb";
                //if (!Directory.Exists(uploadsFolder1))
                //{
                //    Directory.CreateDirectory(uploadsFolder1);
                //}
                //string filePath1 = Path.Combine(uploadsFolder1, uniqueFileName);
                //var input_Image_Path = filePath;
                // var output_Image_Path = Path.Combine(_hostingEnvironment.WebRootPath, "uploads/thumb");
                uniqueFileName1 = Guid.NewGuid().ToString() + "_" + httpPostedFile.FileName;
                using (var stream = httpPostedFile.OpenReadStream())
                {
                    var uploadedImage = Image.FromStream(stream);
                    var x = uploadedImage.Width;
                    var y = uploadedImage.Height;
                    if (x > y)
                    {
                        x = 175;
                        y = y / x * 175;
                    }
                    else
                    {
                        y = 150;
                        x = x / y * 150;
                    }
                    //returns Image file
                    var img = ImageResize.Scale(uploadedImage, x, y);
                    img.SaveAs(uploadsFolder + "/" + uniqueFileName1);
                }

                return "{" + '"' + "avatar" + '"' + ":" + '"' + uniqueFileName + '"' + "," + '"' + "thumb" + '"' + ":" + '"' + uniqueFileName1 + '"' + "}";
                //shop.thumb = uniqueFileName;
            }
            else
            {
                string directory = Path.Combine(_hostingEnvironment.WebRootPath, "uploads/" + folderName);
                if (!Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }

                string subDirectory = directory + "/" + id;
                if (!Directory.Exists(subDirectory))
                {
                    Directory.CreateDirectory(subDirectory);
                }

                string uploadsFolder = subDirectory;
                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                //string uploadsFolder1 = subDirectory + "/thumb";
                //if (!Directory.Exists(uploadsFolder1))
                //{
                //    Directory.CreateDirectory(uploadsFolder1);
                //}

                string n = id.ToString();
                string sourceDir = Path.Combine(_hostingEnvironment.WebRootPath, "uploads");
                string backupDir = Path.Combine(_hostingEnvironment.WebRootPath, "uploads/" + folderName + n);
                string uniqueFileName = null;
                uniqueFileName = "no-image.png";
                //System.IO.File.Copy(Path.Combine(sourceDir, "default.png"), Path.Combine(uploadsFolder, uniqueFileName), true);
                //System.IO.File.Copy(Path.Combine(sourceDir, "default.png"), Path.Combine(uploadsFolder1, uniqueFileName), true);
                //shop.avatar = uniqueFileName;
                //shop.thumb = uniqueFileName;
                return "{" + '"' + "avatar" + '"' + ":" + '"' + uniqueFileName + '"' + "," + '"' + "thumb" + '"' + ":" + '"' + uniqueFileName + '"' + "}";
            }
        }

        public string changeImage(IHostingEnvironment _hostingEnvironment, IFormFile httpPostedFile, string folderName, int id)
        {
            if (httpPostedFile != null)
            {
                string directory = Path.Combine(_hostingEnvironment.WebRootPath, "uploads/" + folderName + "/" + id);

                if (!Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }

                string uniqueFileName = null;
                string uniqueFileName1 = null;

                string uploadsFolder = directory;
                uniqueFileName = Guid.NewGuid().ToString() + "_" + httpPostedFile.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                httpPostedFile.CopyTo(new FileStream(filePath, FileMode.Create));
                // shop.avatar = uniqueFileName;

                //string uploadsFolder1 = directory + "/thumb";
                //string filePath1 = Path.Combine(uploadsFolder1, uniqueFileName);
                //var input_Image_Path = filePath;
                // var output_Image_Path = Path.Combine(_hostingEnvironment.WebRootPath, "uploads/thumb");
                uniqueFileName1 = Guid.NewGuid().ToString() + "_" + httpPostedFile.FileName;
                using (var stream = httpPostedFile.OpenReadStream())
                {
                    var uploadedImage = Image.FromStream(stream);
                    var x = uploadedImage.Width;
                    var y = uploadedImage.Height;
                    if (x > y)
                    {
                        x = 175;
                        y = y / x * 175;
                    }
                    else
                    {
                        y = 150;
                        x = x / y * 150;
                    }
                    //returns Image file
                    var img = ImageResize.Scale(uploadedImage, x, y);

                    img.SaveAs(uploadsFolder + "/" + uniqueFileName1);
                }

                return "{" + '"' + "avatar" + '"' + ":" + '"' + uniqueFileName + '"' + "," + '"' + "thumb" + '"' + ":" + '"' + uniqueFileName1 + '"' + "}";
                //shop.thumb = uniqueFileName;
            }else
              return "";
        }
    }

}
