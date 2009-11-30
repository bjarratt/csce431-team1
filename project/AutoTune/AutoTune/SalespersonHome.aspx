<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SalespersonHome.aspx.cs" Inherits="AutoTune.SalespersonHome" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Salesperson Homepage</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    Salesperson Home
        <br />
        <br />
        You are logged in as: (employee name, number)<br />
        <br />
        <asp:LinkButton ID="LinkButton1" runat="server" onclick="LinkButton1_Click">View Vehicle Inventory</asp:LinkButton>
        <br />
        <br />
        <asp:LinkButton ID="LinkButton2" runat="server">View Employee Roster</asp:LinkButton>
        <br />
        <br />
        <asp:LinkButton ID="LinkButton3" runat="server">Messaging</asp:LinkButton>
        <br />
        <br />
        <asp:LinkButton ID="LinkButton4" runat="server">Logout</asp:LinkButton>
    </div>
    </form>
</body>
</html>
