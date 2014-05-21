<%@ Page Title="" Language="C#" MasterPageFile="~/web/UserSite.Master" AutoEventWireup="true" CodeBehind="WhoSavedMe.aspx.cs" Inherits="IntellidateUser.web.WebForm1" %>

<%@ Register Src="~/web/uc/uc_whosavedme.ascx" TagPrefix="uc2" TagName="uc_whosavedme" %>



<asp:Content ID="Content1" ContentPlaceHolderID="objHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="objContentPlaceHolder" runat="server">
    <uc2:uc_whosavedme runat="server" ID="uc_whosavedme1" Name="WhoSaved"/>
    <script src="../Scripts/jquery.timeago.js"></script>
    <script src="js/jswhosavedme.js"></script>
<%--    <asp:Button ID="Button1" runat="server" Text="Button" OnClick="Button1_Click" />--%>
</asp:Content>
