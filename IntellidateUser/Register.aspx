<%@ Page Title="" Language="C#" MasterPageFile="~/Default.Master" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="IntellidateUser.Register" %>

<%@ Register Src="~/uc/uc_register.ascx" TagPrefix="uc1" TagName="uc_register" %>



<asp:Content ID="Content1" ContentPlaceHolderID="objHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="objContentPlaceHolder" runat="server">
    
    <uc1:uc_register runat="server" id="uc_register" />
    

</asp:Content> 
