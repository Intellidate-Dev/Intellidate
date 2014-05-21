<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="uc_percentagecircle.ascx.cs" Inherits="IntellidateUser.web.uc.uc_percentagecircle" %>
<link href="../web/css/CircleCSS.css" rel="stylesheet" />
<div id="representation">
    <div class="overlay"><%#Eval("Percentage") %></div>
    <div id="rank" style="display: none;"><%#Eval("Percentage") %></div>
</div>

