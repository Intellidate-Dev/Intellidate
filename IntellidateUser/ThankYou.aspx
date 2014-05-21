<%@ Page Title="" Language="C#" MasterPageFile="~/Default.Master" AutoEventWireup="true" CodeBehind="ThankYou.aspx.cs" Inherits="IntellidateUser.ThankYou" %>

<%@ Register Src="~/uc/uc_thankyou.ascx" TagPrefix="uc1" TagName="uc_thankyou" %>

<asp:Content ID="Content1" ContentPlaceHolderID="objHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="objContentPlaceHolder" runat="server">
    <uc1:uc_thankyou runat="server" id="uc_thankyou" />
</asp:Content>
