﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AdminEmployee.aspx.cs" Inherits="AutoTune.AdminEmployee" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Administrator Employee List</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    Employee List
        <br />
        <br />
        You are logged in as: (employee name, number)<br />
        <br />
        <asp:LinkButton ID="LinkButton1" runat="server" onclick="LinkButton1_Click">Return to Admin Home</asp:LinkButton>
        <br />
        <br />
        Employee Info Goes Here....
        <br />
        <br />
        <asp:LinkButton ID="LinkButton2" runat="server">Return to Admin Home</asp:LinkButton>
        <br />
        <br />
        <asp:LinkButton ID="LinkButton3" runat="server">Logout</asp:LinkButton>
    </div>
    </form>
</body>
</html>
