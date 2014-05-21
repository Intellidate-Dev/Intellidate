<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="uc_login.ascx.cs" Inherits="IntellidateUser.uc.uc_login" %>
<style>
    .loginButton {
        width: 220px;
    }
    .lblmessage{
        width: 220px; margin-left: 80px;
    }
</style>
<div class="container">

    <div class="row">
        <!--welcome msg-->
        <div class="col-lg-4  col-xs-offset-4" style="text-align: center;">
            <h1><small>Welcome Back</small></h1>
        </div>
    </div>
    <!--end of welcome msg-->

    <div class="row" style="margin-top: 25px;">
        <!--heading row-->
        <div class="col-lg-4  col-xs-offset-4 top-rounded-corners" style="background-color: #2d6ca2; height: 60px; text-align: center;">
            <h3 class="reg-form-title">Member Login</h3>
        </div>
    </div>
    <!--end of heading row-->

    <div class="row">
        <!--formContainer row-->
        <div class="col-lg-4  col-xs-offset-4 bottom-rounded-corners" style="background-color: #eaeff3; height: auto; min-height: 400px;">
            <!--Parent column-->
            <form role="form">
                <div class="row">
                    <!--nested row for username-->
                    <div class="col-lg-12 col-xs-offset-2" style="margin-top: 15px;">
                        <input type="text" class="txtUserName txt-align-left form-control" id="txtUserName" name="username" placeholder="User name" runat="server">
                    </div>
                </div>
                <!--end of nested row for username-->
                <div class="row">
                    <!--nested row for password-->
                    <div class="col-lg-12 col-xs-offset-2">
                        <input type="password" class="txtPassword txt-align-left form-control" id="txtPassword" name="password" placeholder="Password" runat="server">
                    </div>
                </div>
                <!--end of nested row for password-->
                <div class="row displaynone" id="dvError">
                    <div class="col-lg-5 col-xs-offset-5" style="margin: 10px;">
                        <div class="alert alert-danger" style="width: 220px; margin-left: 55px" id="dvErrorMessage"></div>
                    </div>
                </div>
                <div class="row">
                    <asp:Label ID="lblErrorMessage" Visible="false" runat="server" Text="" CssClass="alert alert-danger lblmessage" ></asp:Label>
                </div>
                <div class="row">
                    <!--nested row for chk box-->
                    <div class="col-lg-8 col-xs-offset-2" style="margin-top: 10px;">
                        <label id="lblchk" style="font-family: Segoe, 'Segoe UI', 'DejaVu Sans', 'Trebuchet MS', Verdana, sans-serif; color: #858585; font-size: 14px; margin-left: 3px; font-weight: normal; margin: 0px; padding: 0px;">
                        <input type="checkbox" id="chkRemember" name="chkbox" runat="server">
                        Remember me. </label>

                    </div>
                </div>
                <!--end of nested row for chk box-->

                <div class="row">
                    <!--nested row for caution msg-->
                    <div style="text-align: center;">
                        <div>
                            <span style="height: 20px; font-size: 12px; text-align: center; color: #A00205;">Do not check if you’re at public or shared computer</span>
                        </div>
                    </div>
                </div>
                <!--end of nested row for caution msg-->

                <div class="row">
                    <!--nested row for submit button-->
                    <div class="col-lg-8 col-xs-offset-2" style="margin-top: 15px;">
                        <asp:Button ID="btnLogin" OnClientClick="return ValidateUser();" runat="server" CssClass="loginButton btn btn-primary" Text="L O G I N" OnClick="btnLogin_Click" />
                    </div>
                </div>
                <!--end of nested row for forgot submit button-->

                <div class="row">
                    <!--nested row for divider-->
                    <div class="col-lg-10 col-xs-offset-1" style="margin-top: 15px;">
                        <div><span class="divider" style="opacity: 0.2"></span></div>
                    </div>
                </div>
                <!--end of nested row for  divider-->

                <div class="row">
                    <!--nested row for join now-->
                    <div class="col-lg-9 col-xs-offset-2" style="margin-top: 25px;">
                        <div><i>Not a member yet?</i> <a href="Register"><span class="label label-success">JOIN NOW </span></a></div>
                    </div>
                </div>
                <!--end of nested row for forgot password-->

                <div class="row">
                    <!--nested row for forgot password-->
                    <div class="col-lg-10 col-xs-offset-2" style="margin-top: 12px;">
                        <div><i>Forgot password?</i> <a href="#" id="lnkFPassword">click here</a></div>
                    </div>
                </div>
                <!--end of nested row for forgot password-->
            </form>
        </div>
        <!--end of Parent column-->
    </div>
    <!--end of formContainer row-->


</div>
<script src="../js/jslogin.js"></script>
