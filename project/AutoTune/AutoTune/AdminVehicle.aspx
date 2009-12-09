<%@ Page Language="C#" MasterPageFile="~/Layout.Master" AutoEventWireup="true" CodeBehind="AdminVehicle.aspx.cs" Inherits="AutoTune.AdminVehicle" Title="Untitled Page" %>
<%@ Import Namespace="System.ComponentModel"%>
<%@ Import Namespace="AutoTune.Models"%>

<asp:Content ID="TitleContent" ContentPlaceHolderID="TitleContentPlaceHolder" runat="server">
    Administrator Vehicle List
</asp:Content>
<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadContentPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="MessageButton" ContentPlaceHolderID="MessageContentPlaceholder" runat="server">
<asp:LinkButton ID="LinkButton2" runat="server" OnClick="MessageClick" Height="16px"
Width="70px">Messaging</asp:LinkButton>
</asp:Content>
<asp:Content ID="LoginContent" ContentPlaceHolderID="LoginPlaceHolder1" runat="server">
    You are logged in as: <br /><asp:Label ID="Label1" runat="server" Font-Bold="true" ></asp:Label>
<br />
<br />
    <asp:Button ID="Logout_Button" runat="server" onclick="Logout_Click" 
        Text="Logout" />
<br />
    <asp:Panel ID="Panel2" runat="server" Height="29px">
    </asp:Panel>
<asp:HyperLink ID="HyperLink4" runat="server" 
            NavigateUrl="ChangePassword.aspx">Change Password</asp:HyperLink>
<br />
<br />
</asp:Content>
<asp:Content ID="MainContent" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
	<div>
    	<h2 class="title"><a href="#">Welcome to the AutoTune Vehicle Listing</a></h2>
        <h2 class="title"><a href="#">Page</a></h2> 
        <br />
        <br />
        <asp:HyperLink ID="HyperLink1" runat="server" 
            NavigateUrl="AdminHome.aspx">Return to Admin Home</asp:HyperLink>
        <br />
        <br />
        <asp:Repeater ID="VehiclesRepeater" runat="server">
			<HeaderTemplate>
				<table style="margin: auto; width: 75%;">
			</HeaderTemplate>
			<ItemTemplate>
				<tr>
					<td colspan="2"><h2><%# ((Vehicle)Container.DataItem).ToString() %></h2></td>
				</tr>
				<tr>
					<td><img alt="<%# ((Vehicle)Container.DataItem).ToString() %>" src="images/Vehicles/<%# ((Vehicle)Container.DataItem).ImageUri %>" width="256" /></td>
					<td align="left">
					    <b>VIN: </b> <em><%# ((Vehicle)Container.DataItem).Identifier %></em><br />
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
        <br />
        <br />
        <asp:HyperLink ID="HyperLink2" runat="server" 
            NavigateUrl="AdminHome.aspx">Return to Admin Home</asp:HyperLink>
    </div>
</asp:Content>
