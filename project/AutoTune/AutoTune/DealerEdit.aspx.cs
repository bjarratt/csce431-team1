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
	public partial class DealerEdit : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if((Employee)Session["User"] == null)
				Response.Redirect("Default.aspx");
			else
				Label1.Text = ((Employee) Session["User"]).Username;

			if (!IsPostBack)
			{
				Dealership dealership = Dealership.Find(int.Parse(Request["id"]));
				NameTextBox.Text = dealership.Name;
				LocationTextBox.Text = dealership.Location;
				EmailTextBox.Text = dealership.Email;
				PhoneTextBox.Text = dealership.Phone;
			}
		}

		protected void SubmitButton_Click(object sender, EventArgs e)
		{
			Dealership dealership = Dealership.Find(int.Parse(Request["id"]));
			dealership.Name = NameTextBox.Text;
			dealership.Location = LocationTextBox.Text;
			dealership.Email = EmailTextBox.Text;
			dealership.Phone = PhoneTextBox.Text;
			dealership.Commit();
			Response.Redirect("AdminDealer.aspx");
		}

		protected void Logout_Click(object sender, EventArgs e)
		{
			Session["User"] = null;
			Response.Redirect("Default.aspx");
		}
	}
}
