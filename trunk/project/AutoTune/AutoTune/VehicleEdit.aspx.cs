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
    public partial class VehicleEdit : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if ((Employee)Session["User"] == null)
                Response.Redirect("Default.aspx");
            else
                Label1.Text = ((Employee)Session["User"]).Username;

            if (!IsPostBack)
            {
                Vehicle Vehicle = Vehicle.Find(int.Parse(Request["id"]));
                YearTextBox.Text = Vehicle.Year.ToString();
                MakeTextBox.Text = Vehicle.Make;
                ModelTextBox.Text = Vehicle.Model;
                VINTextBox.Text = Vehicle.Identifier;
                TrimTextBox.Text = Vehicle.Trim;
                BookValTextBox.Text = Vehicle.BookValue.ToString();
                BaseValTextBox.Text = Vehicle.BaseValue.ToString();
                DisValTextBox.Text = Vehicle.DiscountValue.ToString();
            }

        }
        protected void SubmitButton_Click(object sender, EventArgs e)
        {
            Vehicle Vehicle = Vehicle.Find(int.Parse(Request["id"]));
            Vehicle.Year = int.Parse(YearTextBox.Text);
            Vehicle.Make = MakeTextBox.Text;
            Vehicle.Model = ModelTextBox.Text;
            Vehicle.Identifier = VINTextBox.Text;
            Vehicle.Trim = TrimTextBox.Text;
            Vehicle.BookValue = int.Parse(BookValTextBox.Text);
            Vehicle.BaseValue = int.Parse(BaseValTextBox.Text);
            Vehicle.DiscountValue = int.Parse(DisValTextBox.Text);
            Vehicle.Commit();
            Response.Redirect("ManagerVehicle.aspx");
        }

        protected void Logout_Click(object sender, EventArgs e)
        {
            Session["User"] = null;
            Response.Redirect("Default.aspx");
        }
        protected void MessageClick(object sender, EventArgs e)
        {
            Response.Redirect("Messaging.aspx");
        }
    }
}
