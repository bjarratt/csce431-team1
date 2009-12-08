﻿<%@ Page Language="C#" MasterPageFile="~/Layout.Master" AutoEventWireup="true" CodeBehind="ManagerHome.aspx.cs" Inherits="AutoTune.ManagerHome" Title="Untitled Page" %>
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
<asp:LinkButton ID="LinkButton3" runat="server" onclick="Logout_Click" PostBackUrl="Default.aspx">Logout</asp:LinkButton>
</asp:Content>
<asp:Content ID="MainContent" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
	<div>
    	<h2 class="title"><a href="#">Welcome to the AutoTune Manager Home Page</a></h2>
        <br />
        <br />
        <asp:HyperLink ID="HyperLink1" runat="server" 
            NavigateUrl="SalespersonVehicle.aspx">View Vehicle Inventory</asp:HyperLink>
        <br />
        <br />
        <asp:HyperLink ID="HyperLink2" runat="server" 
            NavigateUrl="SalespersonEmployee.aspx">View Employee Roster</asp:HyperLink>
    </div>
</asp:Content>