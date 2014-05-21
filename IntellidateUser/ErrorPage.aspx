<%@ Page Title="" Language="C#" MasterPageFile="~/Default.Master" AutoEventWireup="true" CodeBehind="ErrorPage.aspx.cs" Inherits="IntellidateUser.ErrorPage" %>

<%@ Register Src="~/uc/uc_error.ascx" TagPrefix="uc1" TagName="uc_error" %>

<asp:Content ID="Content1" ContentPlaceHolderID="objHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="objContentPlaceHolder" runat="server">
    <uc1:uc_error runat="server" id="uc_error" />
</asp:Content>
