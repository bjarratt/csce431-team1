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
            if (user != null)
            {
                Label1.Text = user.Username;
            }
            
            foreach (Vehicle v in Vehicle.FindAll()){
                //Table table1 = new Table();
                table1.RowGroups.Add(new TableRowGroup());
                TableRow currentRow;
                currentRow = table1.RowGroups[0].Rows[0];
                table1.RowGroups.Rows.Add(currentRow);
                
                // Add the header row with content, 
                currentRow.Cells.Add(new TableCell(new Paragraph(new Run(v.TableName()))));
                
                currentRow = table1.RowGroups(0).Rows(1);
                table1.RowGroups(0).Rows.Add(currentRow);
                currentRow.Cells.Add(new TableCell(new Paragraph(new Run("Year"))));
                currentRow.Cells.Add(new TableCell(new Paragraph(new Run("Make"))));
                currentRow.Cells.Add(new TableCell(new Paragraph(new Run("Model"))));
                currentRow.Cells.Add(new TableCell(new Paragraph(new Run("VIN"))));
                currentRow.Cells.Add(new TableCell(new Paragraph(new Run("TRIM"))));
                currentRow.Cells.Add(new TableCell(new Paragraph(new Run("SR VALUE"))));
                currentRow.Cells.Add(new TableCell(new Paragraph(new Run("BASE VALUE"))));
                currentRow.Cells.Add(new TableCell(new Paragraph(new Run("DISC VALUE"))));
                currentRow.Cells.Add(new TableCell(new Paragraph(new Run("LOCATION"))));
                //currentRow.Cells.Add(new TableCell(new Paragraph(new Run("STATUS"))));
                //currentRow.Cells.Add(new TableCell(new Paragraph(new Run("TRADE STAT"))));
                currentRow = table1.RowGroups(0).Rows(2);
                table1.RowGroups(0).Rows.Add(currentRow);
                

                currentRow.Cells.Add(new TableCell(new Paragraph(new Run(v.Year))));
                currentRow.Cells.Add(new TableCell(new Paragraph(new Run(v.Make))));
                currentRow.Cells.Add(new TableCell(new Paragraph(new Run(v.Model))));
                currentRow.Cells.Add(new TableCell(new Paragraph(new Run(v.Identifier))));
                currentRow.Cells.Add(new TableCell(new Paragraph(new Run(v.Trim))));
                currentRow.Cells.Add(new TableCell(new Paragraph(new Run(v.BookValue))));
                currentRow.Cells.Add(new TableCell(new Paragraph(new Run(v.BaseValue))));
                currentRow.Cells.Add(new TableCell(new Paragraph(new Run(v.DiscountValue))));
                currentRow.Cells.Add(new TableCell(new Paragraph(new Run(v.GetLocation()))));
                //currentRow.Cells.Add(new TableCell(new Paragraph(new Run(v.))));
                //currentRow.Cells.Add(new TableCell(new Paragraph(new Run("TRADE STAT"))));


            }
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
