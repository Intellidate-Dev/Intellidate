<%@ Page Title="" Language="C#" EnableEventValidation="false"  MasterPageFile="~/Default.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="IntellidateUser.Login" %>

<%@ Register Src="~/uc/uc_login.ascx" TagPrefix="uc1" TagName="uc_login" %>
<%@ Register Src="~/uc/uc_forgotpassword.ascx" TagPrefix="uc1" TagName="uc_forgotpassword" %>


<asp:Content ID="Content1" ContentPlaceHolderID="objHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="objContentPlaceHolder" runat="server">
    <uc1:uc_login runat="server" ID="uc_login" />
    <div class="dvFpassword" style="display:none">
    <uc1:uc_forgotpassword runat="server" ID="uc_forgotpassword" />
        </div>
</asp:Content>
