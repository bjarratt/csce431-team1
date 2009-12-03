<%@ Page Language="C#" MasterPageFile="~/Layout.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="AutoTune.WebForm1" Title="Untitled Page" %>
<asp:Content ID="TitleContent" ContentPlaceHolderID="TitleContentPlaceHolder" runat="server">
    AutoTune
</asp:Content>
<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadContentPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="MainContent" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
	<asp:Label ID="UserLabel" runat="server" Text=""></asp:Label>
	<br />
	Username:
	<asp:TextBox ID="Username" runat="server"></asp:TextBox>
	<br />
	Password:
	<asp:TextBox ID="Password" runat="server" TextMode="Password"></asp:TextBox>
	<br />
	<asp:Button ID="LoginButton" runat="server" Text="Login" onclick="Login_Click" />
	<asp:Button ID="LogoutButton" runat="server" Text="Logout" onclick="Logout_Click" />
</asp:Content>
