<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMaster/NoMenuMaster.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Client.Site.Login.Login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server">
        <telerik:RadTextBox ID="rtbUsername" runat="server" Label="Benutzername"></telerik:RadTextBox>
        <telerik:RadTextBox ID="rtbPassword" runat="server" Label="Password" TextMode="Password"></telerik:RadTextBox>
        <telerik:RadButton ID="btnLogin" runat="server" Text="Login" OnClick="btnLogin_Click"></telerik:RadButton>
        <asp:Label ID="lblAlert" runat="server" Text=""></asp:Label>
    </telerik:RadAjaxPanel>
</asp:Content>
