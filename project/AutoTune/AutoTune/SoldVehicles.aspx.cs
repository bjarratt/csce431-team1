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
    public partial class SoldVehicles : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            Employee user = (Employee)Session["User"];
            if (user != null)
            {
                Label1.Text = user.Username;
            	VehiclesRepeater.DataSource = user.Location.GetSoldVehicles();
                VehiclesRepeater.DataBind();
            }
            else
            {
                Response.Redirect("Default.aspx");
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