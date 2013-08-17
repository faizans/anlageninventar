<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UserSearchBox.ascx.cs" Inherits="Client.Site.Controls.UserSearchControl.UserSearchBox" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<div class="UserSearchControl">
    <telerik:RadComboBox ID="rcbSearch" runat="server" EmptyMessage="Bitte Email eintragen" MarkFirstMatch="true" CausesValidation="false"
        EnableLoadOnDemand="true" AutoPostBack="true" OnItemsRequested="rcbSearch_ItemsRequested" OnSelectedIndexChanged="rcbSearch_SelectedIndexChanged" ToolTip="Der Benutzer wird anhand der eingetragenen Email in der Active Directory gesucht.">
    </telerik:RadComboBox>
</div>
