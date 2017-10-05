using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.Web;

namespace SlickCMS.Core
{
    /// <summary>
    /// SlickCMS implementation of Captcha: Completely Automated Public Turing test to tell Computers and Humans Apart
    /// http://en.wikipedia.org/wiki/CAPTCHA
    /// </summary>
    public class Captcha
    {
        public string Text { get; set; }

        public void Generate()
        {
            Generate(7);
        }

        public void Generate(int length)
        {
            this.Text = System.Guid.NewGuid().ToString().Substring(0, length);
        }

        public void RenderCaptcha()
        {
            // create bitmap
            Bitmap bmp = new Bitmap(150, 50, PixelFormat.Format24bppRgb);

            // create/setup canvas for bitmap
            Graphics canvas = Graphics.FromImage(bmp);
            canvas.PageUnit = GraphicsUnit.Millimeter;
            canvas.TextRenderingHint = TextRenderingHint.AntiAlias;

            // create local colors
            Color orange = ColorTranslator.FromHtml("#FF9900");
            Color blue = ColorTranslator.FromHtml("#0099FF");
            Color white = Color.White;

            // set canvas background
            canvas.Clear(blue);

            // add to canvas
            Font font = new Font("Arial", 20, FontStyle.Bold);
            SolidBrush brush = new SolidBrush(white);
            canvas.DrawString(this.Text, font, brush, 3, 3);

            // render label
            var context = System.Web.HttpContext.Current;
            context.Response.ContentType = "image/jpeg";
            bmp.Save(context.Response.OutputStream, ImageFormat.Jpeg);

            // destroy objects
            bmp.Dispose();
            canvas.Dispose();
            font.Dispose();
            brush.Dispose();
        }
    }
}
