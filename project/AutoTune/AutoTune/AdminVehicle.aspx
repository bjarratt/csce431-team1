﻿<%@ Page Language="C#" MasterPageFile="~/Layout.Master" AutoEventWireup="true" CodeBehind="AdminVehicle.aspx.cs" Inherits="AutoTune.AdminVehicle" Title="Untitled Page" %>
<asp:Content ID="TitleContent" ContentPlaceHolderID="TitleContentPlaceHolder" runat="server">
    Administrator Vehicle List
</asp:Content>
<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadContentPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="MainContent" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
	<div>
    Vehicle List
        <br />
        <br />
        You are logged in as: (employee name, number)<br />
        <br />
        <asp:HyperLink ID="HyperLink1" runat="server" 
            NavigateUrl="AdminHome.aspx">Return to Admin Home</asp:HyperLink>
        <br />
        <br />
        <asp:Button ID="Button1" runat="server" Text="Add Vehicle" />
        <br />
        Vehicle Info Goes Here....
        <br />
        <br />
        <asp:HyperLink ID="HyperLink2" runat="server" 
            NavigateUrl="AdminHome.aspx">Return to Admin Home</asp:HyperLink>
        <br />
        <br />
        <asp:LinkButton ID="LinkButton3" runat="server">Logout</asp:LinkButton>
    </div>
</asp:Content>
