<%@ Page Language="C#" MasterPageFile="~/Layout.Master" AutoEventWireup="true" CodeBehind="ManagerHome.aspx.cs" Inherits="AutoTune.ManagerHome" Title="Untitled Page" %>
<asp:Content ID="TitleContent" ContentPlaceHolderID="TitleContentPlaceHolder" runat="server">
    Manager Homepage
</asp:Content>
<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadContentPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="LoginContent" ContentPlaceHolderID="LoginPlaceHolder1" runat="server">
    You are logged in as: <br />(employee name, number)
<br />
<br />
<br />
<asp:LinkButton ID="LinkButton3" runat="server">Logout</asp:LinkButton>
</asp:Content>
<asp:Content ID="MainContent" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
	<div>
    Manager Home
        <br />
        <br />
        <asp:HyperLink ID="HyperLink1" runat="server" 
            NavigateUrl="SalespersonVehicle.aspx">View Vehicle Inventory</asp:HyperLink>
        <br />
        <br />
        <asp:HyperLink ID="HyperLink2" runat="server" 
            NavigateUrl="SalespersonEmployee.aspx">View Employee Roster</asp:HyperLink>
        <br />
        <br />
        <asp:LinkButton ID="LinkButton1" runat="server">Messaging</asp:LinkButton>
        <br />
        <br />
        <asp:LinkButton ID="LinkButton2" runat="server">Logout</asp:LinkButton>
    </div>
</asp:Content>