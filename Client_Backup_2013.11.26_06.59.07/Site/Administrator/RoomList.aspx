<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMaster/StandardMaster.Master" AutoEventWireup="true" CodeBehind="RoomList.aspx.cs" Inherits="Client.Site.Administrator.RoomList" %>

<%@ Register Src="~/Site/Controls/RoomTree/RoomTreeControl.ascx" TagPrefix="uc1" TagName="RoomTreeControl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder" runat="server">

    <uc1:RoomTreeControl runat="server" ID="RoomTreeControl" />

</asp:Content>
