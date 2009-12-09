<%@ Page Language="C#" MasterPageFile="~/Layout.Master" AutoEventWireup="true" CodeBehind="AccessDenied.aspx.cs" Inherits="AutoTune.AccessDenied" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContentPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HeadContentPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MessageContentPlaceholder" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
    <asp:Panel ID="Panel1" runat="server" Height="268px" Width="436px">
        <asp:Image ID="Image1" runat="server" ImageUrl="images/AccessDenied.jpg" />
        <br />
        <br />
        <br />
        Please Login to Continue...
    </asp:Panel>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="LoginPlaceHolder1" runat="server">
<%--<asp:Label ID="UserLabel" runat="server" Text=""></asp:Label>--%>
	Username:
	<br />
	<asp:TextBox ID="UsernameBox" runat="server"></asp:TextBox><br /><br />
	Password:
	<br />
	<asp:TextBox ID="PasswordBox" runat="server" TextMode="Password"></asp:TextBox>
	<br />
	<br />
	<asp:Button ID="LoginButton" runat="server" Text="Login" 
        onclick="Login_Click" />
    
</asp:Content>
