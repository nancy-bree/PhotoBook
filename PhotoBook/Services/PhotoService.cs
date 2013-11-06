using System;
using System.IO;
using System.Web;
using System.Web.Helpers;
using PhotoBook.Properties;

namespace PhotoBook.Services
{
    public static class PhotoService
    {
        private static void SaveThumbnail(HttpPostedFileBase file, string name)
        {
            string thumbnailFilename = "_thumbnail_" + name + Path.GetExtension(file.FileName);
            string thumbnailPath = GetPhotoPath(thumbnailFilename);

            WebImage img = new WebImage(file.InputStream);
            float aspectRatio = (float)img.Width / (float)img.Height;
            int thumbnailHeight = Convert.ToInt32(Settings.Default.ThumbnailWidth / aspectRatio);
            img.Resize(Settings.Default.ThumbnailWidth, thumbnailHeight).Crop(1, 1);
            img.Save(thumbnailPath);
        }

        public static string SavePhoto(HttpPostedFileBase file)
        {
            string mainName = Guid.NewGuid().ToString();
            string filename = mainName + Path.GetExtension(file.FileName);
            string path = GetPhotoPath(filename);
            file.SaveAs(path);
            SaveThumbnail(file, mainName);
            /*SaveSepia(path, mainName);
            SaveGrayscale(path, mainName);
            SaveContrast(path, mainName);*/
            return filename;
        }

        private static void SaveSepia(string path, string name)
        {
            string sepiaFilename = "_sepia_" + name + Path.GetExtension(path);
            string sepiaPath = GetPhotoPath(sepiaFilename);
            PhotoEditor.ToSepia(path, 20).Save(sepiaPath, System.Drawing.Imaging.ImageFormat.Jpeg);
        }

        private static void SaveGrayscale(string path, string name)
        {
            string grayscaleFilename = "_gs_" + name + Path.GetExtension(path);
            string grayscalePath = GetPhotoPath(grayscaleFilename);
            PhotoEditor.ToGrayscale(path).Save(grayscalePath, System.Drawing.Imaging.ImageFormat.Jpeg);
        }

        private static void SaveContrast(string path, string name)
        {
            string contrastFilename = "_contrast_" + name + Path.GetExtension(path);
            string contrastPath = GetPhotoPath(contrastFilename);
            PhotoEditor.ApplyContrast(path, 20).Save(contrastPath, System.Drawing.Imaging.ImageFormat.Jpeg);
        }

        private static string GetPhotoPath(string filename)
        {
            return Path.Combine(HttpContext.Current.Server.MapPath(PhotoBook.Properties.Settings.Default.UserUploads), filename);
        }
    }
}