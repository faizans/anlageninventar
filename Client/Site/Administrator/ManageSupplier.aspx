<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMaster/NoMenuMaster.Master" AutoEventWireup="true" CodeBehind="ManageSupplier.aspx.cs" Inherits="Client.Site.Administrator.ManageSupplier" %>

<%@ Register Src="~/Site/Controls/ListBox2/ListBoxControl.ascx" TagPrefix="uc1" TagName="ListBoxControl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" Width="300px">
        <div class="content_input_form">
            <!-- Name -->
            <div class="input_form_row">
                <asp:Label ID="Label1" runat="server" Text="Name" CssClass="element_label"></asp:Label>
                <telerik:RadTextBox ID="rtbName" runat="server" Width="300px" ReadOnly="false"></telerik:RadTextBox>
                <asp:RequiredFieldValidator ID="rfvName" runat="server" ErrorMessage="Eingabe benötigt." ControlToValidate="rtbName" Display="Dynamic"/>
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
                <telerik:RadTextBox ID="rtbBranchPlace" runat="server" Width="300px" Enabled="false">
                </telerik:RadTextBox>
            </div>
            <div class="input_form_row">
                <asp:Label ID="Label5" runat="server" Text="PLZ" CssClass="element_label"></asp:Label>
                <telerik:RadTextBox ID="rtbBranchPlz" runat="server" Width="300px" Enabled="false">
                </telerik:RadTextBox>
            </div>
            <div class="input_form_row">
                <asp:Label ID="Label3" runat="server" Text="Kommentar" CssClass="element_label"></asp:Label>
                <telerik:RadTextBox ID="rtbComment" runat="server" Width="300px" TextMode="MultiLine" Rows="5" Enabled="false">
                </telerik:RadTextBox>
            </div>
            <div class="input_form_row">
                <telerik:RadButton ID="btnApply" runat="server" Text="Übernehmen" OnClick="btnApply_Click" CausesValidation="false" Enabled="false"/>
            </div>
        </div>
        <div class="input_interaction_row">
            <telerik:RadButton ID="btnBack" runat="server" Text="Back" OnClick="btnBack_Click" CausesValidation="false" />
            <telerik:RadButton ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" />
        </div>
    </telerik:RadAjaxPanel>
    <telerik:RadWindowManager ID="RadWindowManager1" runat="server">
    </telerik:RadWindowManager>
    <script>
        function alertCallBackFn(arg) {

        }

    </script>
</asp:Content>
