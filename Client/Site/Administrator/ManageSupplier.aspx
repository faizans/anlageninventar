﻿<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMaster/NoMenuMaster.Master" AutoEventWireup="true" CodeBehind="ManageSupplier.aspx.cs" Inherits="Client.Site.Administrator.ManageSupplier" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <telerik:RadAjaxPanel ID="pnAjax" runat="server">
        <div class="content_input_form">
            <!-- Name -->
            <div class="input_form_row">
                <asp:Label ID="Label1" runat="server" Text="Name" CssClass="element_label"></asp:Label>
                <telerik:RadTextBox ID="rtbName" runat="server" Width="300px" ReadOnly="false"></telerik:RadTextBox>
<%--                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Display="Dynamic" ControlToValidate="rtbName" ErrorMessage="Eingabe benötigt!" />--%>
            </div>
            <div class="input_form_row">
                <asp:Label ID="Label2" runat="server" Text="" CssClass="element_label"></asp:Label>
            </div>
            <div class="input_form_row">
                <asp:Label ID="Label6" runat="server" Text="Standorte" CssClass="element_label"></asp:Label>
                <telerik:RadListBox ID="branchList" runat="server" DataTextField="Place" DataValueField="SupplierBranchId" Width="300px" Height="150px"
                    AllowDelete="true" AllowAutomaticUpdates="true" OnSelectedIndexChanged="branchList_SelectedIndexChanged" SelectionMode="Single"
                    AutoPostBack="true">
                </telerik:RadListBox>
            </div>
            <div class="input_form_row">
                <asp:Label ID="Label4" runat="server" Text="Ort" CssClass="element_label"></asp:Label>
                <telerik:RadTextBox ID="rtbBranchPlace" runat="server" Width="300px" ReadOnly="false"></telerik:RadTextBox>
<%--                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" Display="Dynamic" ControlToValidate="rtbBranchPlace" ErrorMessage="Eingabe benötigt!" />--%>
            </div>
            <div class="input_form_row">
                <asp:Label ID="Label5" runat="server" Text="PLZ" CssClass="element_label"></asp:Label>
                <telerik:RadTextBox ID="rtbBranchPlz" runat="server" Width="300px" ReadOnly="false"></telerik:RadTextBox>
<%--                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" Display="Dynamic" ControlToValidate="rtbBranchPlz" ErrorMessage="Eingabe benötigt!" />--%>
            </div>
        </div>
        <div class="input_interaction_row">
            <asp:Button ID="btnBack" runat="server" Text="Back" OnClick="btnBack_Click" CausesValidation="false" />
            <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" />
        </div>
    </telerik:RadAjaxPanel>
</asp:Content>