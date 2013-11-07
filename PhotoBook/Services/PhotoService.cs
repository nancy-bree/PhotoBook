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

        private static void SaveSepia(string path, string newPath)
        {
            PhotoEditor.ToSepia(path, 20).Save(newPath, System.Drawing.Imaging.ImageFormat.Jpeg);
        }

        private static void SaveGrayscale(string path, string newPath)
        {
            PhotoEditor.ToGrayscale(path).Save(newPath, System.Drawing.Imaging.ImageFormat.Jpeg);
        }

        private static void SaveContrast(string path, string newPath)
        {
            PhotoEditor.ApplyContrast(path, 20).Save(newPath, System.Drawing.Imaging.ImageFormat.Jpeg);
        }

        private static string GetPhotoPath(string filename)
        {
            return Path.Combine(HttpContext.Current.Server.MapPath(PhotoBook.Properties.Settings.Default.UserUploads), filename);
        }

        public static void ApplyEffect(Effect effect, string filename)
        {
            string tmpFilename = "_tmp_" + filename;
            string tmpPath = GetPhotoPath(tmpFilename);
            string sourcePath = GetPhotoPath(filename);

            if (!File.Exists(tmpPath))
            {
                File.Copy(sourcePath, tmpPath);
            }
            File.Delete(sourcePath);

            switch (effect)
            {
                case Effect.Autocontrast:
                    {
                        SaveContrast(tmpPath, sourcePath);
                        break;
                    }
                case Effect.Monochrome:
                    {
                        SaveGrayscale(tmpPath, sourcePath);
                        break;
                    }
                case Effect.Sepia:
                    {
                        SaveSepia(tmpPath, sourcePath);
                        break;
                    }
            }
        }

        public static void DeleteEffect(string filename)
        {
            string tmpFilename = "_tmp_" + filename;
            string tmpPath = GetPhotoPath(tmpFilename);
            string sourcePath = GetPhotoPath(filename);

            File.Delete(sourcePath);
            File.Move(tmpPath, sourcePath);
            File.Delete(tmpPath);
        }
    }
}