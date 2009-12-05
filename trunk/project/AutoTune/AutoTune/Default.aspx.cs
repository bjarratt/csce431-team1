using System;
using System.Web.UI;
using AutoTune.Models;

namespace AutoTune
{
	public partial class WebForm1 : Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			Dealership[] dealerships = Dealership.Find(null);
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
	}
}
