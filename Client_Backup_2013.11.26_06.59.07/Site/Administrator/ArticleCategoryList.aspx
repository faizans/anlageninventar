﻿<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMaster/StandardMaster.Master" AutoEventWireup="true" CodeBehind="ArticleCategoryList.aspx.cs" Inherits="Client.Site.Administrator.ArticleCategoryList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server">
        <telerik:RadGrid ID="rgCategories" runat="server" AutoGenerateEditColumn="True" AutoGenerateDeleteColumn="True" CellSpacing="0" GridLines="None"  Skin="Silk"
            AllowPaging="True" AllowSorting="True" ShowStatusBar="true" DataSourceID="EntityDataSource1"
            AllowAutomaticInserts="true" AllowAutomaticUpdates="true" OnItemCommand="rgCategories_ItemCommand">

            <ClientSettings>
                <Scrolling AllowScroll="True" UseStaticHeaders="True"></Scrolling>
            </ClientSettings>

            <MasterTableView DataKeyNames="ArticleCategoryId" AutoGenerateColumns="False" AllowAutomaticDeletes="true" AllowAutomaticInserts="true" AllowAutomaticUpdates="true"
                CommandItemDisplay="Top" InsertItemPageIndexAction="ShowItemOnCurrentPage">

                <CommandItemTemplate>
                    <div style="padding: 5px;">
                        <telerik:RadButton ID="btnNew" runat="server" CommandName="InitInsert" Text="Neue Kategorie" AutoPostBack="true"/>
                    </div>
                </CommandItemTemplate>

                <Columns>
                    <telerik:GridBoundColumn DataField="ArticleCategoryId" Display="false" ReadOnly="True" HeaderText="ArticleCategoryId" SortExpression="ArticleCategoryId" UniqueName="ArticleCategoryId" DataType="System.Int32" FilterControlAltText="Filter ArticleCategoryId column">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="Name" HeaderText="Name" SortExpression="Name" UniqueName="Name" FilterControlAltText="Filter Name column">
                    </telerik:GridBoundColumn>
                </Columns>
            </MasterTableView>

        </telerik:RadGrid>

        <asp:EntityDataSource runat="server" ID="EntityDataSource1" DefaultContainerName="IP3AnlagenInventarEntities" ConnectionString="name=IP3AnlagenInventarEntities" EnableFlattening="False" EnableInsert="True" EnableUpdate="True" EntitySetName="ArticleCategories">
        </asp:EntityDataSource>
    </telerik:RadAjaxPanel>
    <telerik:RadWindowManager ID="RadWindowManager1" runat="server">
    </telerik:RadWindowManager>
    <script>
        function alertCallBackFn(arg) {

        }

    </script>
</asp:Content>