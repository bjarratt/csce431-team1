<%@ Page Language="C#" MasterPageFile="~/Layout.Master" AutoEventWireup="true" CodeBehind="ChangePassword.aspx.cs" Inherits="AutoTune.WebForm3" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContentPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HeadContentPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MessageContentPlaceholder" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
    <asp:Panel ID="Panel1" runat="server" Height="244px">
        Current username:<br />
        <asp:TextBox ID="usernameBox" runat="server"></asp:TextBox>
        <br />
        <br />
        Current password:<br />
        <asp:TextBox ID="CurrentPWBox" runat="server" TextMode="Password"></asp:TextBox>
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
        <asp:Label ID="AuthenticLabel" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
        <br />
        <br />
    </asp:Panel>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="LoginPlaceHolder1" runat="server">
</asp:Content>
