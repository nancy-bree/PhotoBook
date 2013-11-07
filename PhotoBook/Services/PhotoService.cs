﻿using System;
using System.IO;
using System.Web;
using System.Web.Helpers;
using PhotoBook.Properties;

namespace PhotoBook.Services
{
    public static class PhotoService
    {
        public static string SavePhoto(HttpPostedFileBase file)
        {
            string mainName = Guid.NewGuid().ToString();
            string filename = mainName + Path.GetExtension(file.FileName);
            string path = GetPhotoPath(filename);

            file.SaveAs(path);
            SaveThumbnail(file, mainName);
            return filename;
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
                        ApplyAutocontrast(tmpPath, sourcePath);
                        break;
                    }
                case Effect.Grayscale:
                    {
                        ApplyGrayscale(tmpPath, sourcePath);
                        break;
                    }
                case Effect.Sepia:
                    {
                        ApplySepia(tmpPath, sourcePath);
                        break;
                    }
                case Effect.None:
                    {
                        DeleteEffect(filename);
                        break;
                    }
            }
        }

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

        private static void ApplySepia(string path, string newPath)
        {
            var photo = PhotoEditor.ToSepia(path, 20);
            photo.Save(newPath, System.Drawing.Imaging.ImageFormat.Jpeg);
            photo.Dispose();
        }

        private static void ApplyGrayscale(string path, string newPath)
        {
            var photo = PhotoEditor.ToGrayscale(path);
            photo.Save(newPath, System.Drawing.Imaging.ImageFormat.Jpeg);
            photo.Dispose();
        }

        private static void ApplyAutocontrast(string path, string newPath)
        {
            var photo = PhotoEditor.ApplyContrast(path, 20);
            photo.Save(newPath, System.Drawing.Imaging.ImageFormat.Jpeg);
            photo.Dispose();
        }

        private static string GetPhotoPath(string filename)
        {
            return Path.Combine(HttpContext.Current.Server.MapPath(PhotoBook.Properties.Settings.Default.UserUploads), filename);
        }

        private static int DeleteEffect(string filename)
        {
            string tmpFilename = "_tmp_" + filename;
            string tmpPath = GetPhotoPath(tmpFilename);
            string sourcePath = GetPhotoPath(filename);

            if (File.Exists(tmpPath))
            {
                //File.Delete(sourcePath);
                File.Move(tmpPath, sourcePath);
                File.Delete(tmpPath);
                return 1;
            }
            return 0;
        }
    }
}