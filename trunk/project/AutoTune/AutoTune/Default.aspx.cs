using System;
using System.Web.UI;
using AutoTune.Models;

namespace AutoTune
{
	public partial class WebForm1 : Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
		}

		protected void Login_Click(object sender, EventArgs e)
		{
			string username = Username.Text;
			string password = Password.Text;

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
