<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMaster/StandardMaster.Master" EnableEventValidation="false" AutoEventWireup="true" CodeBehind="ReportView.aspx.cs" Inherits="Client.Site.Administrator.ReportView" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder" runat="server">

    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
        <ClientEvents OnRequestStart="onRequestStart"></ClientEvents>
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="rgReport">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rgReport"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>

    <telerik:RadGrid ID="rgReport" runat="server" AutoGenerateEditColumn="False"  CellSpacing="0" GridLines="None" Skin="Silk"
        AllowPaging="False" AllowSorting="True" AllowFilteringByColumn="true" ShowStatusBar="true" OnDataBound="rgReport_DataBound">

        <ClientSettings>
            <Scrolling AllowScroll="True" UseStaticHeaders="True"></Scrolling>
        </ClientSettings>

        <ExportSettings HideStructureColumns="true" />

        <MasterTableView DataKeyNames="ArticleId" AutoGenerateColumns="False" AllowFilteringByColumn="true" ShowFooter="true" 
            CssClass="MasterTableViewNoHeight" CommandItemDisplay="Top">

            <CommandItemTemplate>
                <div style="padding: 5px;">
                    <div style="float: right; margin-right: 10px;">
                        <asp:ImageButton ID="btnExportToExcel" ImageUrl="~/Resources/Images/Icons/ExcelBiff.png"
                            OnClick="btnExportToExcel_Click" runat="server" CssClass="ImageButtons" Height="23px" />
                    </div>
                </div>
            </CommandItemTemplate>

            <Columns>
                <telerik:GridBoundColumn DataField="ArticleId" Display="false" HeaderText="ArticleId" SortExpression="ArticleId" UniqueName="ArticleId" DataType="System.Int32" FilterControlAltText="Filter ArticleId column">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="Name" HeaderText="Name" SortExpression="Name" UniqueName="Name" FilterControlAltText="Filter Name column">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="Barcode" HeaderText="Barcode" SortExpression="Barcode" UniqueName="Barcode" FilterControlAltText="Filter Barcode column">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="Value" HeaderText="Preis" SortExpression="Value" UniqueName="Value" FilterControlAltText="Filter Value column">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="ArticleGroup.Name" HeaderText="ArticleGroup" SortExpression="ArticleGroup.Name" UniqueName="ArticleGroup.Name" FilterControlAltText="Filter ArticleGroup.Name column">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="SupplierBranch.Supplier.Name" HeaderText="Lieferant" SortExpression="SupplierBranch.Supplier.Name" UniqueName="SupplierBranch.Supplier.Name" FilterControlAltText="Filter SupplierBranch.Supplier.Name column">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="Room.Name" HeaderText="Raum" SortExpression="Room.Name" UniqueName="Room.Name" FilterControlAltText="Filter Room.Name column">
                </telerik:GridBoundColumn>
            </Columns>
        </MasterTableView>

    </telerik:RadGrid>
     <script type="text/javascript">

         function onRequestStart(sender, args) {
             if (args.get_eventTarget().indexOf("ExportTo") >= 0) {
                 args.set_enableAjax(false);
             }
         }

    </script>
</asp:Content>
