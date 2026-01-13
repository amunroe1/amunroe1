using System;

namespace Group43
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Enable Admin button only for admin users
                var clearance = Session["clearance"] as string;
                Button4.Enabled = (clearance == "admin");
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Response.Redirect("Shopping.aspx");
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            Response.Redirect("Registration.aspx");
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            Response.Redirect("login.aspx");
        }

        protected void Button4_Click(object sender, EventArgs e)
        {
            // Only allow if this session is admin
            var clearance = Session["clearance"] as string;

            if (clearance == "admin")
            {
                // Go to ShoppingOrdersPage; that page already knows how
                // to show ALL orders for admins.
                Response.Redirect("ShoppingOrdersPage.aspx");
            }
            else
            {
                // Safety fallback – non-admins get bounced to login
                Response.Redirect("login.aspx");
            }
        }
    }
}