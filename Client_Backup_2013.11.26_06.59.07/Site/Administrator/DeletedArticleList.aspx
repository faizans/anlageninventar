<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMaster/StandardMaster.Master" AutoEventWireup="true" EnableEventValidation="False" CodeBehind="DeletedArticleList.aspx.cs" Inherits="Client.Site.Administrator.DeletedArticleList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder" runat="server">

    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="rgArticles">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rgArticles" ></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>


    <telerik:RadGrid ID="rgArticles" runat="server" AutoGenerateEditColumn="False" CellSpacing="0" GridLines="None" Skin="Silk" OnInit="rgArticles_Init"
        AllowPaging="True" AllowSorting="True" AllowFilteringByColumn="true" ShowStatusBar="true" Height="580px" ClientSettings-DataBinding-EnableCaching="true"
        OnItemCommand="rgArticles_ItemCommand" DataSourceID="GridSource" AllowMultiRowSelection="True" OnPreRender="rgArticles_PreRender" OnItemCreated="rgArticles_ItemCreated">

        <PagerStyle Visible="true"></PagerStyle>

        <ClientSettings>
            <Scrolling  AllowScroll="True" EnableVirtualScrollPaging="False" UseStaticHeaders="True"
                SaveScrollPosition="True"></Scrolling>
        </ClientSettings>

        <ExportSettings HideStructureColumns="true" />

        <MasterTableView DataKeyNames="ArticleId" AutoGenerateColumns="False" AllowAutomaticDeletes="false" AllowAutomaticInserts="false" AllowAutomaticUpdates="false"
            CommandItemDisplay="Top" InsertItemPageIndexAction="ShowItemOnCurrentPage" DataSourceID="GridSource" PageSize="80">

            <CommandItemSettings ShowExportToExcelButton="true" />

            <CommandItemTemplate>
                <div style="padding: 5px;">
                    <telerik:RadButton ID="btnReverseSelection" runat="server" CommandName="ReverseSelection" Text="Wieder in Bestand aufnehmen" OnClientClicking="OnClientClicking" />
                    <telerik:RadButton ID="btnReallyDelete" runat="server" CommandName="ReallyDelete" Text="Endgültig löschen" OnClientClicking="OnReallyDelete" />
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
                <telerik:GridBoundColumn DataField="SupplierBranch.Supplier.Name" HeaderText="Lieferant" SortExpression="SupplierBranch.Supplier" UniqueName="SupplierBranch.Supplier" FilterControlAltText="Filter SupplierBranch.Supplier column">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="Room.Name" HeaderText="Raum" SortExpression="Room.Name" UniqueName="Room.Name" FilterControlAltText="Filter Room.Name column">
                </telerik:GridBoundColumn>
            </Columns>
        </MasterTableView>

    </telerik:RadGrid>
    <asp:ObjectDataSource ID="GridSource" runat="server" SelectMethod="GetDeleted" TypeName="Data.Model.Diagram.Article"></asp:ObjectDataSource>

    <telerik:RadWindowManager ID="RadWindowManager1" runat="server">
    </telerik:RadWindowManager>
    <script type="text/javascript">
        function alertCallBackFn(arg) {

        }

        function OnClientClicking(sender, args) {
            var callBackFunction = Function.createDelegate(sender, function (argument) {
                if (argument) {
                    this.click();
                }
            });

            var text = "Sind Sie sicher dass Sie die Auswahl wieder in den aktiven Bestand aufnhemen wollen?";
            radconfirm(text, callBackFunction, 300, 100, null, "In Bestand aufnehmen");
            args.set_cancel(true);
        }

        function OnReallyDelete(sender, args) {
            var callBackFunction = Function.createDelegate(sender, function (argument) {
                if (argument) {
                    this.click();
                }
            });

            var text = "Sind Sie sicher dass Sie die Auswahl endgültig aus dem Inventar entfernen wollen?";
            radconfirm(text, callBackFunction, 300, 100, null, "Endgültig entfernen");
            args.set_cancel(true);
        }

    </script>
</asp:Content>
