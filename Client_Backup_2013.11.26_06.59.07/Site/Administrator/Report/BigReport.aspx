<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMaster/StandardMaster.Master" EnableEventValidation="false" AutoEventWireup="true" CodeBehind="BigReport.aspx.cs" Inherits="Client.Site.Administrator.BigReport" %>

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

    <telerik:RadGrid ID="rgReport" runat="server" AutoGenerateEditColumn="False" CellSpacing="0" GridLines="None" Skin="Silk" OnPreRender="rgReport_PreRender" PageSize="80"
        AllowPaging="True" AllowSorting="True" AllowFilteringByColumn="true" ShowStatusBar="true" OnDataBound="rgReport_DataBound" OnInit="rgReport_Init" Height="600px"
        ShowFooter="true">

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
                    <div style="float: right; margin-top: 3px;">
                        <telerik:RadButton ID="btnTelerikExport" runat="server" Text="RadButton" OnClientClicking="OnExportToPrintView" ToolTip="Öffne Druckansicht" Height="20px" Width="20px"
                            OnClick="btnTelerikExport_Click" Target="_blank">
                            <Image ImageUrl="~/Resources/Images/Icons/Report1.png" />
                        </telerik:RadButton>
                    </div>
                    <div style="float: right; margin-left: 5px; margin-right: 5px;">
                        <telerik:RadComboBox ID="rcbTelerikReport" runat="server" Label="Exportansicht" AutoPostBack="true"
                            OnSelectedIndexChanged="rcbTelerikReport_SelectedIndexChanged">
                        </telerik:RadComboBox>
                    </div>
                    <div>
                        <div style="float: right; margin-top: 3px; margin-right:10px;">
                            <telerik:RadButton ID="btnGroupSource" runat="server" ToolTip="Führt Artikel zusammen" Value="Artikel gruppieren" Height="20px" Width="20px"
                                OnClick="btnGroupSource_Click" Target="_blank" OnClientClicking="OnGroupArticles">
                                <Image ImageUrl="~/Resources/Images/Icons/GroupArticles.png" EnableImageButton="true" />
                            </telerik:RadButton>
                        </div>
                        <div style="float: right; margin-top: 1px; margin-right:5px;">
                            <asp:Label ID="lblGroupArticles" runat="server" Text="Artikel gruppieren" Font-Size="12px" Font-Names="segoe ui,arial,sans-serif;" ForeColor="Black" />

                        </div>
                    </div>
                    <div style="display: none;">
                        <div style="float: right; margin-top: 3px;">
                            <telerik:RadButton ID="btnExport" runat="server" Text="RadButton" OnClientClicking="OnExportToExcel" ToolTip="Exportiere zu Excel" Height="20px" Width="20px"
                                OnClick="btnExportToExcel_Click">
                                <Image ImageUrl="~/Resources/Images/Icons/Excel.jpg" />
                            </telerik:RadButton>
                        </div>
                        <div style="float: right; margin-left: 5px; margin-right: 5px;">
                            <telerik:RadComboBox ID="rcbExcelTemplate" runat="server" DataValueField="FullName" DataTextField="Name" Label="Excelvorlage" AutoPostBack="true"
                                OnSelectedIndexChanged="rcbExcelTemplate_SelectedIndexChanged">
                            </telerik:RadComboBox>
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
                <telerik:GridBoundColumn DataField="ArticleAmount" HeaderText="Anzahl" SortExpression="ArticleAmount" UniqueName="ArticleAmount" FilterControlAltText="Filter ArticleAmount column">
                </telerik:GridBoundColumn>
                <telerik:GridNumericColumn DataField="Value" HeaderText="Preis" SortExpression="Value" UniqueName="Value" FilterControlAltText="Filter Value column"
                    DataFormatString="{0:##,##0.00}" FilterControlWidth="80px">
                </telerik:GridNumericColumn>
                <telerik:GridNumericColumn DataField="AverageDepreciation" HeaderText="Abschreibung/Jahr" SortExpression="AverageDepreciation" UniqueName="AverageDepreciation" FilterControlAltText="Filter AverageDepreciation column"
                    DataFormatString="{0:##,##0.00}" FilterControlWidth="80px">
                </telerik:GridNumericColumn>
                <telerik:GridNumericColumn DataField="CumulatedDepreciation" HeaderText="Kumuliert" SortExpression="CumulatedDepreciation" UniqueName="CumulatedDepreciation" FilterControlAltText="Filter CumulatedDepreciation column"
                    DataFormatString="{0:##,##0.00}" FilterControlWidth="80px">
                </telerik:GridNumericColumn>
                <telerik:GridNumericColumn DataField="DepreciationValue" HeaderText="Ist-Wert" SortExpression="DepreciationValue" UniqueName="DepreciationValue" FilterControlAltText="Filter DepreciationValue column"
                    DataFormatString="{0:##,##0.00}" FilterControlWidth="80px">
                </telerik:GridNumericColumn>
                <telerik:GridBoundColumn DataField="DepreciationCategory.Name" HeaderText="Abschreibungskategorie" SortExpression="DepreciationCategory.Name" UniqueName="DepreciationCategory.Name" FilterControlAltText="Filter DepreciationCategory.Name column">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="InsuranceCategory.Name" HeaderText="Versicherungskategorie" SortExpression="InsuranceCategory.Name" UniqueName="InsuranceCategory.Name" FilterControlAltText="Filter InsuranceCategory.Name column">
                </telerik:GridBoundColumn>
                 <telerik:GridBoundColumn Display="false" DataField="ArticleGroup.ArticleCategory.Name" HeaderText="Artikelgruppe" SortExpression="ArticleGroup.ArticleCategory.Name" UniqueName="ArticleGroup.ArticleCategory.Name" FilterControlAltText="Filter ArticleGroup.ArticleCategory.Name column">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="AcquisitionDate" DataFormatString="{0:dd/MM/yyyy}" HeaderText="Anschaffungsdatum" SortExpression="AcquisitionDate" UniqueName="AcquisitionDate" FilterControlAltText="Filter AcquisitionDate column">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="IsAvailable" HeaderText="Ist vorhanden" SortExpression="IsAvailable" UniqueName="IsAvailable" FilterControlAltText="Filter IsAvailable column">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="SupplierBranch.Supplier.Name" HeaderText="Lieferant" SortExpression="SupplierBranch.Supplier.Name" UniqueName="SupplierBranch.Supplier.Name" FilterControlAltText="Filter SupplierBranch.Supplier.Name column">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="Room.Name" HeaderText="Raum" SortExpression="Room.Name" UniqueName="Room.Name" FilterControlAltText="Filter Room.Name column">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="Room.Floor.Name" HeaderText="Stockwerk" SortExpression="Room.Floor.Name" UniqueName="Room.Floor.Name" FilterControlAltText="Filter Room.Floor.Name column">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="Room.Floor.Building.Name" HeaderText="Gebäude" SortExpression="Room.Floor.Building.Name" UniqueName="Room.Floor.Building.Name" FilterControlAltText="Filter Room.Floor.Building.Name column">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="Room.ResponsiblePerson" HeaderText="Verantwortlich" SortExpression="Room.ResponsiblePerson" UniqueName="Room.ResponsiblePerson" FilterControlAltText="Filter Room.ResponsiblePerson column">
                </telerik:GridBoundColumn>
            </Columns>
        </MasterTableView>

        <ClientSettings>
            <Scrolling AllowScroll="true" UseStaticHeaders="true" SaveScrollPosition="true" EnableVirtualScrollPaging="true">
            </Scrolling>
        </ClientSettings>

        <PagerStyle Visible="false" AlwaysVisible="false" Height="0px" />

    </telerik:RadGrid>
    <telerik:RadWindowManager ID="RadWindowManager1" runat="server">
    </telerik:RadWindowManager>


    <script type="text/javascript">
        function alertCallBackFn(arg) {

        }

        function OnExportToExcel(sender, args) {
            var callBackFunction = Function.createDelegate(sender, function (argument) {
                if (argument) {
                    this.click();
                }
            });

            var text = "Der Export kann einige Zeit in Anspruch nehmen. Sind Sie sicher dass Sie exportieren wollen?";
            radconfirm(text, callBackFunction, 300, 100, null, "Verschieben");
            args.set_cancel(true);
        }

        function OnExportToPrintView(sender, args) {
            var callBackFunction = Function.createDelegate(sender, function (argument) {
                if (argument) {
                    this.click();
                }
            });

            var text = "Das öffnen in der Druckansicht sowie das Exportieren oder Drucken können einige Zeit in Anspruch nehmen.";
            radconfirm(text, callBackFunction, 300, 100, null, "Exportieren");
            args.set_cancel(true);
        }

        function OnGroupArticles(sender, args) {
            var callBackFunction = Function.createDelegate(sender, function (argument) {
                if (argument) {
                    this.click();
                }
            });

            var text = "Wird bearbeitet";
            radconfirm(text, callBackFunction, 300, 100, null, "Gruppieren - Streuen");
            args.set_cancel(true);
        }

    </script>
</asp:Content>
