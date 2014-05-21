<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="uc_resetpassword.ascx.cs" Inherits="IntellidateUser.uc.uc_resetpassword" %>
<div class="container">
    <style type="text/css">
        .msg{
            margin-left: 15px;
        }
        .displaynone{
            display:none;
        }
    </style>
    <div class="row">
        <!--signout msg-->
        <div class="col-lg-8  col-xs-offset-2" style="text-align: center; margin-top: 50px;">
            <h2 style="color: #707070;">Reset Your password - <small>go ahead and make that change !!</small> </h2>
        </div>
    </div>
    <!--end of Signout msg-->


    <div class="row">
        <!--formContainer row-->
        <div class="col-lg-8  col-xs-offset-2 bottom-rounded-corners top-rounded-corners" style="background-color: #eaeff3; height: auto; min-height: 100px; margin-top: 10px; padding-top: 15px;padding-bottom:8px;">
            <!--Parent column-->
            <form role="form">

                <div class="row">
                    <!--nested row for submit button-->

                    <div class="col-lg-4 ">
                        <input style="width: 235px;" type="password" class="txt-align-left form-control" runat="server" id="txtPassword" name="password" placeholder="password">
                    </div>

                    <div class="col-lg-4 ">
                        <input style="width: 235px;" type="password" class="txt-align-left form-control" runat="server" id="txtRePassword" name="password" placeholder="re-type password">
                    </div>

                    <div class="col-lg-4 " style="margin-top: 15px;">
                        <asp:Button ID="btnResetPwd" CssClass="btn btn-primary" runat="server" Text="Reset Password" OnClick="btnResetPwd_Click" OnClientClick="return ValidatePassword();" />
                    </div>


                </div>
                <!--end of nested row for forgot submit button-->
                <div class="row displaynone" id="dvError">
                    <div class="col-lg-5 col-xs-offset-5" style="margin-bottom: 10px;margin-left:0px;">
                        <div class="alert alert-danger" style="width: 360px;" id="dvErrorMessage"></div>
                    </div>
                </div>
                <div class="row">
                    <asp:Label ID="lblMsg" runat="server" CssClass="alert alert-danger msg" Visible="false"></asp:Label>
                    </div>
            </form>
        </div>
        <!--end of Parent column-->
    </div>
    <!--end of formContainer row-->


    <div class="row">
        <!--signout msg-->
        <div class="col-lg-12  " style="text-align: center; margin-top: 50px;">
            <h2 style="color: #a3bd96;">Statement Needed (Site promotion or App related)</h2>
        </div>
    </div>
    <!--end of Signout msg-->

</div>

<script src="../js/jsresetpwd.js"></script>
