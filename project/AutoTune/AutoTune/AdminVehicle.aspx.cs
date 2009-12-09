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
    public partial class AdminVehicle : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Employee user = (Employee)Session["User"];
            if (user != null && user.IsAdmin())
            {
                Label1.Text = user.Username;
            }
            else
            {
                Response.Redirect("AccessDenied.aspx");
            }
            
            /*foreach (Vehicle v in Vehicle.FindAll()){
                //Table table1 = new Table();
                table1.Rows.Add(new TableRow());
                TableRow currentRow;
                currentRow = table1.Rows[0];
                
                // Add the header row with content, 
                currentRow.Cells.Add(new TableCell());
                currentRow.Cells[0].Text = v.TableName();
                
                table1.Rows.Add(new TableRow());
                currentRow = table1.Rows[1];
                for (int i = 0; i < 9; i++)
                {
                    currentRow.Cells.Add(new TableCell());
                }
                currentRow.Cells[0].Text = "Year";
                currentRow.Cells[1].Text ="Make";
                currentRow.Cells[2].Text ="Model";
                currentRow.Cells[3].Text ="VIN";
                currentRow.Cells[4].Text ="TRIM";
                currentRow.Cells[5].Text ="SR VALUE";
                currentRow.Cells[6].Text ="BASE VALUE";
                currentRow.Cells[7].Text ="DISC VALUE";
                currentRow.Cells[8].Text ="LOCATION";
                //currentRow.Cells.Add(new TableCell(new Paragraph(new Run("STATUS"))));
                //currentRow.Cells.Add(new TableCell(new Paragraph(new Run("TRADE STAT"))));
                
                table1.Rows.Add(new TableRow());
                currentRow = table1.Rows[2];
                for (int i = 0; i < 9; i++)
                {
                    currentRow.Cells.Add(new TableCell());
                }
                currentRow.Cells[0].Text = v.Year.ToString();
                currentRow.Cells[1].Text = v.Make;
                currentRow.Cells[2].Text = v.Model;
                currentRow.Cells[3].Text = v.Identifier;
                currentRow.Cells[4].Text = v.Trim;
                //currentRow.Cells[5].Text = v.BookValue +"";
                //currentRow.Cells[6].Text = v.BaseValue +"";
                //currentRow.Cells[7].Text = v.DiscountValue +"";
                currentRow.Cells[8].Text = v.GetLocation().ToString();
               
                //currentRow.Cells.Add(new TableCell(new Paragraph(new Run(v.))));
                //currentRow.Cells.Add(new TableCell(new Paragraph(new Run("TRADE STAT"))));


            }*/
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
