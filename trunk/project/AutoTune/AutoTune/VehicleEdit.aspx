<%@ Page Language="C#" MasterPageFile="~/Layout.Master" AutoEventWireup="true" CodeBehind="VehicleEdit.aspx.cs" Inherits="AutoTune.WebForm5" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContentPlaceHolder" runat="server">
Edit Vehicle
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HeadContentPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MessageContentPlaceholder" runat="server">
<asp:LinkButton ID="LinkButton2" runat="server" OnClick="MessageClick" Height="16px"
Width="70px">Messaging</asp:LinkButton>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
<div style="border: 1px solid #000000; margin: 16px; padding: 8px;">
	Year: <asp:TextBox ID="YearTextBox" runat="server"></asp:TextBox><br />
	Make: <asp:TextBox ID="MakeTextBox" runat="server"></asp:TextBox><br />
	Model: <asp:TextBox ID="ModelTextBox" runat="server"></asp:TextBox><br />
	VIN: <asp:TextBox ID="VINTextBox" runat="server"></asp:TextBox><br />
	Trim: <asp:TextBox ID="TrimTextBox" runat="server"></asp:TextBox><br />
	Book Value: <asp:TextBox ID="BookValTextBox" runat="server"></asp:TextBox><br />
	Base Value: <asp:TextBox ID="BaseValTextBox" runat="server"></asp:TextBox><br />
	Discount Value: <asp:TextBox ID="DisValTextBox" runat="server"></asp:TextBox><br />
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
