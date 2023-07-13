using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace Discord.Modules
{
    public static class ScreenExService
    {
        public static Bitmap GetFullScreenCapture()
        {
            int screenWidth = Screen.PrimaryScreen.Bounds.Width;
            int screenHeight = Screen.PrimaryScreen.Bounds.Height;

            Bitmap bmpScreenshot = new Bitmap(screenWidth, screenHeight, PixelFormat.Format24bppRgb);
            Graphics g = Graphics.FromImage(bmpScreenshot);

            IntPtr dc1 = WindowAPIs.GetDC(WindowAPIs.GetDesktopWindow());
            IntPtr dc2 = g.GetHdc();

            //Main drawing, copies the screen to the bitmap
            //last number is the copy constant
            WindowAPIs.BitBlt(dc2, 0, 0, screenWidth, screenHeight, dc1, 0, 0, 13369376);

            //Clean up
            WindowAPIs.ReleaseDC(WindowAPIs.GetDesktopWindow(), dc1);
            g.ReleaseHdc(dc2);
            g.Dispose();

            return bmpScreenshot;
        }

        public static Bitmap GetScreenCapture(Rectangle fov)
        {
            var screenCopy = new Bitmap(fov.Width, fov.Height, PixelFormat.Format24bppRgb);

            using (var gdest = Graphics.FromImage(screenCopy))

            using (var gsrc = Graphics.FromHwnd(IntPtr.Zero))
            {
                var hSrcDc = gsrc.GetHdc();
                var hDc = gdest.GetHdc();
                WindowAPIs.BitBlt(hDc, 0, 0, fov.Width, fov.Height, hSrcDc, fov.X, fov.Y, (int)CopyPixelOperation.SourceCopy); // (int)CopyPixelOperation.SourceCopy == 13369376

                gdest.ReleaseHdc();
                gsrc.ReleaseHdc();
            }

            //for (int y = 0; y < screenCopy.Height; y++)
            //{
            //    for (int x = 0; x < screenCopy.Width; x++)
            //    {
            //        Color c = screenCopy.GetPixel(x, y);

            //        if (ColorWithinRange(c))
            //        {

            //            screenCopy.SetPixel(x, y, Color.Red);
            //        }


            //    }
            //}

            //screenCopy.Save(@"C:\Users\MSI\Desktop\CarrySharp\CarrySharp\bin\Debug\bbcbb.jpeg");
            return screenCopy;
        }

        //private static Color _from = Color.FromArgb(76, 244, 240); // ColorTranslator.FromHtml("#4cf4f0");
        //private static Color _to = Color.FromArgb(112, 255, 255); // ColorTranslator.FromHtml("#98ffff");
        //static bool ColorWithinRange(Color c)
        //{
        //    return
        //       (_from.R <= c.R && c.R <= _to.R) &&
        //       (_from.G <= c.G && c.G <= _to.G) &&
        //       (_from.B <= c.B && c.B <= _to.B);
        //}
    }

}
