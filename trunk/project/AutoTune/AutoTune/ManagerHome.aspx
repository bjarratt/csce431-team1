<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ManagerHome.aspx.cs" Inherits="AutoTune.ManagerHome" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Manager Homepage</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    Manager Home
        <br />
        <br />
        You are logged in as: (employee name, number)<br />
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
    </form>
</body>
</html>
