<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMaster/NoMenuMaster.Master" AutoEventWireup="true" CodeBehind="ManageArticle.aspx.cs" Inherits="Client.Site.Administrator.ManageArticle" %>

<%@ Register Src="~/Site/Controls/UserSearchControl/UserSearchBox.ascx" TagPrefix="uc1" TagName="UserSearchBox" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <telerik:RadAjaxPanel ID="pnAjax" runat="server">
        <div class="content_input_form">
            <!-- Name -->
            <div class="input_form_row">
                <asp:Label ID="Label1" runat="server" Text="Name" CssClass="element_label"></asp:Label>
                <telerik:RadTextBox ID="rtbName" runat="server" Width="300px" ReadOnly="false"></telerik:RadTextBox>
                <asp:RequiredFieldValidator ID="rfvName" runat="server" ErrorMessage="Eingabe benötigt." ControlToValidate="rtbName" Display="Dynamic"/>
            </div>

            <!-- Amount -->
            <div class="input_form_row">
                <asp:Label ID="Label3" runat="server" Text="Anzahl" CssClass="element_label"></asp:Label>
                <telerik:RadNumericTextBox ID="rtbAmount" runat="server" Width="300px"
                    OnTextChanged="rtbAmount_TextChanged" AutoPostBack="true">
                </telerik:RadNumericTextBox>
                <asp:RequiredFieldValidator ID="rfvAmount" runat="server" ErrorMessage="Eingabe benötigt." ControlToValidate="rtbAmount" Display="Dynamic" />
            </div>

            <!-- Barcode -->
            <div class="input_form_row">
                <asp:Label ID="Label2" runat="server" Text="Barcode" CssClass="element_label"></asp:Label>
                <telerik:RadTextBox ID="rtbBarcode" runat="server" Width="300px" ReadOnly="false"></telerik:RadTextBox>
                <asp:RequiredFieldValidator ID="rfvBarcode" runat="server" ErrorMessage="Eingabe benötigt." ControlToValidate="rtbBarcode" Display="Dynamic" />
            </div>

            <asp:Panel runat="server" ID="GroupPanel" Visible="false">
                <!-- Group Name -->
                <div class="input_form_row">
                    <asp:Label ID="Label6" runat="server" Text="Gruppenname" CssClass="element_label"></asp:Label>
                    <telerik:RadTextBox ID="rtbGroupName" runat="server" Width="300px" ReadOnly="false"></telerik:RadTextBox>
                </div>

                <!-- Group Code -->
                <div class="input_form_row">
                    <asp:Label ID="Label8" runat="server" Text="Gruppencode" CssClass="element_label"></asp:Label>
                    <telerik:RadTextBox ID="rtbGroupBarcode" runat="server" Width="300px" ReadOnly="false"></telerik:RadTextBox>
                </div>
            </asp:Panel>

            <!-- Price -->
            <div class="input_form_row">
                <asp:Label ID="Label4" runat="server" Text="Preis" CssClass="element_label"></asp:Label>
                <telerik:RadNumericTextBox ID="rtbPrice" runat="server" Width="300px"></telerik:RadNumericTextBox>
                <asp:RequiredFieldValidator ID="rfvPrice" runat="server" ErrorMessage="Eingabe benötigt." ControlToValidate="rtbPrice" Display="Dynamic" />
            </div>

            <!-- AcquisitionDate -->
            <div class="input_form_row">
                <asp:Label ID="Label5" runat="server" Text="Anschaffungsdatum" CssClass="element_label"></asp:Label>
                <telerik:RadDatePicker ID="rdpAcquisitionDate" runat="server" MinDate="1900-01-01" AutoPostBack="true" Width="300px">
                    <Calendar ID="Calendar1" RangeMinDate="1900-01-01" runat="server">
                    </Calendar>
                </telerik:RadDatePicker>
                 <asp:RequiredFieldValidator ID="rfvDate" runat="server" ErrorMessage="Eingabe benötigt." ControlToValidate="rdpAcquisitionDate" Display="Dynamic" />
            </div>

            <!-- Supplier -->
            <div class="input_form_row">
                <asp:Label ID="Label15" runat="server" Text="Lieferant" CssClass="element_label"></asp:Label>
                <telerik:RadComboBox ID="rcbSupplier" runat="server" Width="300px"
                    DataValueField="SupplierId" DataTextField="Name"
                    OnSelectedIndexChanged="rcbLieferant_SelectedIndexChanged" AutoPostBack="true"
                    EmptyMessage="- Bitte auswählen -">
                </telerik:RadComboBox>
            </div>

            <!-- SupplierBranch -->
            <div class="input_form_row">
                <asp:Label ID="Label16" runat="server" Text="Standort" CssClass="element_label"></asp:Label>
                <telerik:RadComboBox ID="rcbSupplierBranch" runat="server" Width="300px"
                    DataValueField="SupplierBranchId" DataTextField="Name"
                    EmptyMessage="- Bitte auswählen -">
                </telerik:RadComboBox>
            </div>

            <!-- ArticleCategory -->
            <div class="input_form_row">
                <asp:Label ID="Label9" runat="server" Text="Aritkelkategorie" CssClass="element_label"></asp:Label>
                <telerik:RadComboBox ID="rcbArticleCategory" runat="server" Width="300px"
                    DataValueField="ArticleCategoryId" DataTextField="Name"
                    EmptyMessage="- Bitte auswählen -">
                </telerik:RadComboBox>
            </div>

            <!-- InsuranceCategory -->
            <div class="input_form_row">
                <asp:Label ID="Label10" runat="server" Text="Versicherungskategorie" CssClass="element_label"></asp:Label>
                <telerik:RadComboBox ID="rcbInsuranceCategory" runat="server" Width="300px"
                    DataValueField="InsuranceCategoryId" DataTextField="Name"
                    EmptyMessage="- Bitte auswählen -">
                </telerik:RadComboBox>
            </div>

            <!-- DepreciationCategory -->
            <div class="input_form_row">
                <asp:Label ID="Label11" runat="server" Text="Abschreibungskategorie" CssClass="element_label"></asp:Label>
                <telerik:RadComboBox ID="rcbDepreciationCategory" runat="server" Width="300px"
                    DataValueField="DepreciationCategoryId" DataTextField="Name"
                    OnSelectedIndexChanged="rcbDepreciationCategory_SelectedIndexChanged" AutoPostBack="true"
                    EmptyMessage="- Bitte auswählen -">
                </telerik:RadComboBox>
            </div>
            <!-- Depreciation -->
            <div class="input_form_row">
                <asp:Label ID="Label12" runat="server" Text="Interval" CssClass="element_label"></asp:Label>
                <telerik:RadComboBox ID="rcbDepreciationInterval" runat="server" Width="300px"
                    DataValueField="DepreciationId" DataTextField="Name"
                    EmptyMessage="- Bitte auswählen -">
                </telerik:RadComboBox>
            </div>

            <!-- Building -->
            <div class="input_form_row">
                <asp:Label ID="Label17" runat="server" Text="Gebäude" CssClass="element_label"></asp:Label>
                <telerik:RadComboBox ID="rcbBuilding" runat="server" Width="300px"
                    DataValueField="BuildingId" DataTextField="Name"
                    OnSelectedIndexChanged="rcbBuilding_SelectedIndexChanged" AutoPostBack="true"
                    EmptyMessage="- Bitte auswählen -">
                </telerik:RadComboBox>
            </div>
            <!-- Floor -->
            <div class="input_form_row">
                <asp:Label ID="Label18" runat="server" Text="Stockwerk" CssClass="element_label"></asp:Label>
                <telerik:RadComboBox ID="rcbFloor" runat="server" Width="300px"
                    DataValueField="FloorId" DataTextField="Name"
                    OnSelectedIndexChanged="rcbFloor_SelectedIndexChanged" AutoPostBack="true"
                    EmptyMessage="- Bitte auswählen -">
                </telerik:RadComboBox>
            </div>
            <!-- Room -->
            <div class="input_form_row">
                <asp:Label ID="Label19" runat="server" Text="Raum" CssClass="element_label"></asp:Label>
                <telerik:RadComboBox ID="rcbRoom" runat="server" Width="300px"
                    DataValueField="RoomId" DataTextField="Name"
                    EmptyMessage="- Bitte auswählen -">
                </telerik:RadComboBox>
                <asp:RequiredFieldValidator ID="rfvRoom" runat="server" ErrorMessage="Eingabe benötigt." ControlToValidate="rcbRoom" Display="Dynamic" />
            </div>

            <!-- Old Barcode -->
            <div class="input_form_row">
                <asp:Label ID="Label13" runat="server" Text="Alter Barcode" CssClass="element_label"></asp:Label>
                <telerik:RadTextBox ID="rtbOldBarcode" runat="server" Width="300px" ReadOnly="false"></telerik:RadTextBox>
            </div>

            <!-- Comment -->
            <div class="input_form_row">
                <asp:Label ID="Label14" runat="server" Text="Kommentar" CssClass="element_label"></asp:Label>
                <telerik:RadTextBox ID="rtbComment" runat="server" Width="300px" ReadOnly="false" TextMode="MultiLine" Rows="8"></telerik:RadTextBox>
            </div>

            <!-- IsAvailable -->
            <div class="input_form_row">
                <asp:Label ID="Label7" runat="server" Text="Ist vorhanden" CssClass="element_label"></asp:Label>
                <asp:CheckBox ID="chbIsAvailable" runat="server" />
            </div>

        </div>
        <div class="input_interaction_row">
            <asp:Button ID="btnBack" runat="server" Text="Zurück" OnClick="btnBack_Click" CausesValidation="false" />
            <asp:Button ID="btnSave" runat="server" Text="Speichern" OnClick="btnSave_Click" CausesValidation="true"/>
        </div>
    </telerik:RadAjaxPanel>
</asp:Content>
