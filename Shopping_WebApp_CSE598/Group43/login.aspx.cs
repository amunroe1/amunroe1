using System;
using System.Web;
using System.Xml;
using MyEncryption;

namespace Group43
{
    public partial class login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Login_Click(object sender, EventArgs e)
        {
            
            string filePath = HttpRuntime.AppDomainAppPath + @"\Account\App_Data\Members.xml";
            string email = EmailInput.Text;
            string pwd = PwdInput.Text;
           
            //Load Members.xml and encrypt password
            XmlDocument myDoc = new XmlDocument();
            myDoc.Load(filePath);
            pwd = Cipher.Encrypt(pwd);

            
            // Get the current members
            XmlNodeList memberList = myDoc.SelectNodes("/Members/member");
            string clearance = "";
            Boolean success = false;

            //Check for a user matching the input information
            foreach (XmlNode member in memberList)
            {
                if (member.SelectSingleNode("email").InnerText == email)
                {
                    if (member.SelectSingleNode("pwd").InnerText == pwd)
                    {
                        //Check for clearance level
                        clearance = member.Attributes["level"].Value;
                        Session["userName"] = email;
                        Session["clearance"] = clearance;
                        success = true;
                        break;

                    }
                    else
                    {
                        msg.Text = "Incorrect password.";
                    }
                }
                else
                {
                    msg.Text = "An account with that email was not found.";
                }
            }



            //Redirect to ShoppingOrders page on successful login
            if (success)
            {
                Response.Redirect("ShoppingOrdersPage.aspx");
            }
            else
            {
                return;
            }

            //testing
            /*if (success && clearance == "admin")
            {
                msg.Text = $"Admin login successful. Logged in as {Session["userName"]}";
            }
            if (success && clearance == "customer")
            {
                msg.Text = "Customer login successful.";
            }*/
        }

        protected void Cancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("Default.aspx");
        }
    }
}