using System;
using System.Drawing;

/*     
 *      CSE 598 - Assigment 5 & 6
 *                CAPTCHA User control
 *       Author - Mark Adan
 *      version - 1.0
 * last uppdate - 10/11/12
 */

namespace Captcha
{
    public partial class CaptchaControl : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Set Visibllity, ImageHandler.ashx.cs will
            // create the graphics  and write
            // to memory stream for the image url
            Image1.Visible = true;
        }
    }
}