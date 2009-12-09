<%@ Page Language="C#" MasterPageFile="~/Layout.Master" AutoEventWireup="true" CodeBehind="AddDealership.aspx.cs" Inherits="AutoTune.AddDealership" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContentPlaceHolder" runat="server">
 Administrator Add Dealership
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HeadContentPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MessageContentPlaceholder" runat="server">
    <asp:LinkButton ID="LinkButton2" runat="server" OnClick="MessageClick" Height="16px"
Width="70px">Messaging</asp:LinkButton>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
<div>
    	<h2 class="title"><a href="#">View/Edit AutoTune Dealership Listing</a></h2>
    <asp:Panel ID="Panel3" runat="server" Height="305px">
        <br />
        Dealership Name:<br />
        <asp:TextBox ID="NameBox" runat="server"></asp:TextBox>
        <br />
        <br />
        Location:<br />
        <asp:TextBox ID="LocationBox" runat="server"></asp:TextBox>
        <br />
        <br />
        Email:<br />
        <asp:TextBox ID="EmailBox" runat="server"></asp:TextBox>
        <br />
        <br />
        Phone:<br />
        <asp:TextBox ID="PhoneBox" runat="server"></asp:TextBox>
        <br />
        <br />
        <asp:Button ID="SubmitButton" runat="server" Text="Submit" 
            onclick="SubmitButton_Click" />
        <br />
        <br />
        <asp:Label ID="SuccessLabel" runat="server"></asp:Label>
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
<asp:HyperLink ID="changepassword" runat="server" 
            NavigateUrl="ChangePassword.aspx">Change Password</asp:HyperLink>
<br />
<br />
<asp:HyperLink ID="HyperLink2" runat="server" 
            NavigateUrl="AdminHome.aspx">Return to Admin Home</asp:HyperLink>
</asp:Content>
