<%@ Page Title="" Language="C#" MasterPageFile="~/Secured/AdminMaster.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="AdminModule.Secured.Settings.Default" %>
<asp:Content ID="Content1" ContentPlaceHolderID="objHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="objContentPlaceHolder" runat="server">
    <script src="/Scripts/jquery.signalR-1.1.4.min.js"></script>
    <script src="/signalr/hubs"></script>

    <div style="margin-top:10px;border:1px solid;width:400px;height:174px;">
        <div style="padding:4px;font-size:22px;" class="Transformer">Change Password</div>
        <div style="padding:6px;">
            <input type="password" id="txtPassword" placeholder="New Password" style="width:250px;" /> 
        </div>
        <div style="padding:6px;">
            <input type="password" id="txtRPassword" placeholder="Retype Password" style="width:250px;" /> 
        </div>
        <div style="padding:6px;">
            <input type="button" id="btnChangePassword" value="Change Password" />
        </div>
         <div style="padding-left:6px;color:red;font-size:small;font-weight:bold;">
             <p style="padding: 0px;margin: 0px;" id="pPwdMsg"></p>
             </div>
    </div>
    <div style="margin-top:10px;border:1px solid;width:400px;height:130px;">
        <div style="padding:4px;font-size:22px;" class="Transformer">Change Email</div>
        <div style="padding:6px;">
            <input type="text" id="txtEmail" placeholder="Email Address" style="width:250px;" /> 
        </div>
        <div style="padding:6px;">
            <input type="button" id="btnChangeEmail" value="Change Email" />
        </div>
         <div style="padding-left:6px;float:left;color:red;font-size:small;font-weight:bold;">
             <p style="padding: 0px;margin: 0px;" id="pEmailMsg"></p>
             </div>
    </div>
    <script src="../../Scripts/Snippets.js"></script>
    
    <script src="Script.js"></script>

</asp:Content>
