﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="RoomTreeControl.ascx.cs" Inherits="Client.Site.Controls.RoomTree.RoomTreeControl" %>
<%@ Register Src="~/Site/Controls/UserSearchControl/UserSearchBox.ascx" TagPrefix="uc1" TagName="UserSearchBox" %>

<telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server">

    <div style="margin-bottom: 20px;">
        <telerik:RadButton ID="btnAddBuilding" runat="server" Text="Neues Gebäude" OnClick="btnAddBuilding_Click" Enabled="false"></telerik:RadButton>
        <telerik:RadButton ID="btnAddFloor" runat="server" Text="Neues Stockwerk" OnClick="btnAddFloor_Click" Enabled="false"></telerik:RadButton>
        <telerik:RadButton ID="btnAddRoom" runat="server" Text="Neuer Raum" OnClick="btnAddRoom_Click" Enabled="false"></telerik:RadButton>
        <telerik:RadButton ID="btnReport" runat="server" Text="Report" OnClick="btnReport_Click" Enabled="false"></telerik:RadButton>
        <telerik:RadButton ID="btnDelete" runat="server" Text="Löschen" OnClick="btnDelete_Click" Enabled="false"></telerik:RadButton>
    </div>

    <div style="margin-bottom: 20px;">
        <asp:Label ID="lblWarning" runat="server" />
    </div>

    <div style="float: left; min-height: 300px; width: 300px; border: 1px solid black; padding: 10px; background-color: white;">
        <telerik:RadTreeView ID="RadTreeView1" runat="server" DataFieldID="Id" DataFieldParentID="ParentId" DataTextField="Text"
            OnNodeClick="RadTreeView1_NodeClick" OnNodeDataBound="RadTreeView1_NodeDataBound" CheckBoxes="true" OnNodeCheck="RadTreeView1_NodeCheck">
        </telerik:RadTreeView>
    </div>

    <div style="float: left; min-height: 300px; width: 300px; border: 1px solid black; margin-left: 20px; padding: 10px; background-color: white;" runat="server" id="EditForm" visible="false">
        <div id="Div1" class="content_input_form" runat="server">
            <div class="input_form_row">
                <asp:Label ID="Label0" runat="server" Text="Name" CssClass="element_label"></asp:Label>
                <telerik:RadTextBox ID="txtNodeName" runat="server" Width="200px"></telerik:RadTextBox>
            </div>
        </div>
        <div class="content_input_form" runat="server" id="ResponsibleAttribute" visible="false">
            <div class="input_form_row">
                <asp:Label ID="Label1" runat="server" Text="Verantwortlich" CssClass="element_label"></asp:Label>
                <telerik:RadAjaxPanel ID="RadAjaxPanel2" runat="server">
                    <uc1:UserSearchBox runat="server" ID="UserSearchBox" Width="200px" MinimumInput="3" />
                </telerik:RadAjaxPanel>
            </div>
        </div>
        <div class="input_interaction_row">
            <telerik:RadButton ID="btnSave" runat="server" Text="Speichern" OnClick="btnSave_Click" />
        </div>
    </div>
</telerik:RadAjaxPanel>

