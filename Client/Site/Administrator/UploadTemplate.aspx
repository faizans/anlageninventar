<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMaster/StandardMaster.Master" AutoEventWireup="true" CodeBehind="UploadTemplate.aspx.cs" Inherits="Client.Site.Administrator.UploadTemplate" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <asp:Panel ID="pnlContent" runat="server" Width="700px">
        <div>
            <img src="../../Resources/Images/Icons/ExcelLogo.png" height="50px"  />
            <asp:Label ID="Label1" runat="server" Text="Laden Sie Ihre Excel-Exportvorlage rauf."></asp:Label>
        </div>
        <div class="content_input_form" style="margin-top:20px;">
            <!-- Domain -->
            <div class="input_form_row">
                <asp:Label ID="Label3" runat="server" Text="Vorlage" CssClass="element_label"></asp:Label>
                <div style="float: left;">
                    <telerik:RadAsyncUpload ID="rauExcelTemplate" runat="server" MaxFileInputsCount="1" AllowedFileExtensions="xls">
                    </telerik:RadAsyncUpload>
                </div>
            </div>
        </div>
        <div class="input_interaction_row">
            <telerik:RadButton ID="btnUpload" runat="server" Text="Heraufladen" OnClick="btnUpload_Click" CausesValidation="true"/>
        </div>
    </asp:Panel>
</asp:Content>
