<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SalespersonVehicle.aspx.cs" Inherits="AutoTune.SalespersonEmployee" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Salesperson Vehicle List</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    Vehicle Inventory
        <br />
        <br />
        You are logged in as: (employee name, number)<br />
        <br />
        <asp:LinkButton ID="LinkButton1" runat="server" onclick="LinkButton1_Click">Return to Salesperson Home</asp:LinkButton>
        <br />
        <br />
        Vehicle Info Goes Here....
        <br />
        <br />
        <asp:LinkButton ID="LinkButton2" runat="server">Return to Salesperson Home</asp:LinkButton>
        <br />
        <br />
        <asp:LinkButton ID="LinkButton3" runat="server">Logout</asp:LinkButton>
    </div>
    </form>
</body>
</html>
