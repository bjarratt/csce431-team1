<%@ Page Language="C#" MasterPageFile="~/Layout.Master" AutoEventWireup="true" CodeBehind="ManagerDealer.aspx.cs" Inherits="AutoTune.WebForm4" Title="Untitled Page" %>
<%@ Import Namespace="System.ComponentModel"%>
<%@ Import Namespace="AutoTune.Models"%>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContentPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HeadContentPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MessageContentPlaceholder" runat="server">
<asp:LinkButton ID="LinkButton2" runat="server" OnClick="MessageClick" Height="16px"
Width="70px">Messaging</asp:LinkButton>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
<h2 class="title"><a href="#">Welcome to the AutoTune Manager Dealership Page</a></h2>
        <br />
<asp:Repeater ID="DealershipsRepeater" runat="server">
					<ItemTemplate>
					<div style="border: 1px solid #000000; margin: 16px; padding: 8px;">
						<h2><%# ((Dealership)Container.DataItem).Name %></h2>
						<b>Location: </b><em><%# ((Dealership)Container.DataItem).Location %></em><br />
						<b>Location: </b><em><%# ((Dealership)Container.DataItem).Email %></em><br />
						<b>Location: </b><em><%# ((Dealership)Container.DataItem).Phone %></em><br />
						<a href="DealerEdit.aspx?id=<%# ((Dealership)Container.DataItem).ID %>">Edit</a>
					</div>
					</ItemTemplate>
        </asp:Repeater>
        <br />
        <br />
Year:<asp:TextBox ID="Year" runat="server"></asp:TextBox><br />
Make:<asp:TextBox ID="Make" runat="server"></asp:TextBox><br />
Model:<asp:TextBox ID="Model" runat="server"></asp:TextBox><br />
VIN:<asp:TextBox ID="VIN" runat="server"></asp:TextBox><br />
Trim:<asp:TextBox ID="Trim" runat="server"></asp:TextBox><br />
Book Value:<asp:TextBox ID="BookVal" runat="server"></asp:TextBox><br />
Base Value:<asp:TextBox ID="BaseVal" runat="server"></asp:TextBox><br />
Discount Value:<asp:TextBox ID="DisVal" runat="server"></asp:TextBox><br />
    <asp:Button ID="Button1" runat="server" OnClick="AddVehicle" Text="Add Vehicle" />
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="LoginPlaceHolder1" runat="server">
You are logged in as: <br /><asp:Label ID="Label1" runat="server" Font-Bold="true" ></asp:Label>
<br />
<br />
    <asp:Button ID="Logout_Button" runat="server" onclick="Logout_Click" 
        Text="Logout" />
<br />
</asp:Content>
