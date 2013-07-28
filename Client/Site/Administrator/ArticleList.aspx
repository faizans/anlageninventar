﻿<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMaster/StandardMaster.Master" AutoEventWireup="true" CodeBehind="ArticleList.aspx.cs" Inherits="Client.Site.Administrator.ArticleList" %>

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
        
    <telerik:RadGrid ID="rgArticles" runat="server" AutoGenerateEditColumn="True" CellSpacing="0" GridLines="None" Skin="Silk"
        AllowPaging="True" AllowSorting="True" AllowFilteringByColumn="true" ShowStatusBar="true"
        OnItemCommand="rgArticles_ItemCommand" DataSourceID="GridSource" AllowMultiRowSelection="True" OnPreRender="rgArticles_PreRender" OnItemCreated="rgArticles_ItemCreated">

            <ClientSettings>
                <Scrolling AllowScroll="True" UseStaticHeaders="True"></Scrolling>
            </ClientSettings>

            <ExportSettings HideStructureColumns="true"/>

            <MasterTableView DataKeyNames="ArticleId" AutoGenerateColumns="False" AllowAutomaticDeletes="false" AllowAutomaticInserts="true" AllowAutomaticUpdates="true"
                CommandItemDisplay="Top" InsertItemPageIndexAction="ShowItemOnCurrentPage" DataSourceID="GridSource">

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
                        <div style="float:right; margin-right:10px;">
                            <asp:ImageButton ID="btnExportToExcel" ImageUrl="~/Resources/Images/Icons/ExcelBiff.png" ToolTip="Exportiere zu Excel"
                                OnClick="btnExportToExcel_Click" runat="server" CssClass="ImageButtons" Height="23px" />
                        </div>
                        <div style="float:right; margin-right:5px;">
                            <asp:ImageButton ID="btnReport" ImageUrl="~/Resources/Images/Icons/Report1.png" ToolTip="Generiere Report"
                                OnClick="btnReport_Click" runat="server" CssClass="ImageButtons" Height="25px" />
                        </div>
                    </div>
                </CommandItemTemplate>

                <Columns>
                    <telerik:GridTemplateColumn UniqueName="CheckBoxTemplateColumn" Exportable="false">
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
                    <telerik:GridBoundColumn DataField="Value" HeaderText="Preis" SortExpression="Value" UniqueName="Value" FilterControlAltText="Filter Value column">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="GroupName" HeaderText="Gruppe" SortExpression="GroupName" UniqueName="GroupName" FilterControlAltText="Filter GroupName column">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="SupplierName" HeaderText="Lieferant" SortExpression="SupplierName" UniqueName="SupplierName" FilterControlAltText="Filter SupplierName column">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="RoomName" HeaderText="Raum" SortExpression="RoomName" UniqueName="RoomName" FilterControlAltText="Filter RoomName column">
                    </telerik:GridBoundColumn>
                </Columns>
            </MasterTableView>

        </telerik:RadGrid>
    <asp:SqlDataSource ID="GridSource" runat="server" ConnectionString='<%$ ConnectionStrings:IP3AnlagenInventarConnectionString %>' SelectCommand="SELECT ArticleGroup.Name AS GroupName, Building.Name + '/' + Floor.Name + '/' + Room.Name AS RoomName, Article.ArticleId, Article.Name, Article.Value, Article.Amount, Article.Barcode, Article.OldBarcode, Article.ArticleGroupId, Article.SupplierBranchId, Article.RoomId, Article.AcquisitionDate, Article.DepreciationId, Article.ArticleCategoryId, Article.InsuranceCategoryId, Article.Comment, Article.IsAvailable, Article.IsDeleted, Supplier.Name AS SupplierName FROM Supplier INNER JOIN SupplierBranch INNER JOIN Article ON SupplierBranch.SupplierBranchId = Article.SupplierBranchId ON Supplier.SupplierId = SupplierBranch.SupplierId FULL OUTER JOIN ArticleGroup ON Article.ArticleGroupId = ArticleGroup.ArticleGroupId FULL OUTER JOIN Floor INNER JOIN Room ON Floor.FloorId = Room.FloorId INNER JOIN Building ON Floor.BuildingId = Building.BuildingId ON Article.RoomId = Room.RoomId WHERE (Article.IsDeleted = 'false')"></asp:SqlDataSource>
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
