<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMaster/StandardMaster.Master" EnableEventValidation="false" AutoEventWireup="true" CodeBehind="ReportView.aspx.cs" Inherits="Client.Site.Administrator.ReportView" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="rgReport">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rgReport"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>

    <telerik:RadGrid ID="rgReport" runat="server" AutoGenerateEditColumn="False" CellSpacing="0" GridLines="None" Skin="Silk" OnPreRender="rgReport_PreRender"
        AllowPaging="False" AllowSorting="True" AllowFilteringByColumn="true" ShowStatusBar="true" OnDataBound="rgReport_DataBound" OnInit="rgReport_Init">

        <ClientSettings>
            <Scrolling AllowScroll="True" UseStaticHeaders="True"></Scrolling>
        </ClientSettings>

        <ExportSettings HideStructureColumns="true" />

        <MasterTableView DataKeyNames="ArticleId" AutoGenerateColumns="False" AllowFilteringByColumn="true" ShowFooter="true"
            CssClass="MasterTableViewNoHeight" CommandItemDisplay="Top">

            <CommandItemTemplate>
                <div style="padding: 5px;">
                    <div style="float: left; margin-left: 10px">
                        <telerik:RadNumericTextBox ID="rtbYear" runat="server" Label="Abschreibung für Jahr" MinValue="1900" MaxValue="2100" Type="Number" Width="300px" LabelWidth="160px"
                            OnTextChanged="rtbYear_TextChanged" AutoPostBack="true">
                            <NumberFormat GroupSeparator="" DecimalDigits="0" />
                        </telerik:RadNumericTextBox>

                        <telerik:RadButton ID="btnCalculate" runat="server" Text="Berechnen" AutoPostBack="true" OnClick="btnApplyYear_Click"></telerik:RadButton>
                    </div>
                    <div style="float: right;">
                        <telerik:RadComboBox ID="rcbExcelTemplate" runat="server" DataValueField="FullName" DataTextField="Name" Label="Excelvorlage" AutoPostBack="true" OnSelectedIndexChanged="rcbExcelTemplate_SelectedIndexChanged"></telerik:RadComboBox>
                        <div style="margin-right: 10px; margin-left: 10px; float: right;">
                            <asp:ImageButton ID="btnExportToExcel" ImageUrl="~/Resources/Images/Icons/ExcelBiff.png" ToolTip="Exportiere zu Excel"
                                OnClick="btnExportToExcel_Click" runat="server" CssClass="ImageButtons" Height="23px" />
                        </div>
                    </div>
                </div>
            </CommandItemTemplate>

            <Columns>
                <telerik:GridBoundColumn DataField="ArticleId" Display="false" HeaderText="ArticleId" SortExpression="ArticleId" UniqueName="ArticleId" DataType="System.Int32" FilterControlAltText="Filter ArticleId column">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="Name" HeaderText="Name" SortExpression="Name" UniqueName="Name" FilterControlAltText="Filter Name column" HeaderStyle-Width="180px" ItemStyle-Width="180px">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="Barcode" HeaderText="Barcode" SortExpression="Barcode" UniqueName="Barcode" FilterControlAltText="Filter Barcode column">
                </telerik:GridBoundColumn>
                <telerik:GridNumericColumn DataField="Value" HeaderText="Preis" SortExpression="Value" UniqueName="Value" FilterControlAltText="Filter Value column"
                    DataFormatString="{0:##,##0.00}" FilterControlWidth="80px">
                </telerik:GridNumericColumn>
                <telerik:GridNumericColumn DataField="DepreciationValue" HeaderText="Abschreibung" SortExpression="DepreciationValue" UniqueName="DepreciationValue" FilterControlAltText="Filter DepreciationValue column"
                    DataFormatString="{0:##,##0.00}" FilterControlWidth="80px">
                </telerik:GridNumericColumn>
                <telerik:GridBoundColumn DataField="ArticleGroup.Name" HeaderText="ArticleGroup" SortExpression="ArticleGroup.Name" UniqueName="ArticleGroup.Name" FilterControlAltText="Filter ArticleGroup.Name column">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="AcquisitionDate" DataFormatString="{0:MM/dd/yy}" HeaderText="Anschaffungsdatum" SortExpression="AcquisitionDate" UniqueName="AcquisitionDate" FilterControlAltText="Filter AcquisitionDate column">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="SupplierBranch.Supplier.Name" HeaderText="Lieferant" SortExpression="SupplierBranch.Supplier.Name" UniqueName="SupplierBranch.Supplier.Name" FilterControlAltText="Filter SupplierBranch.Supplier.Name column">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="Room.Name" HeaderText="Raum" SortExpression="Room.Name" UniqueName="Room.Name" FilterControlAltText="Filter Room.Name column">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="Room.ResponsiblePerson" HeaderText="Verantwortlich" SortExpression="Room.ResponsiblePerson" UniqueName="Room.ResponsiblePerson" FilterControlAltText="Filter Room.ResponsiblePerson column">
                </telerik:GridBoundColumn>
            </Columns>
        </MasterTableView>
    </telerik:RadGrid>

</asp:Content>
