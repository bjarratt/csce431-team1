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
    public partial class AccessDenied : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void Login_Click(object sender, EventArgs e)
        {

            string username = UsernameBox.Text;
            string password = PasswordBox.Text;

            Employee user = Employee.Authenticate(username, password);
            if (user != null)
            {
                Session["User"] = user;
                if (user.IsAdmin())
                    Response.Redirect("AdminHome.aspx");
                if (user.IsManager)
                    Response.Redirect("ManagerHome.aspx");
                else //if (user.IsSalesperson())
                    Response.Redirect("SalespersonHome.aspx");
            }
        }
    }
}
