﻿<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMaster/StandardMaster.Master" AutoEventWireup="true" CodeBehind="ArticleList.aspx.cs" Inherits="Client.Site.Administrator.ArticleList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server">
        <telerik:RadGrid ID="rgArticles" runat="server" AutoGenerateEditColumn="True" AutoGenerateDeleteColumn="False" CellSpacing="0" GridLines="None" Skin="Silk"
            AllowPaging="True" AllowSorting="True" AllowFilteringByColumn="true" ShowStatusBar="true" AllowAutomaticDeletes="false"
            OnItemCommand="rgArticles_ItemCommand" DataSourceID="GridSource" AllowMultiRowSelection="True" OnPreRender="rgArticles_PreRender" OnItemCreated="rgArticles_ItemCreated">

            <ClientSettings>
                <Scrolling AllowScroll="True" UseStaticHeaders="True"></Scrolling>
            </ClientSettings>

            <MasterTableView DataKeyNames="ArticleId" AutoGenerateColumns="False" AllowAutomaticDeletes="false" AllowAutomaticInserts="true" AllowAutomaticUpdates="true"
                CommandItemDisplay="Top" InsertItemPageIndexAction="ShowItemOnCurrentPage" DataSourceID="GridSource">

                <CommandItemTemplate>
                    <div style="padding: 5px;">
                        <telerik:RadButton ID="btnNew" runat="server" CommandName="InitInsert" Text="Neuer Artikel" />
                        <telerik:RadButton ID="btnDeleteSelection" runat="server" Text="Löschen" CommandName="DeleteSelection" OnClientClicking="OnClientClicking"/>

                        <telerik:RadComboBox ID="rcbTargetRoom" runat="server" Label="Verschiebe nach:"
                            DataSourceID="RoomDataSource" DataValueField="RoomId" DataTextField="RoomPath" EmptyMessage="- Raum wählen -"
                            OnSelectedIndexChanged="rcbTargetRoom_SelectedIndexChanged">
                        </telerik:RadComboBox>
                        <telerik:RadButton ID="btnMoveToRoom" runat="server" CommandName="MoveSelection" Text="Verschieben" OnClientClicking="OnMoveToRoom" />
                    </div>
                </CommandItemTemplate>

                <Columns>
                    <telerik:GridTemplateColumn UniqueName="CheckBoxTemplateColumn">
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
                    <telerik:GridBoundColumn DataField="RoomName" HeaderText="Raum" SortExpression="RoomName" UniqueName="RoomName" FilterControlAltText="Filter RoomName column">
                    </telerik:GridBoundColumn>
                </Columns>
            </MasterTableView>

        </telerik:RadGrid>
        <asp:SqlDataSource ID="GridSource" runat="server" ConnectionString='<%$ ConnectionStrings:IP3AnlagenInventarConnectionString %>' SelectCommand="
            SELECT        dbo.Article.ArticleId, dbo.Article.Name, dbo.Article.Value, dbo.Article.Amount, dbo.Article.Barcode, dbo.Article.OldBarcode, dbo.Article.ArticleGroupId, 
                dbo.Article.SupplierBranchId, dbo.Article.RoomId, dbo.Article.AcquisitionDate, dbo.Article.DepreciationId, dbo.Article.ArticleCategoryId, 
                dbo.Article.InsuranceCategoryId, dbo.Article.Comment, dbo.Article.IsAvailable, dbo.Article.IsDeleted, dbo.ArticleGroup.Name AS GroupName, 
                dbo.Room.Name AS RoomName
            FROM            dbo.Article INNER JOIN
                dbo.Room ON dbo.Article.RoomId = dbo.Room.RoomId LEFT OUTER JOIN
                dbo.ArticleGroup ON dbo.Room.RoomId = dbo.ArticleGroup.RoomId AND dbo.Article.ArticleGroupId = dbo.ArticleGroup.ArticleGroupId
            WHERE        (dbo.Article.IsDeleted = 'false')">

        </asp:SqlDataSource>
        <asp:EntityDataSource ID="RoomDataSource" runat="server"
            ConnectionString="name=IP3AnlagenInventarEntities"
            DefaultContainerName="IP3AnlagenInventarEntities"
            EnableFlattening="False" EntitySetName="Rooms">
        </asp:EntityDataSource>
    </telerik:RadAjaxPanel>
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

    </script>
</asp:Content>
