<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMaster/NoMenuMaster.Master" AutoEventWireup="true" CodeBehind="ManageDepreciationCategory.aspx.cs" Inherits="Client.Site.Administrator.ManageDepreciationCategory" %>

<%@ Register Src="~/Site/Controls/ListBox2/ListBoxControl.ascx" TagPrefix="uc1" TagName="ListBoxControl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server">
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
                <asp:Label ID="Label6" runat="server" Text="Abschreibungen" CssClass="element_label"></asp:Label>
                <div style="float: left;">
                    <uc1:ListBoxControl runat="server" ID="ListBoxControl"
                        OnSelectedIndexChanged="ListBoxControl_SelectedIndexChanged"
                        OnAddNewItem="ListBoxControl_AddNewItem"
                        OnItemRemove="ListBoxControl_ItemRemove" />
                </div>
            </div>
            <div class="input_form_row">
                <asp:Label ID="Label4" runat="server" Text="Von" CssClass="element_label"></asp:Label>
                <telerik:RadNumericTextBox ID="rtbFromYear" runat="server" Enabled="false">
                    <NumberFormat GroupSeparator="" DecimalDigits="0" /> 
                </telerik:RadNumericTextBox>
            </div>
            <div class="input_form_row">
                <asp:Label ID="Label5" runat="server" Text="Bis" CssClass="element_label"></asp:Label>
                <telerik:RadNumericTextBox ID="rtbToYear" runat="server"  Enabled="false">
                    <NumberFormat GroupSeparator="" DecimalDigits="0" /> 
                </telerik:RadNumericTextBox>
            </div>
            <div class="input_form_row">
                <asp:Button ID="btnApply" runat="server" Text="Übernehmen" OnClick="btnApply_Click" CausesValidation="false" Enabled="false" />
            </div>
        </div>
        <div class="input_interaction_row">
            <asp:Button ID="btnBack" runat="server" Text="Zurück" OnClick="btnBack_Click" CausesValidation="false" />
            <asp:Button ID="btnSave" runat="server" Text="Speichern" OnClick="btnSave_Click" />
        </div>
    </telerik:RadAjaxPanel>
    <telerik:RadWindowManager ID="RadWindowManager1" runat="server">
    </telerik:RadWindowManager>
    <script>
        function alertCallBackFn(arg) {

        }

    </script>
</asp:Content>
