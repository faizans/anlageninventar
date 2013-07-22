﻿<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMaster/NoMenuMaster.Master" AutoEventWireup="true" CodeBehind="ManageSupplier.aspx.cs" Inherits="Client.Site.Administrator.ManageSupplier" %>

<%@ Register Src="~/Site/Controls/ListBox2/ListBoxControl.ascx" TagPrefix="uc1" TagName="ListBoxControl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" Width="300px">
        <div class="content_input_form">
            <!-- Name -->
            <div class="input_form_row">
                <asp:Label ID="Label1" runat="server" Text="Name" CssClass="element_label"></asp:Label>
                <telerik:RadTextBox ID="rtbName" runat="server" Width="300px" ReadOnly="false"></telerik:RadTextBox>
            </div>
            <div class="input_form_row">
                <asp:Label ID="Label2" runat="server" Text="" CssClass="element_label"></asp:Label>
            </div>
            <div class="input_form_row">
                <asp:Label ID="Label6" runat="server" Text="Standorte" CssClass="element_label"></asp:Label>
                <div style="float: left;">
                    <uc1:ListBoxControl runat="server" ID="ListBoxControl"
                        OnSelectedIndexChanged="ListBoxControl_SelectedIndexChanged"
                        OnAddNewItem="ListBoxControl_AddNewItem"
                        OnItemRemove="ListBoxControl_ItemRemove" />
                </div>
            </div>
            <div class="input_form_row">
                <asp:Label ID="Label4" runat="server" Text="Ort" CssClass="element_label"></asp:Label>
                <telerik:RadTextBox ID="rtbBranchPlace" runat="server" Width="300px" ReadOnly="false"
                    OnTextChanged="rtbBranchPlz_TextChanged" AutoPostBack="true">
                </telerik:RadTextBox>
            </div>
            <div class="input_form_row">
                <asp:Label ID="Label5" runat="server" Text="PLZ" CssClass="element_label"></asp:Label>
                <telerik:RadTextBox ID="rtbBranchPlz" runat="server" Width="300px" ReadOnly="false"
                    OnTextChanged="rtbBranchPlz_TextChanged" AutoPostBack="true">
                </telerik:RadTextBox>
            </div>
        </div>
        <div class="input_interaction_row">
            <asp:Button ID="btnBack" runat="server" Text="Back" OnClick="btnBack_Click" CausesValidation="false" />
            <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" />
        </div>
    </telerik:RadAjaxPanel>

</asp:Content>
