<%@ Page Language="C#" MasterPageFile="~/Layout.Master" AutoEventWireup="true" CodeBehind="SalespersonEmployee.aspx.cs" Inherits="AutoTune.SalespersonEmployee1" Title="Untitled Page" %>
<%@ Import Namespace="System.ComponentModel"%>
<%@ Import Namespace="AutoTune.Models"%>
<asp:Content ID="TitleContent" ContentPlaceHolderID="TitleContentPlaceHolder" runat="server">
    Manager Employee List
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
    	<h2 class="title"><a href="#">Welcome to the AutoTune Employee Info</a></h2>
        <h2 class="title"><a href="#">Page</a></h2>
        <br />
        <br />
        <asp:HyperLink ID="HyperLink1" runat="server" 
            NavigateUrl="ManagerHome.aspx">Return to Manager Home</asp:HyperLink>
        <br />
        <br />
        <asp:Repeater ID="EmployeesRepeater" runat="server">
        <ItemTemplate>
        <div style="border: 1px solid #000000; margin: 16px; padding: 8px;">
					<h2><%# ((Employee)Container.DataItem).FullName %></h2>
					<b>Username: </b> <em><%# ((Employee)Container.DataItem).Username %></em><br />
					<b>Location: </b> <em><%# ((Employee)Container.DataItem).Location.Name %></em><br />
					<b>Email: </b> <em><%# ((Employee)Container.DataItem).Email %></em><br />
					<b>Phone: </b> <em><%# ((Employee)Container.DataItem).Phone %></em><br />
					<b>IsManager: </b> <em><%# ((Employee)Container.DataItem).IsManager %></em><br />
					<b>ImageUri: </b> <em><%# ((Employee)Container.DataItem).ImageUri %></em><br />
        </div>
        </ItemTemplate>
        </asp:Repeater>
        <br />
        <asp:HyperLink ID="HyperLink2" runat="server" 
            NavigateUrl="ManagerHome.aspx">Return to Manager Home</asp:HyperLink>
    </div>
</asp:Content>
    