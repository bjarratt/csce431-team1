<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="VehiclesTest.aspx.cs" Inherits="AutoTune.VehiclesTest" %>
<%@ Import Namespace="System.ComponentModel"%>
<%@ Import Namespace="AutoTune.Models"%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
			<asp:Repeater ID="VehiclesRepeater" runat="server">
			<HeaderTemplate>
				<h1>Vehicles</h1>
				<table style="margin: auto; width: 50%;">
			</HeaderTemplate>
			<ItemTemplate>
				<tr>
					<td colspan="2"><h2><%# ((Vehicle)Container.DataItem).ToString() %></h2></td>
				</tr>
				<tr>
					<td><img alt="<%# ((Vehicle)Container.DataItem).ToString() %>" src="images/Vehicles/<%# ((Vehicle)Container.DataItem).ImageUri %>" width="256" /></td>
					<td align="left">
						<b>Trim: </b> <em><%# ((Vehicle)Container.DataItem).Trim %></em><br />
						<b>Book Value: </b> <em><%# ((Vehicle)Container.DataItem).BookValue %></em><br />
						<b>Base Value: </b> <em><%# ((Vehicle)Container.DataItem).BaseValue %></em><br />
						<b>Discount Value: </b> <em><%# ((Vehicle)Container.DataItem).DiscountValue %></em><br />
					</td>
				</tr>
			</ItemTemplate>
			<FooterTemplate>
				</table>
			</FooterTemplate>
			</asp:Repeater>
    </div>
    </form>
</body>
</html>
