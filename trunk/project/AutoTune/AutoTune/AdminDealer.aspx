<%@ Page Language="C#" MasterPageFile="~/Layout.Master" AutoEventWireup="true" CodeBehind="AdminDealer.aspx.cs" Inherits="AutoTune.AdminDealer" Title="Untitled Page" %>
<%@ Import Namespace="AutoTune.Models"%>
<asp:Content ID="TitleContent" ContentPlaceHolderID="TitleContentPlaceHolder" runat="server">
    Administrator Dealership List
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
</asp:Content>
<asp:Content ID="MainContent" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
	<div>
    	<h2 class="title"><a href="#">Welcome to the AutoTune Dealership Listing</a></h2>
        <h2 class="title"><a href="#">Page</a></h2>
        <br />
        <br />
        <asp:HyperLink ID="HyperLink1" runat="server" 
            NavigateUrl="AdminHome.aspx">Return to Admin Home</asp:HyperLink>
        <br />
        <br />
        <asp:Button ID="Button1" runat="server" Text="Add Dealership" />
        <br />
        <asp:Repeater ID="DealershipsRepeater" runat="server">
					<ItemTemplate>
					<div style="border: 1px solid #000000; margin: 16px; padding: 8px;">
						<h2><%# ((Dealership)Container.DataItem).Name %></h2>
						<b>Location: </b><em><%# ((Dealership)Container.DataItem).Location %></em><br />
						<b>Email: </b><em><%# ((Dealership)Container.DataItem).Email %></em><br />
						<b>Phone: </b><em><%# ((Dealership)Container.DataItem).Phone %></em><br />
						<a href="DealerEdit.aspx?id=<%# ((Dealership)Container.DataItem).ID %>">Edit</a>
					</div>
					</ItemTemplate>
        </asp:Repeater>
        <br />
        <br />
        <asp:HyperLink ID="HyperLink2" runat="server" 
            NavigateUrl="AdminHome.aspx">Return to Admin Home</asp:HyperLink>
    </div>
</asp:Content>