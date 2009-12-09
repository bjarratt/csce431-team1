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
    public partial class WebForm4 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Employee user = (Employee)Session["User"];
            if (!IsPostBack)
            {
                UpdateDataBindings();
            }
            if (user == null)
                Response.Redirect("Default.aspx");
            else
                Label1.Text = user.Username;
        }

        private void UpdateDataBindings()
        {
            DealershipsRepeater.DataSource = Dealership.FindAll();
            DealershipsRepeater.DataBind();
            DealershipBox.DataSource = Dealership.FindAll();
            DealershipBox.DataTextField = "Name";
            DealershipBox.DataValueField = "ID";
            DealershipBox.DataBind();
        }

        protected void AddVehicle(object sender, EventArgs e)
        {
            Vehicle v = new Vehicle();
            v["DealershipID"] = DealershipBox.SelectedItem.Value;
            v.Year = int.Parse(Year.Text);
            v.Make = Make.Text;
            v.Model = Model.Text;
            v.Identifier = VIN.Text;
            v.BookValue = int.Parse(BookVal.Text);
            v.BaseValue = int.Parse(BaseVal.Text);
            v.DiscountValue = int.Parse(DisVal.Text);
            v.Commit();
            UpdateDataBindings();
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
