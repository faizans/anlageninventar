<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMaster/NoMenuMaster.Master" AutoEventWireup="true" CodeBehind="ManageInsuranceCategory.aspx.cs" Inherits="Client.Site.Administrator.ManageInsuranceCategory" %>

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
                <asp:Label ID="Label3" runat="server" Text="Ist Standart" CssClass="element_label"></asp:Label>
                <asp:CheckBox ID="chbIsDefault" runat="server" Checked="false"/>
            </div>
            <div class="input_form_row">
                <asp:Label ID="Label2" runat="server" Text="" CssClass="element_label"></asp:Label>
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
