﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AdminVehicle.aspx.cs" Inherits="AutoTune.AdminVehicle" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Administrator Vehicle List</title>
</head>
<body>
    <form id="form1" runat="server">
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
    </form>
</body>
</html>
