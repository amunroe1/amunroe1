using System;
using System.Web;
using System.Xml;
using MyEncryption;

namespace Group43
{
    public partial class Registration : System.Web.UI.Page
    {
        // Page_Load - get the CAPTCHA user control
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Save random string for verification
                Session["RandomString"] = getRandomString();

                // User control visible after
                // after graphics update
                userControlCaptcha1.Visible = true;
            }
        }

        // GetRandomString - generate a random string
        //                   from a string of random
        //                   characters
        public string getRandomString()
        {
            Random random = new Random();

            const string keyboard = "qwertyuiopasdfghjklzxcvbnmQWERTYUIOPASDFGHJKLZXCVBNM0123456789!@#$%^&*?";
            string randomString = "";

            for (int i = 0; i < 5; i++)
                randomString += keyboard[random.Next(keyboard.Length)];

            return randomString;
        }

        // RefreshBtn_Click - regenerate the CAPTCHA
        protected void refreshBtn_Click(object sender, EventArgs e)
        {
            Session["RandomString"] = getRandomString();
        }

        // registerBtn_Click - Verify user registration
        protected void registerBtn_Click(object sender, EventArgs e)
        {
            if ( passwordTxtBox.Text.Length == 0)
            {
                // Error
                captchaErrorLbl.Text = "Password is required.";
                captchaErrorLbl.Visible = true;
                return;
            }

            string verificationString = (string)Session["RandomString"];

            // Verify CAPTCHA and register
            if (verificationTxtBox.Text.Length > 0 && verificationString == verificationTxtBox.Text)
            {
                captchaErrorLbl.Visible = false;
                Register();
            }
            else
            {
                // Error
                captchaErrorLbl.Text = "You did not enter the correct text.";
                captchaErrorLbl.Visible = true;
            }
        }

        // cancelBtn_Click - Cancel registration
        protected void cancelBtn_Click(object sender, EventArgs e)
        {
            Response.Redirect("Default.aspx");
        }

        // Register - Create new user or check if exists
        private void Register()
        {
            string filePath = HttpRuntime.AppDomainAppPath + @"\Account\App_Data\Members.xml";
            string user = userNameTxtBox.Text;
            string password = passwordTxtBox.Text;

            // Hashing
            password = Cipher.Encrypt(password);
            XmlDocument myDoc = new XmlDocument();
            myDoc.Load(filePath);

            // Open file
            XmlElement rootElement = myDoc.DocumentElement;
            
            // Get the current members
            XmlNodeList memberList = myDoc.SelectNodes("/Members/member[@level='customer']");

            // Check if member exists
            foreach (XmlNode memberNode in memberList)
            {

                if (memberNode["email"].InnerText == user)
                {
                    captchaErrorLbl.Text = String.Format("Account with username {0} already exists.", user);
                    captchaErrorLbl.Visible = true;
                    return;
                }
            }
             
            captchaErrorLbl.Visible = false;
            
            // Create new member and add 'level' attribute
            XmlElement myMember = myDoc.CreateElement("member", rootElement.NamespaceURI);
            myMember.SetAttribute("level", "customer");
            rootElement.AppendChild(myMember);

            XmlElement myUser = myDoc.CreateElement("email", rootElement.NamespaceURI);
            myMember.AppendChild(myUser);
            myUser.InnerText = user;

            XmlElement myPwd = myDoc.CreateElement("pwd", rootElement.NamespaceURI);
            myMember.AppendChild(myPwd);
            myPwd.InnerText = password;

            myDoc.Save(filePath);

            Response.Redirect("login.aspx");
        }
    }
}