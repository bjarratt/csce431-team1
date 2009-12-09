<%@ Page Language="C#" MasterPageFile="~/Layout.Master" AutoEventWireup="true" CodeBehind="Messaging.aspx.cs" Inherits="AutoTune.WebForm2" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContentPlaceHolder" runat="server">
Messaging
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HeadContentPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
<h2 class="title"><a href="#">Welcome to your AutoTune Messaging Page!</a></h2>
&nbsp;<asp:Panel ID="Panel1" runat="server" Height="467px">
        <asp:TextBox ID="MessagesBox" runat="server" BorderStyle="Double" 
            Height="105px" Width="574px"></asp:TextBox>
        <br />
        <br />
        <br />
        <br />
        <br />
        Message Recipients(separated by commas)<br />
        <asp:TextBox ID="RecipientsBox" runat="server" BorderStyle="Double" 
            Height="18px" Width="432px"></asp:TextBox>
        <br />
        <br />
        <br />
        Message<br />
        <asp:TextBox ID="NewMessageBox" runat="server" BorderStyle="Double" 
            Height="105px" Width="430px"></asp:TextBox>
        <br />
        <br />
        &nbsp;&nbsp;
        <asp:Button ID="SendButton" runat="server" Height="23px" 
            onclick="SendButton_Click" Text="Send Message" Width="91px" />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
    </asp:Panel>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="LoginPlaceHolder1" runat="server">
</asp:Content>
