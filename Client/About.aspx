<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMaster/StandardMaster.Master" AutoEventWireup="true" CodeBehind="About.aspx.cs" Inherits="Client.About" %>

<%@ Register Src="~/Site/Controls/UserSearchControl/UserSearchBox.ascx" TagPrefix="uc1" TagName="UserSearchBox" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <uc1:usersearchbox runat="server" id="UserSearchBox" Width="300px" MinimumInput="3"/>
</asp:Content>
