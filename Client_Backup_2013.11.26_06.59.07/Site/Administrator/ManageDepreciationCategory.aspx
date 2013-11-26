<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMaster/NoMenuMaster.Master" AutoEventWireup="true" CodeBehind="ManageDepreciationCategory.aspx.cs" Inherits="Client.Site.Administrator.ManageDepreciationCategory" %>

<%@ Register Src="~/Site/Controls/ListBox2/ListBoxControl.ascx" TagPrefix="uc1" TagName="ListBoxControl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server">
        <div class="content_input_form">
            <!-- Name -->
            <div class="input_form_row">
                <asp:Label ID="Label1" runat="server" Text="Name" CssClass="element_label"></asp:Label>
                <telerik:RadTextBox ID="rtbName" runat="server" Width="300px" ReadOnly="false"></telerik:RadTextBox>
                <asp:RequiredFieldValidator ID="rfvName" runat="server" ErrorMessage="Eingabe benötigt." ControlToValidate="rtbName" Display="Dynamic" />
            </div>
            <div class="input_form_row">
                <asp:Label ID="Label4" runat="server" Text="Zeitspanne" CssClass="element_label"></asp:Label>
                <telerik:RadNumericTextBox ID="rtbTimeSpan" runat="server" Width="300px">
                    <NumberFormat GroupSeparator="" DecimalDigits="0" />
                </telerik:RadNumericTextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Eingabe benötigt." ControlToValidate="rtbTimeSpan" Display="Dynamic" />
            </div>
        </div>
        <div class="input_interaction_row">
            <telerik:RadButton ID="btnBack" runat="server" Text="Zurück" OnClick="btnBack_Click" CausesValidation="false" />
            <telerik:RadButton ID="btnSave" runat="server" Text="Speichern" OnClick="btnSave_Click" />
        </div>
    </telerik:RadAjaxPanel>
    <telerik:RadWindowManager ID="RadWindowManager1" runat="server">
    </telerik:RadWindowManager>
    <script>
        function alertCallBackFn(arg) {

        }

    </script>
</asp:Content>
