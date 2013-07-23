<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UserSearchBox.ascx.cs" Inherits="Client.Site.Controls.UserSearchControl.UserSearchBox" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<div class="UserSearchControl">
    <telerik:RadComboBox ID="rcbSearch" runat="server" EmptyMessage="Enter Email" MarkFirstMatch="true" CausesValidation="false"
        EnableLoadOnDemand="true" AutoPostBack="true" OnItemsRequested="rcbSearch_ItemsRequested" OnSelectedIndexChanged="rcbSearch_SelectedIndexChanged">
    </telerik:RadComboBox>
</div>
