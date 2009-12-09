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
    public partial class WebForm3 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Employee user = (Employee)Session["User"];
            if (user != null && user.IsAdmin())
                Label1.Text = user.Username;
            else
                Response.Redirect("AccessDenied.aspx");

        }

        protected void SubmitButton_Click(object sender, EventArgs e)
        {
            string username = usernameBox.Text;
            string newPW = NewPWBox.Text;

            Employee user = Employee.FindByUsername(username);

            if (user == null)
            {
                AssignedLabel.Text = "User does not exist. Please try again";
                AssignedLabel.ForeColor = System.Drawing.Color.Red;
                NewPWBox.Text = String.Empty;
            }
            else
            {
                AssignedLabel.Text = "Assignment Successful";
                AssignedLabel.ForeColor = System.Drawing.Color.Green;
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
