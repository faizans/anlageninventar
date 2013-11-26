<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMaster/NoMenuMaster.Master" AutoEventWireup="true" CodeBehind="ManageUser.aspx.cs" Inherits="Client.Site.Administrator.ManageUser" %>

<%@ Register Src="~/Site/Controls/UserSearchControl/UserSearchBox.ascx" TagPrefix="uc1" TagName="UserSearchBox" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <telerik:RadAjaxPanel ID="pnAjax" runat="server">
        <div class="content_input_form">
            <!-- ADUSer -->
            <div class="input_form_row">
                <asp:Label ID="Label9" runat="server" Text="Benutzersuche" CssClass="element_label"></asp:Label>
                <uc1:UserSearchBox runat="server" ID="UserSearchBox"  Width="300px" MinimumInput="3" OnUserSearchBoxIndexChanged="UserSearchBox_UserSearchBoxIndexChanged" />
            </div>
            <!-- FirstName -->
            <div class="input_form_row">
                <asp:Label ID="Label1" runat="server" Text="Vorname" CssClass="element_label"></asp:Label>
                <telerik:RadTextBox ID="rtbFirstNAme" runat="server" Width="300px" ReadOnly="false"></telerik:RadTextBox>
            </div>
            <!-- LastName -->
            <div class="input_form_row">
                <asp:Label ID="Label2" runat="server" Text="Nachname" CssClass="element_label"></asp:Label>
                <telerik:RadTextBox ID="rtbLastName" runat="server" Width="300px" ReadOnly="false"></telerik:RadTextBox>
            </div>
            <!-- Domain -->
            <div class="input_form_row">
                <asp:Label ID="Label3" runat="server" Text="Domain" CssClass="element_label"></asp:Label>
                <telerik:RadTextBox ID="rtbDomain" runat="server" Width="300px" ReadOnly="false"></telerik:RadTextBox>
            </div>
            <!-- Username -->
            <div class="input_form_row">
                <asp:Label ID="Label4" runat="server" Text="Benutzername" CssClass="element_label"></asp:Label>
                <telerik:RadTextBox ID="rtbUsername" runat="server" Width="300px" ReadOnly="false"></telerik:RadTextBox>
            </div>
            <!-- Email -->
            <div class="input_form_row">
                <asp:Label ID="Label5" runat="server" Text="Email" CssClass="element_label"></asp:Label>
                <telerik:RadTextBox ID="rtbEmail" runat="server" Width="300px" ReadOnly="false"></telerik:RadTextBox>
            </div>
            <!-- Password -->
            <div class="input_form_row">
                <asp:Label ID="Label6" runat="server" Text="Passwort" CssClass="element_label"></asp:Label>
                <telerik:RadTextBox ID="rtbPasswort" runat="server" Width="300px" ReadOnly="false"></telerik:RadTextBox>
            </div>
            <!-- IsAdmin -->
            <div class="input_form_row">
                <asp:Label ID="Label7" runat="server" Text="Administrator" CssClass="element_label"></asp:Label>
                 <asp:CheckBox ID="chbIsAdmin" runat="server" />
            </div>
            <!-- IsActive -->
            <div class="input_form_row">
                <asp:Label ID="Label8" runat="server" Text="Aktiv" CssClass="element_label"></asp:Label>
                 <asp:CheckBox ID="chbIsActive" runat="server" />
            </div>
        </div>
        <div class="input_interaction_row">
            <telerik:RadButton ID="btnBack" runat="server" Text="Back" OnClick="btnBack_Click" CausesValidation="false" />
            <telerik:RadButton ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" />
        </div>
    </telerik:RadAjaxPanel>
</asp:Content>
