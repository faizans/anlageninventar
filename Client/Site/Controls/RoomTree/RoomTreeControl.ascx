<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="RoomTreeControl.ascx.cs" Inherits="Client.Site.Controls.RoomTree.RoomTreeControl" %>
<%@ Register Src="~/Site/Controls/UserSearchControl/UserSearchBox.ascx" TagPrefix="uc1" TagName="UserSearchBox" %>

<telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server">

    <div style="margin-bottom: 20px;">
        <telerik:RadButton ID="btnAddBuilding" runat="server" Text="Neues Gebäude" OnClick="btnAddBuilding_Click" Enabled="false"></telerik:RadButton>
        <telerik:RadButton ID="btnAddFloor" runat="server" Text="Neuer Stock" OnClick="btnAddFloor_Click" Enabled="false"></telerik:RadButton>
        <telerik:RadButton ID="btnAddRoom" runat="server" Text="Neuer Raum" OnClick="btnAddRoom_Click" Enabled="false"></telerik:RadButton>
        <telerik:RadButton ID="btnDelete" runat="server" Text="Delete" OnClick="btnDelete_Click" Enabled="false"></telerik:RadButton>
    </div>

    <div style="float: left; height: 300px; width: 300px; border: 1px solid black; padding: 10px; background-color:white;">
        <telerik:RadTreeView ID="RadTreeView1" runat="server" DataFieldID="Id" DataFieldParentID="ParentId" DataTextField="Text"
            OnNodeClick="RadTreeView1_NodeClick" OnNodeDataBound="RadTreeView1_NodeDataBound">
            
        </telerik:RadTreeView>
    </div>

    <div style="float: left; height: 300px; width: 300px; border: 1px solid black; margin-left: 20px; padding: 10px; background-color:white;" runat="server"  id="EditForm" visible="false">
        <div id="Div1" class="content_input_form" runat="server">
            <div class="input_form_row">
                <asp:Label ID="Label0" runat="server" Text="Name" CssClass="element_label"></asp:Label>
                <telerik:RadTextBox ID="txtNodeName" runat="server" Width="200px"></telerik:RadTextBox>
            </div>
        </div>
        <div class="content_input_form" runat="server" id="ResponsibleAttribute" visible="false">
            <div class="input_form_row">
                <asp:Label ID="Label1" runat="server" Text="Verantwortlich" CssClass="element_label"></asp:Label>
                <uc1:UserSearchBox runat="server" ID="UserSearchBox" Width="200px" MinimumInput="3"/>
            </div>
        </div>
        <div class="input_interaction_row">
            <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" />
        </div>
    </div>
    <telerik:RadWindowManager ID="RadWindowManager1" runat="server">
    </telerik:RadWindowManager>
    <script>
        function alertCallBackFn(arg) {

        }

    </script>

</telerik:RadAjaxPanel>

