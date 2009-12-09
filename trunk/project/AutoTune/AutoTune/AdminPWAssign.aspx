<%@ Page Language="C#" MasterPageFile="~/Layout.Master" AutoEventWireup="true" CodeBehind="AdminPWAssign.aspx.cs" Inherits="AutoTune.WebForm3" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContentPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HeadContentPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MessageContentPlaceholder" runat="server">
<asp:LinkButton ID="LinkButton2" runat="server" OnClick="MessageClick" Height="16px"
Width="70px">Messaging</asp:LinkButton>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
<div>
    	<h2 class="title"><a href="#">Assign a Temporary Password to an Employee</a></h2>
    	<asp:Panel ID="Panel1" runat="server" Height="244px">
        Employee:<br />
        <asp:TextBox ID="usernameBox" runat="server"></asp:TextBox>
        <br />
        <br />
        New password:<br />
        <asp:TextBox ID="NewPWBox" runat="server" TextMode="Password"></asp:TextBox>
        <br />
        <br />
        <asp:Button ID="SubmitButton" runat="server" onclick="SubmitButton_Click" 
            Text="Submit" />
        <br />
        <br />
        <asp:Label ID="AssignedLabel" runat="server" Font-Bold="True" ForeColor="Lime"></asp:Label>
        <br />
        <br />
    </asp:Panel>
</div>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="LoginPlaceHolder1" runat="server">
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
