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
    public partial class SellVehicle : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        	Session["Vehicle"] = Vehicle.Find(int.Parse(Request["id"]));
            Employee user = (Employee)Session["User"];
            if (user != null)
            {
                Label1.Text = user.Username;
            	if(!IsPostBack)
								UpdateDataBindings();
            }
            else
            {
                Response.Redirect("Default.aspx");
            }
        }

			private void UpdateDataBindings()
			{
				Employee user = (Employee) Session["User"];
				SalespersonList.DataSource = user.Location.GetEmployees();
				SalespersonList.DataTextField = "Username";
				SalespersonList.DataValueField = "ID";
				SalespersonList.DataBind();
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

				protected void SellButton_Click(object sender, EventArgs e)
				{
					Vehicle vehicle = (Vehicle)Session["Vehicle"];
					Employee user = (Employee) Session["User"];
					Employee salesperson = Employee.Find(int.Parse(SalespersonList.SelectedItem.Value));
					double price = double.Parse(PriceTextBox.Text);

					user.SellVehicle(vehicle, salesperson, "", price);

					Response.Redirect("ManagerVehicle.aspx");
				}
    }
}