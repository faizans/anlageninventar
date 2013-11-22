<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMaster/StandardMaster.Master" AutoEventWireup="true" CodeBehind="PrintView.aspx.cs" Inherits="Client.Site.Administrator.Report.PrintView" %>

<%@ Register Assembly="Telerik.ReportViewer.WebForms, Version=7.2.13.1016, Culture=neutral, PublicKeyToken=a9d7983dfcc261be" Namespace="Telerik.ReportViewer.WebForms" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <telerik:ReportViewer ID="reportView" runat="server" Height="600px" Width="100%"></telerik:ReportViewer>
</asp:Content>
