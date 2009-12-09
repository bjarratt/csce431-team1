using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;
using AutoTune.Models;

namespace AutoTune
{
    public partial class ChangePassword : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Employee user = (Employee)Session["User"];
            if (user != null)
                Label1.Text = user.Username;
            else
                Response.Redirect("AccessDenied.aspx");

        }

        protected void SubmitButton_Click(object sender, EventArgs e)
        {
            string username = usernameBox.Text;
            string currentPW = CurrentPWBox.Text;
            string newPW = NewPWBox.Text;

            Employee user = Employee.Authenticate(username, currentPW);
            if (user == null)
            {
                AuthenticLabel.Text = "Authentication Failed. Please try again";
                CurrentPWBox.Text = String.Empty;
                NewPWBox.Text = String.Empty;
            }
            else
            {
                user.Password = newPW;
                user.Commit();
            }
        }
        private void Logout()
        {
            Session["User"] = null;
            Response.Redirect("Default.aspx");
        }

        protected void Logout_Click(object sender, EventArgs e)
        {
            Logout();
        }
        protected void MessageClick(object sender, EventArgs e)
        {
            Response.Redirect("Messaging.aspx");
        }
    }
}
