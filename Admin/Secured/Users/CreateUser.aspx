<%@ Page Title="" Language="C#" MasterPageFile="~/Secured/AdminMaster.Master" AutoEventWireup="true" CodeBehind="CreateUser.aspx.cs" Inherits="AdminModule.Secured.CreateUser" %>

<asp:Content ID="Content1" ContentPlaceHolderID="objHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="objContentPlaceHolder" runat="server">
    <script src="../../Scripts/jquery-1.7.1.min.js"></script>
    <link type="text/css" rel="stylesheet" href="//code.jquery.com/ui/1.10.4/themes/smoothness/jquery-ui.css">
    <script src="//code.jquery.com/jquery-1.9.1.js"></script>
    <script src="//code.jquery.com/ui/1.10.4/jquery-ui.js"></script>
    <link type="text/css" rel="stylesheet" href="/resources/demos/style.css">


    <div class="MediamGap"></div>
    <div class="UserBox">
        <div class="UserTitle">
            <span>Create New User</span>
        </div>

        <div class="SmallGap"></div>
        <div style="margin: 0px auto; width: 280px;">
            <div style="padding: 8px;">
                <input type="text" id="txtLogin" class="NormalText" placeholder="Login Name" />
            </div>
            <div style="padding: 8px;">
                <input type="text" id="txtFullName" class="NormalText" placeholder="Full Name" />
            </div>
            <div style="padding: 8px;">
                <input type="text" id="txtEmail" class="NormalText" placeholder="Email Address" />
            </div>
            <div style="padding: 8px;">
                <input type="password" id="txtPassword" class="NormalText" placeholder="Password" />
            </div>
            <div style="padding: 8px;">
                <input type="password" id="txtConfirmPwd" class="NormalText" placeholder="Confirm password" />
            </div>
            <div style="padding: 8px;">
                <select class="NormalText" id="ddlGender" style="height: 28px; width: 268px;">
                    <option value="0">Choose gender</option>
                    <option value="1">Male</option>
                    <option value="2">Female</option>
                    <option value="3">Other</option>
                </select>
            </div>
            <div style="padding: 8px;">
                <input type="text" id="txtDob" class="NormalText" placeholder="Date of birth" />
            </div>
            <div style="padding: 8px;">
                <input type="button" id="cmdCreateUser" style="height: 30px; width: 268px;" class="NormalText" value="Create User" />
            </div>
            <p style="text-align: center; color: red; font-family: Arial; font-size: 14px;" id="pMsg"></p>
            <input type="hidden" id="hdnValue" />
        </div>
    </div>

    <script src="../../Scripts/jquery-1.9.1.js"></script>
    <script src="../../Scripts/jquery-ui.js"></script>
    <link href="../../StyleSheet/jquery-ui.css" rel="stylesheet" />
    <link rel="stylesheet" href="/resources/demos/style.css">
    <script src="../../Scripts/Snippets.js"></script>
    <script src="CreateUser.js"></script>
</asp:Content>
