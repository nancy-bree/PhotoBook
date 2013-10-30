using System;
using System.IO;
using System.Web;
using System.Web.Helpers;

namespace PhotoBook.Web.Services
{
    public static class PhotoUploadService
    {
        private static void SaveThumbnail(HttpPostedFileBase file, string name)
        {
            string thumbnailFilename = name + "_thumbnail" + Path.GetExtension(file.FileName);
            string thumbnailPath = GetPhotoPath(thumbnailFilename);

            WebImage img = new WebImage(file.InputStream);
            int thumbnailWidth = 320;
            float aspectRatio = (float)img.Width / (float)img.Height;
            int thumbnailHeight = Convert.ToInt32(thumbnailWidth / aspectRatio);
            img.Resize(thumbnailWidth, thumbnailHeight);
            img.Save(thumbnailPath);
        }

        public static string SavePhoto(HttpPostedFileBase file)
        {
            string mainName = Guid.NewGuid().ToString();
            string filename = mainName + Path.GetExtension(file.FileName);
            string path = GetPhotoPath(filename);
            file.SaveAs(path);
            SaveThumbnail(file, mainName);
            return filename;
        }

        /*public static string SaveImage(HttpPostedFileBase file, Filter filter)
        {
            string filename;
            switch (filter)
            {
                case Filter.Contrast:
                    {
                        filename = Guid.NewGuid().ToString() + "_contrast" + Path.GetExtension(file.FileName);
                        string path = GetImagePath(filename);
                        file.SaveAs(path);
                        break;
                    }
                case Filter.Monochrome:
                    {
                        filename = Guid.NewGuid().ToString() + "_monochrome" + Path.GetExtension(file.FileName);
                        break;
                    }
                case Filter.Sepia:
                    {
                        filename = Guid.NewGuid().ToString() + "_sepia" + Path.GetExtension(file.FileName);
                        break;
                    }
            }
            return filename;
        }*/

        private static string GetPhotoPath(string filename)
        {
            return Path.Combine(HttpContext.Current.Server.MapPath(PhotoBook.Properties.Settings.Default.UserUploads), filename);
        }
    }
}