<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMaster/StandardMaster.Master" AutoEventWireup="true" CodeBehind="SupplierList.aspx.cs" Inherits="Client.Site.Administrator.SupplierList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder" runat="server">

    <telerik:RadGrid ID="rgSupplier" runat="server" AllowFilteringByColumn="True" AllowSorting="True" CellSpacing="0" DataSourceID="SupplierDataSource" GridLines="None">

        <MasterTableView DataSourceID="SupplierDataSource" AutoGenerateColumns="False" DataKeyNames="SupplierId">

            <Columns>
                <telerik:GridBoundColumn DataField="SupplierId" Visible="false" DataType="System.Int32" FilterControlAltText="Filter SupplierId column" HeaderText="SupplierId" ReadOnly="True" SortExpression="SupplierId" UniqueName="SupplierId">
                    <ColumnValidationSettings>
                        <ModelErrorMessage Text="" />
                    </ColumnValidationSettings>
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="Name" FilterControlAltText="Filter Name column" HeaderText="Name" SortExpression="Name" UniqueName="Name">
                    <ColumnValidationSettings>
                        <ModelErrorMessage Text="" />
                    </ColumnValidationSettings>
                </telerik:GridBoundColumn>
            </Columns>
        </MasterTableView>

    </telerik:RadGrid>

    <asp:SqlDataSource ID="SupplierDataSource" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand="SELECT * FROM [Supplier]"></asp:SqlDataSource>

</asp:Content>
