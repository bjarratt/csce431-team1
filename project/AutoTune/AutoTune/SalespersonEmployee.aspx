<%@ Page Language="C#" MasterPageFile="~/Layout.Master" AutoEventWireup="true" CodeBehind="SalespersonEmployee.aspx.cs" Inherits="AutoTune.SalespersonEmployee1" Title="Untitled Page" %>
<asp:Content ID="TitleContent" ContentPlaceHolderID="TitleContentPlaceHolder" runat="server">
    Salesperson Employee List
</asp:Content>
<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadContentPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="LoginContent" ContentPlaceHolderID="LoginPlaceHolder1" runat="server">
    You are logged in as: <br />(employee name, number)
<br />
<br />
<br />
<asp:LinkButton ID="LinkButton1" runat="server" onclick="Logout_Click" PostBackUrl="Default.aspx">Logout</asp:LinkButton>
</asp:Content>
<asp:Content ID="MainContent" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
	<div>
    Employee List
        <br />
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
    </div>
</asp:Content>
    