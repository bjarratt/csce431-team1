<%@ Page Language="C#" MasterPageFile="~/Layout.Master" AutoEventWireup="true" CodeBehind="SalespersonVehicle.aspx.cs" Inherits="AutoTune.SalespersonEmployee" Title="Untitled Page" %>
<asp:Content ID="TitleContent" ContentPlaceHolderID="TitleContentPlaceHolder" runat="server">
    Salesperson Vehicle List
</asp:Content>
<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadContentPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="MessageButton" ContentPlaceHolderID="MessageContentPlaceholder" runat="server">
<asp:LinkButton ID="LinkButton2" runat="server" OnClick="HomeClick" Height="16px"
Width="70px">Messaging</asp:LinkButton>
</asp:Content>
<asp:Content ID="LoginContent" ContentPlaceHolderID="LoginPlaceHolder1" runat="server">
    You are logged in as: <br /><asp:Label ID="Label1" runat="server"></asp:Label>
<br />
<br />
<br />
<asp:LinkButton ID="LinkButton1" runat="server" onclick="Logout_Click" PostBackUrl="Default.aspx">Logout</asp:LinkButton>
</asp:Content>
<asp:Content ID="MainContent" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
	<div>
    	<h2 class="title"><a href="#">Welcome to the AutoTune Vehicle Listing</a></h2>
        <h2 class="title"><a href="#">Page</a></h2>
        <br />
        <br />
        <asp:HyperLink ID="HyperLink1" runat="server" 
            NavigateUrl="SalespersonHome.aspx">Return to Salesperson Home</asp:HyperLink>
        <br />
        <br />
        <asp:Button ID="Button1" runat="server" Text="Add Vehicle" />
        <br />
        Vehicle Info Goes Here....
        <br />
        <br />
        <asp:HyperLink ID="HyperLink2" runat="server" 
            NavigateUrl="SalespersonHome.aspx">Return to Salesperson Home</asp:HyperLink>
    </div>
</asp:Content>