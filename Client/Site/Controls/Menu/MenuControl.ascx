<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MenuControl.ascx.cs" Inherits="Client.Site.Controls.Menu.MenuControl" %>
<telerik:RadMenu runat="server" ID="NavigationMenu" CssClass="CustomMenu">
    <Items>
        <telerik:RadMenuItem runat="server" CssClass="MenuItem">
            <ItemTemplate>
                <div>
                    <asp:LinkButton ID="lbArticles" PostBackUrl="~/Site/Administrator/ArticleList.aspx" runat="server">Artikel</asp:LinkButton>
                </div>
            </ItemTemplate>
        </telerik:RadMenuItem>
        <telerik:RadMenuItem runat="server" CssClass="MenuItem">
            <ItemTemplate>
                <div>
                    <asp:LinkButton ID="lbArticleCategories" PostBackUrl="~/Site/Administrator/ArticleCategoryList.aspx" runat="server">Kategorien</asp:LinkButton>
                </div>
            </ItemTemplate>
            <Items>
                <telerik:RadMenuItem runat="server" CssClass="MenuItem">
                    <ItemTemplate>
                        <div>
                             <asp:LinkButton ID="lbArticleCategories" PostBackUrl="~/Site/Administrator/ArticleCategoryList.aspx" runat="server">Artikel</asp:LinkButton>
                        </div>
                    </ItemTemplate>
                </telerik:RadMenuItem>
                <telerik:RadMenuItem runat="server" CssClass="MenuItem">
                    <ItemTemplate>
                        <div>
                            <asp:LinkButton ID="lbArticleCategories" PostBackUrl="~/Site/Administrator/DepreciationCategoryList.aspx" runat="server">Abschreibung</asp:LinkButton>
                        </div>
                    </ItemTemplate>
                </telerik:RadMenuItem>
                 <telerik:RadMenuItem runat="server" CssClass="MenuItem">
                    <ItemTemplate>
                        <div>
                            <asp:LinkButton ID="lbArticleCategories" PostBackUrl="~/Site/Administrator/InsuranceCategoryList.aspx" runat="server">Versicherung</asp:LinkButton>
                        </div>
                    </ItemTemplate>
                </telerik:RadMenuItem>
            </Items>
        </telerik:RadMenuItem>
        <telerik:RadMenuItem runat="server" CssClass="MenuItem">
            <ItemTemplate>
                <div>
                    <asp:LinkButton ID="lbArticleCategories" PostBackUrl="~/Site/Administrator/RoomList.aspx" runat="server">Räume</asp:LinkButton>
                </div>
            </ItemTemplate>
        </telerik:RadMenuItem>
        <telerik:RadMenuItem runat="server" CssClass="MenuItem">
            <ItemTemplate>
                <div>
                    <asp:LinkButton ID="lbArticleCategories" PostBackUrl="~/Site/Administrator/SupplierList.aspx" runat="server">Lieferanten</asp:LinkButton>
                </div>
            </ItemTemplate>
        </telerik:RadMenuItem>
        <telerik:RadMenuItem runat="server" CssClass="MenuItem">
            <ItemTemplate>
                <div>
                    <asp:LinkButton ID="lbArticleCategories" PostBackUrl="~/Site/Administrator/UserList.aspx" runat="server">Benutzer</asp:LinkButton>
                </div>
            </ItemTemplate>
        </telerik:RadMenuItem>

        <telerik:RadMenuItem Value="ReportItem" runat="server" CssClass="MenuItem">
            <ItemTemplate>
                <div>
                    <asp:LinkButton ID="lbReports"  runat="server">Reports</asp:LinkButton>
                </div>
            </ItemTemplate>
            <Items>
                <telerik:RadMenuItem runat="server" CssClass="MenuItem">
                    <ItemTemplate>
                        <div>
                             <asp:LinkButton ID="lbAllReport" runat="server" CommandName="AllArticles" OnClick="lbMenuItemClicked">Alle Artikel</asp:LinkButton>
                        </div>
                    </ItemTemplate>
                </telerik:RadMenuItem>
                 <telerik:RadMenuItem runat="server" CssClass="MenuItem">
                    <ItemTemplate>
                        <div>
                             <asp:LinkButton ID="lbDeletedArticles" runat="server" CommandName="DeletedArticles" OnClick="lbMenuItemClicked">Gelöschte Artikel</asp:LinkButton>
                        </div>
                    </ItemTemplate>
                </telerik:RadMenuItem>
            </Items>
        </telerik:RadMenuItem>
    </Items>
</telerik:RadMenu>

