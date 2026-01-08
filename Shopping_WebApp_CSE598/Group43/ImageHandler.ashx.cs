using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;

/*     
 *      CSE 598 - Assigment 5 & 6
 *                HTTPHandler
 *       Author - Mark Adan
 *      version - 1.0
 * last uppdate - 10/11/12
 */

namespace Captcha
{
    public class ImageHandler : IHttpHandler, System.Web.SessionState.IRequiresSessionState
    {
        // Process request - Creates the graphics
        //                   and ouputs JPEG format for 
        //                   Image1.url in Captcha.ascx
        public void ProcessRequest(HttpContext context)
        {
            // Set content type for the image
            context.Response.ContentType = "image/jpeg";

            Random random = new Random();

            string randomString = (string)context.Session["RandomString"];

            // create bitmap based on string length
            int mapWidth = (int)(randomString.Length * 25);
            Bitmap bitMap = new Bitmap(mapWidth, 40);

            // create graphics object w/ 
            // background color and frame
            Graphics graphics = Graphics.FromImage(bitMap);
            graphics.Clear(Color.Chartreuse);
            graphics.DrawRectangle(new Pen(Color.CornflowerBlue, 0), 0, 0, bitMap.Width - 1, bitMap.Height - 1);

            // create a random noise pattern
            Pen badPen = new Pen(Color.LightPink, 0);
            for (int i = 0; i < 100; i++)
            {
                int x = random.Next(1, bitMap.Width - 1);
                int y = random.Next(1, bitMap.Height - 1);
                graphics.DrawRectangle(badPen, x, y, 4, 3);
                graphics.DrawEllipse(badPen, x, y, 2, 3);
            }

            // text settings
            char[] charString = randomString.ToCharArray();
            Font font = new Font("Tahoma", 18, FontStyle.Bold);
            Color[] colors = { Color.Black, Color.Red, Color.Purple, Color.Green, Color.DarkBlue, Color.Brown, Color.Coral, Color.WhiteSmoke };

            // draw text onto graphics object
            for (int i = 0; i < randomString.Length; i++)
            {
                int distance = random.Next(20, 25); // distance between text
                int level = random.Next(1, 15); // up and down position
                int color = random.Next(0, 7); // random color from colors[]
                string string1 = Convert.ToString(charString[i]); // char[] to string
                Brush brush = new System.Drawing.SolidBrush(colors[color]);
                graphics.DrawString(string1, font, brush, 1 + i * distance, level);
            }

            // Save the image to a MemoryStream
            MemoryStream memoryStream = new MemoryStream();
     
            // Imagee as JPEG
            bitMap.Save(memoryStream, ImageFormat.Jpeg);
            byte[] buffer = memoryStream.ToArray();

            // Write the image bytes to the response
            context.Response.BinaryWrite(buffer);
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}