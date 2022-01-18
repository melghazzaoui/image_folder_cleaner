using System;
using System.Drawing;

namespace ImgComparer.Utils
{
    class ImageUtil
    {
        private static Bitmap resizeDisposeBitmap(Bitmap bmp, int newWidth, int newHeight)
        {
            Bitmap bmp1 = new Bitmap(bmp, new Size(newWidth, newHeight));
            bmp.Dispose();
            return bmp1;
        }

        public static bool filesEqual(string file1, string file2)
        {
            Bitmap bmp1 = null;
            Bitmap bmp2 = null;
            try
            {
                bmp1 = (new Bitmap(file1));
                bmp2 = new Bitmap(file2);
                if (bmp1.Width == bmp2.Width && bmp1.Height == bmp2.Height)
                {
                    double ar = ((double)bmp1.Width) / bmp1.Height;
                    int newWidth = 400;
                    int newHeight = (int)(((double)newWidth) / ar);
                    bmp1 = resizeDisposeBitmap(bmp1, newWidth, newHeight);
                    bmp2 = resizeDisposeBitmap(bmp2, newWidth, newHeight);
                    for (int y = 0; y < bmp1.Height; ++y)
                    {
                        for (int x = 0; x < bmp1.Width; ++x)
                        {
                            Color pix1 = bmp1.GetPixel(x, y);
                            Color pix2 = bmp2.GetPixel(x, y);
                            if (!pix1.Equals(pix2))
                            {
                                return false;
                            }
                        }
                    }
                    return true;
                }
            }
            catch (Exception exc)
            {
                throw exc;
            }
            finally
            {
                if (bmp1 != null)
                {
                    bmp1.Dispose();
                }
                if (bmp2 != null)
                {
                    bmp2.Dispose();
                }
            }

            return false;
        }
    }
}
