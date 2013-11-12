using System;
using System.Drawing;


namespace PhotoBook.Services
{
    /// <summary>
    /// 
    /// </summary>
    public static class PhotoEditor
    {
        public static Bitmap ToSepia(string photoPath, int depth)
        {
            Bitmap photo = new Bitmap(photoPath);

            int A, R, G, B;
            Color pixelColor;

            for (int y = 0; y < photo.Height; y++)
            {
                for (int x = 0; x < photo.Width; x++)
                {
                    pixelColor = photo.GetPixel(x, y);
                    A = pixelColor.A;
                    R = (int)((0.299 * pixelColor.R) + (0.587 * pixelColor.G) + (0.114 * pixelColor.B));
                    G = B = R;

                    R += (depth * 2);
                    if (R > 255)
                    {
                        R = 255;
                    }
                    G += depth;
                    if (G > 255)
                    {
                        G = 255;
                    }

                    photo.SetPixel(x, y, Color.FromArgb(A, R, G, B));
                }
            }
            return photo;
        }

        public static Bitmap ToGrayscale(string photoPath)
        {
            Bitmap photo = new Bitmap(photoPath);

            byte A, R, G, B;
            Color pixelColor;

            for (int y = 0; y < photo.Height; y++)
            {
                for (int x = 0; x < photo.Width; x++)
                {
                    pixelColor = photo.GetPixel(x, y);
                    A = pixelColor.A;
                    R = (byte)((0.299 * pixelColor.R) + (0.587 * pixelColor.G) + (0.114 * pixelColor.B));
                    G = B = R;

                    photo.SetPixel(x, y, Color.FromArgb((int)A, (int)R, (int)G, (int)B));
                }
            }
            return photo;
        }

        public static Bitmap ApplyContrast(string photoPath, double contrast)
        {
            Bitmap photo = new Bitmap(photoPath);

            double A, R, G, B;

            Color pixelColor;

            contrast = (100.0 + contrast) / 100.0;
            contrast *= contrast;

            for (int y = 0; y < photo.Height; y++)
            {
                for (int x = 0; x < photo.Width; x++)
                {
                    pixelColor = photo.GetPixel(x, y);
                    A = pixelColor.A;

                    R = pixelColor.R / 255.0;
                    R -= 0.5;
                    R *= contrast;
                    R += 0.5;
                    R *= 255;

                    if (R > 255)
                    {
                        R = 255;
                    }
                    else if (R < 0)
                    {
                        R = 0;
                    }

                    G = pixelColor.G / 255.0;
                    G -= 0.5;
                    G *= contrast;
                    G += 0.5;
                    G *= 255;
                    if (G > 255)
                    {
                        G = 255;
                    }
                    else if (G < 0)
                    {
                        G = 0;
                    }

                    B = pixelColor.B / 255.0;
                    B -= 0.5;
                    B *= contrast;
                    B += 0.5;
                    B *= 255;
                    if (B > 255)
                    {
                        B = 255;
                    }
                    else if (B < 0)
                    {
                        B = 0;
                    }

                    photo.SetPixel(x, y, Color.FromArgb((int)A, (int)R, (int)G, (int)B));
                }
            }
            return photo;
        }
    }
}