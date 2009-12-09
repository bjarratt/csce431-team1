<%@ Page Language="C#" MasterPageFile="~/Layout.Master" AutoEventWireup="true" CodeBehind="SellVehicle.aspx.cs" Inherits="AutoTune.SellVehicle" Title="Untitled Page" %>
<%@ Import Namespace="System.ComponentModel"%>
<%@ Import Namespace="AutoTune.Models"%>
<asp:Content ID="TitleContent" ContentPlaceHolderID="TitleContentPlaceHolder" runat="server">
    Manager Vehicle List
</asp:Content>
<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadContentPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="MessageButton" ContentPlaceHolderID="MessageContentPlaceholder" runat="server">
<asp:LinkButton ID="LinkButton2" runat="server" OnClick="MessageClick" Height="16px"
Width="70px">Messaging</asp:LinkButton>
</asp:Content>
<asp:Content ID="LoginContent" ContentPlaceHolderID="LoginPlaceHolder1" runat="server">
    You are logged in as: <br /><asp:Label ID="Label1" runat="server" Font-Bold="true" ></asp:Label>
<br />
<br />
    <asp:Button ID="Logout_Button" runat="server" onclick="Logout_Click" 
        Text="Logout" />
<br />
    <asp:Panel ID="Panel2" runat="server" Height="29px">
    </asp:Panel>
<asp:HyperLink ID="HyperLink4" runat="server" 
            NavigateUrl="ChangePassword.aspx">Change Password</asp:HyperLink>
<br />
<br />
</asp:Content>
<asp:Content ID="MainContent" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
	<div>
		<h1>Selling <asp:Label ID="VehicleNameLabel" runat="server"></asp:Label></h1>
        <asp:HyperLink ID="HyperLink1" runat="server" 
            NavigateUrl="ManagerHome.aspx">Return to Manager Home</asp:HyperLink>
            <br /><br /><br />
            Salesperson: <asp:DropDownList ID="SalespersonList" runat="server"></asp:DropDownList><br />
            Price: <asp:TextBox ID="PriceTextBox" runat="server"></asp:TextBox><br />
            Customer Info:<br />
            <asp:TextBox ID="CustomerTextBox" runat="server" Height="150px" 
			TextMode="MultiLine" Width="500px"></asp:TextBox><br />
            <asp:Button ID="SellButton" Text="Sell" runat="server" 
			onclick="SellButton_Click" />
    </div>
</asp:Content>
