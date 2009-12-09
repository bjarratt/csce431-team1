<%@ Page Language="C#" MasterPageFile="~/Layout.Master" AutoEventWireup="true" CodeBehind="DealerEdit.aspx.cs" Inherits="AutoTune.WebForm3" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContentPlaceHolder" runat="server">
 Edit Dealership
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HeadContentPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MessageContentPlaceholder" runat="server">
<asp:LinkButton ID="LinkButton2" runat="server" OnClick="MessageClick" Height="16px"
Width="70px">Messaging</asp:LinkButton>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
	<div style="border: 1px solid #000000; margin: 16px; padding: 8px;">
	Name: <asp:TextBox ID="NameTextBox" runat="server"></asp:TextBox><br />
	Location: <asp:TextBox ID="LocationTextBox" runat="server"></asp:TextBox><br />
	Email: <asp:TextBox ID="EmailTextBox" runat="server"></asp:TextBox><br />
	Phone: <asp:TextBox ID="PhoneTextBox" runat="server"></asp:TextBox><br />
	<asp:Button ID="SubmitButton" Text="Submit" runat="server" 
		onclick="SubmitButton_Click" />
		</div>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="LoginPlaceHolder1" runat="server">
    You are logged in as: <br /><asp:Label ID="Label1" runat="server" Font-Bold="true" ></asp:Label>
<br />
<br />
    <asp:Button ID="Logout_Button" runat="server" onclick="Logout_Click" 
        Text="Logout" />
<br />
</asp:Content>
