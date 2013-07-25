<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMaster/StandardMaster.Master" AutoEventWireup="true" CodeBehind="About.aspx.cs" Inherits="Client.About" %>

<%@ Register Src="~/Site/Controls/UserSearchControl/UserSearchBox.ascx" TagPrefix="uc1" TagName="UserSearchBox" %>
<%@ Register Src="~/Site/Controls/RoomTree/RoomTreeControl.ascx" TagPrefix="uc1" TagName="RoomTreeControl" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <uc1:RoomTreeControl runat="server" id="RoomTreeControl" />
</asp:Content>
