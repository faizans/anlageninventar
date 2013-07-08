<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MenuControl.ascx.cs" Inherits="Client.Site.Controls.Menu.MenuControl" %>
<telerik:RadMenu runat="server" ID="NavigationMenu" CssClass="CustomMenu">
    <Items>
        <telerik:RadMenuItem runat="server" CssClass="MenuItem">
            <ItemTemplate>
                <div>
                    <a href="~/Site/Administrator/ArticleList.aspx" runat="server"><span>Artikel</span></a>
                </div>
            </ItemTemplate>
        </telerik:RadMenuItem>
        <telerik:RadMenuItem runat="server" CssClass="MenuItem">
            <ItemTemplate>
                <div>
                   <a id="A1" href="~/Site/Administrator/CategoryList.aspx" runat="server"><span>Kategorien</span></a>
                </div>
            </ItemTemplate>
        </telerik:RadMenuItem>
        <telerik:RadMenuItem runat="server" CssClass="MenuItem">
            <ItemTemplate>
                <div>
                   <a id="A2" href="~/Site/Administrator/RoomList.aspx" runat="server"><span>Lieferanten</span></a>
                </div>
            </ItemTemplate>
        </telerik:RadMenuItem>
        <telerik:RadMenuItem runat="server" CssClass="MenuItem">
            <ItemTemplate>
                <div>
                   <a id="A3" href="~/Site/Administrator/SupplierList.aspx" runat="server"><span>Räume</span></a>
                </div>
            </ItemTemplate>
        </telerik:RadMenuItem>
        <telerik:RadMenuItem runat="server" CssClass="MenuItem">
            <ItemTemplate>
                <div>
                    <a id="A4" href="~/Site/Administrator/UserList.aspx" runat="server"><span>Benutzer</span></a>
                </div>
            </ItemTemplate>
        </telerik:RadMenuItem>
    </Items>
</telerik:RadMenu>

