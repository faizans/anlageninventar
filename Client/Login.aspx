<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMaster/NoMenuMaster.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Client._Login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server">
        <telerik:RadTextBox ID="rtbUsername" runat="server" Label="Benutzername" LabelWidth="180px" Width="400px"></telerik:RadTextBox><br />
        <telerik:RadTextBox ID="rtbPassword" runat="server" Label="Password" LabelWidth="180px" TextMode="Password" Width="400px"></telerik:RadTextBox><br />
        <telerik:RadButton ID="btnLogin" runat="server" Text="Login" OnClick="btnLogin_Click"></telerik:RadButton>
        <asp:Label ID="lblAlert" runat="server" Text=""></asp:Label>
    </telerik:RadAjaxPanel>
</asp:Content>
