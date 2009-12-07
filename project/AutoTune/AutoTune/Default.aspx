<%@ Page Language="C#" MasterPageFile="~/Layout.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="AutoTune.WebForm1" Title="Untitled Page" %>
<asp:Content ID="TitleContent" ContentPlaceHolderID="TitleContentPlaceHolder" runat="server">
    AutoTune
</asp:Content>
<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadContentPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="LoginContent" ContentPlaceHolderID="LoginPlaceHolder1" runat="server">
	
	<%--<asp:Label ID="UserLabel" runat="server" Text=""></asp:Label>--%>
	Username:
	<asp:TextBox ID="Username" runat="server"></asp:TextBox>
	<asp:Button ID="LoginButton" runat="server" Text="Login" onclick="Login_Click" />
	<br />
	Password:
	<asp:TextBox ID="Password" runat="server" TextMode="Password"></asp:TextBox>
	<br />
	
	<asp:Button ID="LogoutButton" runat="server" Text="Logout" onclick="Logout_Click" />
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
<p><img src="images/auto-tune logo.jpg" alt="" width="564" height="200" /></p>
<p>This software will manage operations at BUKI. It will be responsible, at minimum, for keeping inventory of the cars that are present at the dealership, displaying a car's value to both management and sales teams at the dealership (they might see different variations for the same car), adding and removing managers and salesmen from the system, as well as any additional tasks the client may come up with later.</p>
</asp:Content>				