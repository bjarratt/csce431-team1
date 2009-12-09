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
    public partial class AddDealership : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Employee user = (Employee)Session["User"];
            if (user != null && user.IsAdmin())
            {
                Label1.Text = user.Username;
            }
            else
            {
                Response.Redirect("AccessDenied.aspx");
            }
        }

        protected void SubmitButton_Click(object sender, EventArgs e)
        {
            string dealershipName = NameBox.Text;
            string location = LocationBox.Text;
            string email = EmailBox.Text;
            string phone = PhoneBox.Text;

            Dealership dealership = new Dealership();
            dealership.Name = dealershipName;
            dealership.Location = location;
            dealership.Email = email;
            dealership.Phone = phone;

            dealership.Commit();

            SuccessLabel.Text = "Dealership added Successfully";
            SuccessLabel.ForeColor = System.Drawing.Color.Green;

            NameBox.Text = String.Empty;
            LocationBox.Text = String.Empty;
            EmailBox.Text = String.Empty;
            PhoneBox.Text = String.Empty;
        }
        protected void MessageClick(object sender, EventArgs e)
        {
            Response.Redirect("Messaging.aspx");
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
    }
}
