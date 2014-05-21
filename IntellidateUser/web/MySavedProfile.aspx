<%@ Page Title="" Language="C#" MasterPageFile="~/web/UserSite.Master" AutoEventWireup="true" CodeBehind="MySavedProfile.aspx.cs" Inherits="IntellidateUser.web.WebForm3" %>

<%@ Register Src="~/web/uc/uc_whosavedme.ascx" TagPrefix="uc1" TagName="uc_whosavedme" %>

<asp:Content ID="Content1" ContentPlaceHolderID="objHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="objContentPlaceHolder" runat="server">
    <uc1:uc_whosavedme runat="server" ID="uc_whosavedme" Name="MySaved" />
    <script src="../Scripts/jquery.timeago.js"></script>
    <script src="js/jswhosavedme.js"></script>
</asp:Content>
