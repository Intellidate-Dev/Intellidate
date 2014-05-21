<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="uc_smallpercentagecircle.ascx.cs" Inherits="IntellidateUser.web.uc.uc_smallpercentagecircle" %>
<link href="../web/css/smallcirclecss.css" rel="stylesheet" />
<asp:DataList ID="lstsmallpercents" runat="server" RepeatDirection="Horizontal" RepeatColumns="3">
    <ItemTemplate>
        <div id="representationsmall" style="float: left;">
            <div class="overlay"><%#Eval("CriteriaPercentages") %></div>
            <div id="ranksmall" style="display: none;"><%#Eval("CriteriaPercentages") %></div>
        </div>
    </ItemTemplate>
</asp:DataList>



