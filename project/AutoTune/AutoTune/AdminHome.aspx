<%@ Page Language="C#" MasterPageFile="~/Layout.Master" AutoEventWireup="true" CodeBehind="AdminHome.aspx.cs" Inherits="AutoTune.AdminHome" %>
<asp:Content ID="TitleContent" ContentPlaceHolderID="TitleContentPlaceHolder" runat="server">
    AutoTune
</asp:Content>
<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadContentPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="LoginContent" ContentPlaceHolderID="LoginPlaceHolder1" runat="server">
    You are logged in as: <br />(employee name, number)
<br />
<br />
<br />
<asp:LinkButton ID="LinkButton1" runat="server" onclick="Logout_Click" PostBackUrl="Default.aspx">Logout</asp:LinkButton>
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
	""
</asp:Content>
<%--
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Administrator Homepage</title>
    <link href="default.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    </form>
</body>
</html>
--%>