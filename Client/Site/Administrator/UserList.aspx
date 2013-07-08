<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMaster/StandardMaster.Master" AutoEventWireup="true" CodeBehind="UserList.aspx.cs" Inherits="Client.Site.Administrator.UserList" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    
    <telerik:RadGrid ID="rgUsers" runat="server" AllowFilteringByColumn="True" AllowSorting="True" CellSpacing="0" DataSourceID="UserDataSource" GridLines="None">

        <MasterTableView DataSourceID="UserDataSource" AutoGenerateColumns="False" DataKeyNames="SupplierId">

            <Columns>
                <telerik:GridBoundColumn DataField="AppUserId" Visible="false" DataType="System.Int32" FilterControlAltText="Filter AppUserId column" HeaderText="AppUserId" ReadOnly="True" SortExpression="AppUserId" UniqueName="AppUserId">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="FirstName" FilterControlAltText="Filter FirstName column" HeaderText="Vorname" SortExpression="Vorname" UniqueName="FirstName">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="LastName" FilterControlAltText="Filter LastName column" HeaderText="Nachname" SortExpression="Nachname" UniqueName="LastName">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="UserName" FilterControlAltText="Filter UserName column" HeaderText="Username" SortExpression="Username" UniqueName="UserName">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="Email" FilterControlAltText="Filter Email column" HeaderText="Email" SortExpression="Email" UniqueName="Email">
                </telerik:GridBoundColumn>
                <telerik:GridCheckBoxColumn DataField="IsAdmin" HeaderText="Admin" >
                </telerik:GridCheckBoxColumn>

            </Columns>
        </MasterTableView>

    </telerik:RadGrid>

    <asp:SqlDataSource ID="UserDataSource" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand="SELECT * FROM [AppUser]"></asp:SqlDataSource>

</asp:Content>
