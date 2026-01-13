using System;

namespace Group43
{
    public partial class ErrorPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Load text from errorlog.txt and display in TextBox
            if (!IsPostBack)
            {
                try
                {
                    string errorLogPath = Server.MapPath("~/errorlog.txt");
                    if (System.IO.File.Exists(errorLogPath))
                    {
                        string logContent = System.IO.File.ReadAllText(errorLogPath);
                        Label1.Text = logContent;
                        //clear the log after displaying
                        System.IO.File.WriteAllText(errorLogPath, string.Empty);
                    }
                    else
                    {
                        Label1.Text = "No error log found.";
                    }
                }
                catch (Exception ex)
                {
                    Label1.Text = "Error loading log: " + ex.Message;
                }
            }
        }
    }
}