<%@ Page Title="" Language="C#" MasterPageFile="~/web/UserSite.Master" AutoEventWireup="true" CodeBehind="Compose.aspx.cs" Inherits="IntellidateUser.web.Compose" %>

<%@ Register Src="~/web/uc/uc _compose.ascx" TagPrefix="uc1" TagName="uc_compose" %>

<asp:Content ID="Content1" ContentPlaceHolderID="objHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="objContentPlaceHolder" runat="server">
    <uc1:uc_compose runat="server" id="uc_compose" />
</asp:Content>
