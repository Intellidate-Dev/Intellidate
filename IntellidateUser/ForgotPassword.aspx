<%@ Page Title="" Language="C#" MasterPageFile="~/Default.Master" AutoEventWireup="true" CodeBehind="ForgotPassword.aspx.cs" Inherits="IntellidateUser.ForgotPassword" %>
<%@ Register Src="~/uc/uc_forgotpassword.ascx" TagPrefix="uc1" TagName="uc_forgotpassword" %>
<asp:Content ID="Content1" ContentPlaceHolderID="objHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="objContentPlaceHolder" runat="server">
    <uc1:uc_forgotpassword runat="server" ID="uc_forgotpassword" />
</asp:Content>
