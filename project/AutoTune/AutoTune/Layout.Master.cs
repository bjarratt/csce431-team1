using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using AutoTune.Models;

namespace AutoTune
{
    public partial class Layout : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void HomeClick(object sender, EventArgs e)
        {
            Employee user = (Employee)Session["User"];
            if (user == null)
            {
                Response.Redirect("Default.aspx");
            }
            else if(user.IsAdmin())
            {
                Response.Redirect("AdminHome.aspx");
            }
            else if(user.IsManager)
            {
                Response.Redirect("ManagerHome.aspx");
            }
            else
            {
                Response.Redirect("SalespersonHome.aspx");
            }
        }
    }
}
