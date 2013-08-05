<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMaster/StandardMaster.Master" AutoEventWireup="true" EnableEventValidation="False" CodeBehind="ArticleList.aspx.cs" Inherits="Client.Site.Administrator.ArticleList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder" runat="server">

    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
        <ClientEvents OnRequestStart="onRequestStart"></ClientEvents>
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="rgArticles">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rgArticles"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>


    <telerik:RadGrid ID="rgArticles" runat="server" AutoGenerateEditColumn="True" CellSpacing="0" GridLines="None" Skin="Silk" OnInit="rgArticles_Init"
        AllowPaging="True" AllowSorting="True" AllowFilteringByColumn="true" ShowStatusBar="true" Height="580px" ClientSettings-DataBinding-EnableCaching="true"
        OnItemCommand="rgArticles_ItemCommand" DataSourceID="GridSource" AllowMultiRowSelection="True" OnPreRender="rgArticles_PreRender" OnItemCreated="rgArticles_ItemCreated">

        <PagerStyle Visible="true"></PagerStyle>

        <ClientSettings>
            <Scrolling AllowScroll="True" EnableVirtualScrollPaging="False" UseStaticHeaders="True"
                SaveScrollPosition="True"></Scrolling>
        </ClientSettings>

        <ExportSettings HideStructureColumns="true" />

        <MasterTableView DataKeyNames="ArticleId" AutoGenerateColumns="False" AllowAutomaticDeletes="false" AllowAutomaticInserts="true" AllowAutomaticUpdates="true"
            CommandItemDisplay="Top" InsertItemPageIndexAction="ShowItemOnCurrentPage" DataSourceID="GridSource" PageSize="80">

            <CommandItemSettings ShowExportToExcelButton="true" />

            <CommandItemTemplate>
                <div style="padding: 5px;">
                    <telerik:RadButton ID="btnNew" runat="server" CommandName="InitInsert" Text="Neuer Artikel" />
                    <telerik:RadButton ID="btnDeleteSelection" runat="server" Text="Löschen" CommandName="DeleteSelection" OnClientClicking="OnClientClicking" />

                    <telerik:RadComboBox ID="rcbTargetRoom" runat="server" Label="Verschiebe nach:"
                        DataSourceID="RoomDataSource" DataValueField="RoomId" DataTextField="RoomPath" EmptyMessage="- Raum wählen -"
                        OnSelectedIndexChanged="rcbTargetRoom_SelectedIndexChanged">
                    </telerik:RadComboBox>

                    <telerik:RadButton ID="btnMoveToRoom" runat="server" CommandName="MoveSelection" Text="Verschieben" OnClientClicking="OnMoveToRoom" />
                    <div style="float: right; margin-right: 5px;">
                        <asp:ImageButton ID="btnReport" ImageUrl="~/Resources/Images/Icons/Report1.png" ToolTip="Generiere Report"
                            OnClick="btnReport_Click" runat="server" CssClass="ImageButtons" Height="25px" />
                    </div>
                    <div style="float: right; margin-right: 5px;">
                        <telerik:RadComboBox ID="rcbExcelTemplate" runat="server" DataValueField="FullName" DataTextField="Name" Label="Excelvorlage" AutoPostBack="true"
                            OnSelectedIndexChanged="rcbExcelTemplate_SelectedIndexChanged">
                        </telerik:RadComboBox>
                        <div style="margin-right: 10px; margin-left: 10px; float: right;">
                            <asp:ImageButton ID="btnExportToExcel" ImageUrl="~/Resources/Images/Icons/ExcelBiff.png" ToolTip="Exportiere zu Excel"
                                OnClick="btnExportToExcel_Click" runat="server" CssClass="ImageButtons" Height="23px" />
                        </div>
                    </div>
                </div>
            </CommandItemTemplate>

            <Columns>
                <telerik:GridTemplateColumn UniqueName="CheckBoxTemplateColumn" Exportable="false" AllowFiltering="false" HeaderStyle-Width="40px" ItemStyle-Width="40px">
                    <HeaderTemplate>
                        <asp:CheckBox ID="chbHeaderSelection" OnCheckedChanged="ToggleSelectedState" AutoPostBack="True"
                            runat="server"></asp:CheckBox>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:CheckBox ID="chbSelection" OnCheckedChanged="ToggleRowSelection" AutoPostBack="True"
                            runat="server"></asp:CheckBox>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridBoundColumn DataField="ArticleId" ReadOnly="true" Display="false" HeaderText="ArticleId" SortExpression="ArticleId" UniqueName="ArticleId" DataType="System.Int32" FilterControlAltText="Filter ArticleId column">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="Name" HeaderText="Name" SortExpression="Name" UniqueName="Name" FilterControlAltText="Filter Name column">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="Barcode" HeaderText="Barcode" SortExpression="Barcode" UniqueName="Barcode" FilterControlAltText="Filter Barcode column">
                </telerik:GridBoundColumn>
                <telerik:GridNumericColumn DataField="Value" HeaderText="Preis" SortExpression="Value" UniqueName="Value" FilterControlAltText="Filter Value column"
                    DataFormatString="{0:##,##0.00}" FilterControlWidth="80px">
                </telerik:GridNumericColumn>
                <telerik:GridNumericColumn DataField="DepreciationValue" HeaderText="Abschreibung" SortExpression="DepreciationValue" UniqueName="DepreciationValue" FilterControlAltText="Filter DepreciationValue column"
                    DataFormatString="{0:##,##0.00}" FilterControlWidth="80px">
                </telerik:GridNumericColumn>
                <telerik:GridBoundColumn DataField="ArticleGroup.Name" HeaderText="Gruppe" SortExpression="ArticleGroup.Name" UniqueName="ArticleGroup.Name" FilterControlAltText="Filter ArticleGroup.Name column">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="AcquisitionDate" DataFormatString="{0:MM/dd/yy}" HeaderText="Anschaffungsdatum" SortExpression="AcquisitionDate" UniqueName="AcquisitionDate" FilterControlAltText="Filter AcquisitionDate column">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="SupplierBranch.Supplier.Name" HeaderText="Lieferant" SortExpression="SupplierBranch.Supplier" UniqueName="SupplierBranch.Supplier" FilterControlAltText="Filter SupplierBranch.Supplier column">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="Room.Name" HeaderText="Raum" SortExpression="Room.Name" UniqueName="Room.Name" FilterControlAltText="Filter Room.Name column">
                </telerik:GridBoundColumn>
            </Columns>
        </MasterTableView>

    </telerik:RadGrid>
    <asp:ObjectDataSource ID="GridSource" runat="server" SelectMethod="GetAvailable" TypeName="Data.Model.Diagram.Article"></asp:ObjectDataSource>

    <asp:EntityDataSource ID="RoomDataSource" runat="server"
        ConnectionString="name=IP3AnlagenInventarEntities"
        DefaultContainerName="IP3AnlagenInventarEntities"
        EnableFlattening="False" EntitySetName="Rooms">
    </asp:EntityDataSource>

    <telerik:RadWindowManager ID="RadWindowManager1" runat="server">
    </telerik:RadWindowManager>
    <script type="text/javascript">
        function alertCallBackFn(arg) {

        }

        function confirmCallBackFn(arg) {

        }

        function OnMoveToRoom(sender, args) {
            var callBackFunction = Function.createDelegate(sender, function (argument) {
                if (argument) {
                    this.click();
                }
            });

            var text = "Sind Sie sicher dass Sie die Auswahl verschieben möchten?";
            radconfirm(text, callBackFunction, 300, 100, null, "Verschieben");
            args.set_cancel(true);
        }

        function OnClientClicking(sender, args) {
            var callBackFunction = Function.createDelegate(sender, function (argument) {
                if (argument) {
                    this.click();
                }
            });

            var text = "Sind Sie sicher dass Sie den Artikel entfernen wollen?";
            radconfirm(text, callBackFunction, 300, 100, null, "Löschen");
            args.set_cancel(true);
        }

        function onRequestStart(sender, args) {
            if (args.get_eventTarget().indexOf("ExportTo") >= 0) {
                args.set_enableAjax(false);
            }
        }

    </script>
</asp:Content>
