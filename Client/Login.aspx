<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMaster/NoMenuMaster.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Client._Login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <div style="margin-left:auto; margin-right:auto; margin-top:30px; border:1px solid black; padding:20px; width:430px;">
        <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server">
            <telerik:RadTextBox ID="rtbUsername" runat="server" Label="Benutzername" LabelWidth="120px" Width="400px"></telerik:RadTextBox>
            <br />
            <br />
            <telerik:RadTextBox ID="rtbPassword" runat="server" Label="Password" LabelWidth="120px" TextMode="Password" Width="400px"></telerik:RadTextBox>
            <br />
            <br />
            <telerik:RadButton ID="btnLogin" runat="server" Text="Login" OnClick="btnLogin_Click"></telerik:RadButton>
            <asp:Label ID="lblAlert" runat="server" Text=""></asp:Label>
        </telerik:RadAjaxPanel>
    </div>
</asp:Content>
