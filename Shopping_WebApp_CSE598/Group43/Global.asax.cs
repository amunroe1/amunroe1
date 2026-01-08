using System;
using System.Web;

namespace Group43
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {

        }

        protected void Session_Start(object sender, EventArgs e)
        {
            //Initialize the cart cookie
            HttpCookie cartCookie = new HttpCookie("ShoppingCart");

            //Initialize empty error log file
            string errorLogPath = Server.MapPath("~/errorlog.txt");

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {
            // Get the last unhandled exception that occurred
            Exception ex = Server.GetLastError();
            //Write the exception ex to a errorlog.txt file
            System.IO.File.AppendAllText(Server.MapPath("~/errorlog.txt"), DateTime.Now.ToString() + ": " + ex.ToString() + Environment.NewLine);

            Response.Redirect("ErrorPage.aspx");
        }

        protected void Session_End(object sender, EventArgs e)
        {
            // Get rid of log file on session end
            string errorLogPath = Server.MapPath("~/errorlog.txt");
            if (System.IO.File.Exists(errorLogPath))
            {
                System.IO.File.Delete(errorLogPath);
            }

        }

        protected void Application_End(object sender, EventArgs e)
        {
            
        }
    }
}