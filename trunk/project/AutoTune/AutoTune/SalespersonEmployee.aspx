<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SalespersonEmployee.aspx.cs" Inherits="AutoTune.SalespersonEmployee1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Salesperson Employee List</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    Employee List
        <br />
        <br />
        You are logged in as: (employee name, number)<br />
        <br />
        <asp:HyperLink ID="HyperLink1" runat="server" 
            NavigateUrl="SalespersonHome.aspx">Return to Salesperson Home</asp:HyperLink>
        <br />
        <br />
        <asp:Button ID="Button1" runat="server" Text="Add Employee" />
        <br />
        Employee Info Goes Here....
        <br />
        <br />
        <asp:HyperLink ID="HyperLink2" runat="server" 
            NavigateUrl="SalespersonHome.aspx">Return to Salesperson Home</asp:HyperLink>
        <br />
        <br />
        <asp:LinkButton ID="LinkButton3" runat="server">Logout</asp:LinkButton>
    </div>
    </form>
</body>
</html>
