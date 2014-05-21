<%@ Page Title="" Language="C#" MasterPageFile="~/web/UserSite.Master" AutoEventWireup="true" CodeBehind="WhoViewedMe.aspx.cs" Inherits="IntellidateUser.web.WebForm2" %>

<%@ Register Src="~/web/uc/uc_whosavedme.ascx" TagPrefix="uc1" TagName="uc_whosavedme" %>

<asp:Content ID="Content1" ContentPlaceHolderID="objHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="objContentPlaceHolder" runat="server">
    <uc1:uc_whosavedme runat="server" ID="uc_whosavedme" Name="WhoViewed" />
    <script src="js/jswhosavedme.js"></script>
</asp:Content>
