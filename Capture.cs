using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;

namespace Suss
{
    internal class Capture
    {
        private Bitmap bmpScreenshot;
        private Graphics gfxScreenshot;
        private string basepath;
        private int width;
        private int height;

        public Capture()
        {
            double scaling = 1.25;
            width = (int)Math.Floor(Screen.PrimaryScreen.Bounds.Size.Width * scaling);
            height = (int)Math.Floor(Screen.PrimaryScreen.Bounds.Size.Height * scaling);
            bmpScreenshot = new Bitmap(width, height, PixelFormat.Format32bppArgb);
            gfxScreenshot = Graphics.FromImage(bmpScreenshot);
            basepath = @"C:\Screenshots\";
            if (!Directory.Exists(basepath))
            {
                Directory.CreateDirectory(basepath);
            }
        }

        public void Screenshot()
        {
            string filename = $"{basepath}screenshot_{DateTime.Now:yyyyMMdd-HHmmss}.png";

            gfxScreenshot.CopyFromScreen(Screen.PrimaryScreen.Bounds.X,
                                        Screen.PrimaryScreen.Bounds.Y,
                                        0,
                                        0,
                                        new Size(width, height),
                                        CopyPixelOperation.SourceCopy);

            bmpScreenshot.Save(filename, ImageFormat.Png);
        }

        internal void LogError(string exceptionMessage)
        {
            string filename = $"{basepath}errorlog_{DateTime.Now:yyyyMMdd-HHmmss}.txt";
            using(StreamWriter writer = new StreamWriter(filename))
            {
                writer.Write(exceptionMessage);
            }
        }
    }
}