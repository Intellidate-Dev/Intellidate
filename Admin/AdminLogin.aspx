<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AdminLogin.aspx.cs" Inherits="AdminModule.AdminLogin" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Intellidate - Admin</title>
    <link href="StyleSheet/style.css" rel="stylesheet" type="text/css" />
</head>
<body style="background-color: #F0F0F0; margin-top: 2px; margin-left: 2px;">
    <form id="frmLogin" runat="server">
        <div style="width: 100%; height: 98vh;">
            <div style="float: left; width: 45%; height: 100px; border: 0px solid; top: 40%; position: relative; margin-left: 150px;">
                <div style="font-family: Arial; font-size: 40px; color: #5F5F5F;" class="Transformer">
                    Intellidate Admin
                </div>
                <div style="font-family: Arial; font-size: 22px; color: #5F5F5F;" class="Transformer">Version 1.0</div>
            </div>
            <div style="float: left; width: 30%; height: 200px; border: 0px solid; top: 30%; position: relative; border-radius: 6px 0px; background-color: #fff;">
                <div style="display: block; height: 28px; background-color: #5F5F5F; border-radius: 6px 0px; font-size: 24px; color: #F0F0F0; font-family: Arial; padding-left: 8px;" class="Transformer">Authenticate</div>
                <div style="height: 40px;">&nbsp;</div>
                <div style="width: 350px; height: 200px; border: 0px solid; margin: 0px auto;">
                    <asp:Login ID="objLogin" runat="server" RememberMeSet="true" RememberMeText="Remember Me" DestinationPageUrl="~/Secured/Forums/" OnAuthenticate="objLogin_Authenticate" LoginButtonStyle-Width="100" FailureAction="RedirectToLoginPage" FailureText="Authentication Failed" FailureTextStyle-ForeColor="Red" LoginButtonText="Login" Font-Names="Arial" Font-Size="Medium" TitleText="" Width="300" CheckBoxStyle-HorizontalAlign="Center"></asp:Login>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
