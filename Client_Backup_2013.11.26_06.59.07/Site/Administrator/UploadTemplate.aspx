<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMaster/StandardMaster.Master" AutoEventWireup="true" CodeBehind="UploadTemplate.aspx.cs" Inherits="Client.Site.Administrator.UploadTemplate" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <asp:Panel ID="pnlContent" runat="server" Width="700px">
        <div>
            <img src="../../Resources/Images/Icons/ExcelLogo.png" height="50px" />
            <asp:Label ID="Label1" runat="server" Text="Laden Sie Ihre Excel-Exportvorlage rauf."></asp:Label>
        </div>
        <div class="content_input_form" style="margin-top: 20px;">
            <!-- Domain -->
            <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server">
                <div class="input_form_row">
                    <asp:Label ID="Label2" runat="server" Text="Vorlagen" CssClass="element_label"></asp:Label>
                    <div style="float: left;">
                        <telerik:RadListBox ID="rlbTemplates" runat="server" DataValueField="FullName" DataTextField="Name" AllowDelete="true" OnDeleting="rlbTemplates_Deleting" AutoPostBackOnDelete="true"
                            Width="200px" Height="100px" OnSelectedIndexChanged="rlbTemplates_SelectedIndexChanged" AutoPostBack="true">
                        </telerik:RadListBox>
                        <br />
                        <telerik:RadButton ID="btnHerunterladen" runat="server" Text="Herunterladen" Width="168px" OnClick="btnHerunterladen_Click" Enabled="false"></telerik:RadButton>
                    </div>
                </div>
            </telerik:RadAjaxPanel>

            <div class="input_form_row">
                <asp:Label ID="Label3" runat="server" Text="Neue Vorlage" CssClass="element_label"></asp:Label>
                <div style="float: left;">
                    <telerik:RadAsyncUpload ID="rauExcelTemplate" runat="server" MaxFileInputsCount="1" AllowedFileExtensions="xls">
                    </telerik:RadAsyncUpload>
                </div>
            </div>
        </div>
        <div class="input_interaction_row">
            <telerik:RadButton ID="btnUpload" runat="server" Text="Heraufladen" OnClick="btnUpload_Click" CausesValidation="true" />
        </div>
        <telerik:RadWindowManager ID="RadWindowManager1" runat="server">
        </telerik:RadWindowManager>
    </asp:Panel>
    <script type="text/javascript">
        function alertCallBackFn(arg) {

        }
    </script>
</asp:Content>
