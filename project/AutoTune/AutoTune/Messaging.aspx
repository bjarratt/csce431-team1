<%@ Page Language="C#" MasterPageFile="~/Layout.Master" AutoEventWireup="true" CodeBehind="Messaging.aspx.cs" Inherits="AutoTune.WebForm2" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContentPlaceHolder" runat="server">
Messaging
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HeadContentPlaceHolder" runat="server">
</asp:Content>

<asp:Content ID="MessageButton" ContentPlaceHolderID="MessageContentPlaceholder" runat="server">
<li class="active"><asp:LinkButton ID="LinkButton2" runat="server" OnClick="MessageClick" Height="16px"
Width="70px">Messaging</asp:LinkButton></li>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
    <h2 class="title"><a href="#">Welcome to your AutoTune Messaging Page!</a></h2>
&nbsp;<asp:Panel ID="MessagesTable" runat="server" Height="467px">
        <asp:Label ID="MessagesBox" runat="server" BorderStyle="Double" 
            Font-Bold="False" Font-Italic="False" Font-Size="Medium" Height="184px" 
            Width="421px"></asp:Label>
        <br />
        <br />
        <asp:Button ID="MarkAsRead" runat="server" onclick="MarkAsRead_Click" 
            Text="Mark Messages As Read" />
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
            onclick="SendButton_Click" Text="Send" Width="91px" />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
    </asp:Panel>
</asp:Content>
<asp:Content ID="LoginContent" ContentPlaceHolderID="LoginPlaceHolder1" runat="server">
    You are logged in as: <br /><asp:Label ID="Label1" runat="server" Font-Bold="true" ></asp:Label>
<br />
<br />
    <asp:Button ID="Logout_Button" runat="server" onclick="Logout_Click" 
        Text="Logout" />
<br />
</asp:Content>
