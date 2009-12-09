<%@ Page Language="C#" MasterPageFile="~/Layout.Master" AutoEventWireup="true" CodeBehind="ResetPassword.aspx.cs" Inherits="AutoTune.ResetPassword" Title="Untitled Page" %>
<asp:Content ID="TitleContent" ContentPlaceHolderID="TitleContentPlaceHolder" runat="server">
    AutoTune - Password Reset
</asp:Content>
<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadContentPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="LoginContent" ContentPlaceHolderID="LoginPlaceHolder1" runat="server">
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
	<div style="border: 1px solid #000000; margin: 16px; padding: 8px;">
	<h1>Password Reset</h1>
	<h2><asp:Label ID="WarningLabel" runat="server"></asp:Label></h2>
	<table>
		<tr>
			<td align="right">Username:</td>
			<td align="left"><asp:TextBox ID="UsernameTextBox" runat="server"></asp:TextBox></td>
		</tr>
		<tr>
			<td align="right">Temporary Password:</td>
			<td align="left"><asp:TextBox ID="TemporaryTextBox" TextMode="Password" runat="server"></asp:TextBox></td>
		</tr>
		<tr>
			<td align="right">Password:</td>
			<td align="left"><asp:TextBox ID="PasswordTextBox" TextMode="Password" runat="server"></asp:TextBox></td>
		</tr>
		<tr>
			<td align="right">Confirm:</td>
			<td align="left"><asp:TextBox ID="ConfirmTextBox" TextMode="Password" runat="server"></asp:TextBox></td>
		</tr>
		<tr>
			<td align="right" colspan="2"><asp:Button ID="ResetButton" Text="Reset" 
					runat="server" onclick="ResetButton_Click" /></td>
		</tr>
	</table>
	</div>
</asp:Content>				