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
	public partial class WebForm1 : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
		}

		protected void Login_Click(object sender, EventArgs e)
		{
			string username = Username.Text;
			string password = Password.Text;
			Employee user = Employee.Authenticate(username, password);
			if (user == null)
			{
				Logout();
				UserLabel.Text = "Invalid username or password.";
			}
			else
			{
				UserLabel.Text = string.Format("You are logged in as '{0}'.", user.Username);
				Session["User"] = user;
				bool test = user.AllFieldsAreValid();
			}
		}

		private void Logout()
		{
			Session["User"] = null;
			UserLabel.Text = "You have logged out.";
		}

		protected void Logout_Click(object sender, EventArgs e)
		{
			Logout();
		}

		protected void NewUserButton_Click(object sender, EventArgs e)
		{
			string username = Username.Text;
			string password = Password.Text;

			Employee employee = new Employee();
			employee.Username = username;
			employee.SetPassword(password);
			employee.Commit();
		}
	}
}
