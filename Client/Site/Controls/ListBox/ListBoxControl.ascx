<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ListBoxControl.ascx.cs" Inherits="Client.Site.Controls.ListBox.ListBoxControl" %>
<div style="width:300px;">
    <div class="ListBoxDiv">
        <telerik:RadListBox ID="ListBox" runat="server" Height="150px" Width="300px" DataTextField="Text" DataValueField="Value" 
            OnSelectedIndexChanged="ListBox_SelectedIndexChanged" AutoPostBack="true"></telerik:RadListBox>
    </div>
    <div class="ListBoxButtonsDiv" style="margin-top:10px; float:right;">
        <telerik:RadButton ID="btnAdd" runat="server" Text="" Width="20px" OnClick="btnAdd_Click">
            <Icon PrimaryIconCssClass="rbAdd" PrimaryIconLeft="4" PrimaryIconTop="4" />
        </telerik:RadButton>
        <telerik:RadButton ID="btnRemove" runat="server" Text="" Width="20px" OnClick="btnRemove_Click">
            <Icon PrimaryIconCssClass="rbRemove" PrimaryIconLeft="4" PrimaryIconTop="4" />
        </telerik:RadButton>
    </div>
</div>
