<%@ Page Language="C#" MasterPageFile="~/Layout.Master" AutoEventWireup="true" CodeBehind="ManagerHome.aspx.cs" Inherits="AutoTune.ManagerHome" Title="Untitled Page" %>
<asp:Content ID="TitleContent" ContentPlaceHolderID="TitleContentPlaceHolder" runat="server">
    Manager Homepage
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
<br />
<asp:HyperLink ID="changepassword" runat="server" 
            NavigateUrl="ChangePassword.aspx">Change Password</asp:HyperLink>
<br />
<br />
</asp:Content>
<asp:Content ID="MainContent" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
	<div>
    	<h2 class="title"><a href="#">Welcome to the AutoTune Manager Home Page</a></h2>
        <br />
        <br />
        <asp:HyperLink ID="HyperLink4" runat="server" 
            NavigateUrl="ManagerDealer.aspx">View Dealerships</asp:HyperLink>
        <br />
        <br />
        <asp:HyperLink ID="HyperLink1" runat="server" 
            NavigateUrl="ManagerVehicle.aspx">View Vehicle Inventory</asp:HyperLink>
        <br />
        <br />
        <asp:HyperLink ID="HyperLink2" runat="server" 
            NavigateUrl="SalespersonEmployee.aspx">View Employee Roster</asp:HyperLink>
        <br />
        <br />
        <asp:HyperLink ID="HyperLink3" runat="server"
            NavigateUrl= "Messaging.aspx" >Messaging</asp:HyperLink>
    </div>
</asp:Content>